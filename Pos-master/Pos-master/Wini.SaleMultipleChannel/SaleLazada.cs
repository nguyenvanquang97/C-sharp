using DocumentFormat.OpenXml.InkML;
using Lazop.Api.Util;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Xml.Serialization;
using Wini.Database;
using Wini.Database.Multipe_Channel;
using Wini.SaleMultipleChannel.CreateProductRequest;
using Wini.SaleMultipleChannel.Lazop;
using Wini.SaleMultipleChannel.Model.CreateProduct;
using Wini.SaleMultipleChannel.Model.Order;
using Wini.SaleMultipleChannel.Model.Product;
using Wini.SaleMultipleChannel.Model.Token;
using Wini.SaleMultipleChannel.Response.Lazada.CreateProduct;
using Wini.SaleMultipleChannel.Response.Lazada.Order;
using Wini.SaleMultipleChannel.Response.Lazada.Product;
using Wini.SaleMultipleChannel.Response.Lazada.Token;
using Wini.SaleMultipleChannel.UpdateProductRequest;
using Wini.SellMultipleChanel.Model;
using Wini.Simple;
using Wini.Utils;
using static Wini.SaleMultipleChannel.Model.Product.SaleLazada;
using da = Wini.Database;


namespace Wini.SaleMultipleChannel
{
    public partial class SaleLazada : ISale
    {
        readonly da.ApplicationDbContext _context;
        private string appKey = "117002";
        private string appSecret = "yNXHZs88Pj36kEZ6xxhUBFuROOIY23Ui";
        private string url = "https://api.lazada.vn/rest";
        public SaleLazada(da.ApplicationDbContext context)
        {
            _context = context; ;
        }

        public async Task<BaseResponse<List<Product>>> GetAllProductSynced(int agencyId)
        {
            List<Product> products = _context.Products
                   .Where(p => _context.AgencyProductEcoms.Any(a => a.ProductId == p.Id))
                   .ToList();
            BaseResponse<List<Product>> baseResponse = new BaseResponse<List<Product>>();
            if (products == null || products.Count() == 0)
            {
                baseResponse.Code = 400;
                baseResponse.Message = "Chưa có sản phẩm nào được đồng bộ";
                await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(baseResponse));
                return baseResponse;
            }

            baseResponse.Data = products;
            baseResponse.Code = 200;
            baseResponse.Message = "Có " + products.Count() + " đã được đồng bộ";
            baseResponse.TotalCount = products.Count();
            return baseResponse;
        }
        /// <summary>
        /// get all product chưa đồng bộ từ lazada
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        public BaseResponse<List<ProductEcom>> GetAllProductAsynchronous(int agencyId)
        {
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
          .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
          .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);

            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/products/get");
            request.SetHttpMethod("GET");
            LazopResponse response = client.Execute(request, accessToken);
            JObject jObject = JObject.Parse(response.Body);
            BaseResponse<List<ProductEcom>> baseResponse = new BaseResponse<List<ProductEcom>>();
            Console.WriteLine("OK");
            if (!response.IsError())
            {
                JArray products = (JArray)jObject["data"]["products"];
                List<LazadaProductResponse> lazadaProductResponse = JsonConvert.DeserializeObject<List<LazadaProductResponse>>(products.ToString());
                List<string> sellerSkus = lazadaProductResponse.SelectMany(p => p.Skus.Select(s => s.SellerSku)).ToList();

                List<string> unmatchedSellerSkus = sellerSkus.Where(sku => !_context.AgencyProductEcoms.Any(p => p.SellerSku == sku)).ToList();
                List<LazadaProductResponse> matchedProducts = lazadaProductResponse.Where(p => p.Skus.Any(s => unmatchedSellerSkus.Contains(s.SellerSku))).ToList();
                if (matchedProducts == null || matchedProducts.Count() == 0)
                {
                    baseResponse.Message = "Không có sản phẩm nào chưa đồng bộ";
                    baseResponse.Code = 200;
                    return baseResponse;
                }
                List<ProductEcom> productEcoms = matchedProducts
               .Select(productResponse => LazadaProductResponseToProdcuctEcom(productResponse))
               .ToList();

                baseResponse.Data = productEcoms;
                baseResponse.Code = 200;
                baseResponse.Message = "Success";
                baseResponse.TotalCount = productEcoms.Count();
                Console.WriteLine(JsonConvert.SerializeObject(baseResponse));
            }
            else
            {
                baseResponse.Code = 117;//Token khong chinh xac hoac het han
                if (jObject["detail"] != null && jObject["detail"].HasValues)
                {
                    baseResponse.Message = jObject["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jObject["message"].ToString();
                }
            }


            return baseResponse;

        }
        public async Task<BaseResponse<List<ProductEcom>>> SyncProductFromEcom(int agencyId, List<string> sellerSkus)
        {
            BaseResponse<List<ProductEcom>> baseResponse = GetAllProductAsynchronous(agencyId);
            List<ProductEcom> products = baseResponse.Data;
            if (products == null || products.Count() == 0)
            {
                baseResponse.Message = "Không có sản phẩm nào chưa đồng bộ";
                baseResponse.Code = 200;
                return baseResponse;
            }
            List<ProductEcom> productEcoms = products.Where(p => p.Skus.Any(s => sellerSkus.Contains(s.SellerSku))).ToList();
            if (productEcoms == null || productEcoms.Count() == 0)
            {
                baseResponse.Message = "SellerSku không chính xác";
                baseResponse.Code = 404;
                return baseResponse;
            }
            foreach (ProductEcom productEcom in productEcoms)
            {
                var pictures = productEcom.Skus
                                   .SelectMany(sku => sku.Images
                                   .Select(image => new Picture()
                                   {
                                       Name = productEcom.Attributes.Name,
                                       Url = image,
                                       DateCreated = DateTime.Now.TotalSeconds(),
                                   })).ToList();
                _context.Pictures.AddRange(pictures);
                await _context.SaveChangesAsync();
                var pictureEcoms = pictures.Select(picture => new PictureEcom()
                {
                    PictureId = picture.Id,
                    LazadaImage = picture.Url,
                })
                                                      .ToList();
                _context.PictureEcoms.AddRange(pictureEcoms);
                await _context.SaveChangesAsync();
                Product product = new Product();
                product.Name = productEcom.Attributes.Name;

                product.Price = (decimal)productEcom.Skus[0].Price;
                product.QuantityDay = productEcom.Skus[0].Quantity;
                if (productEcom.Attributes.Description != null)
                {
                    product.Description = productEcom.Attributes.Description;
                }
                else
                {
                    product.Description = productEcom.Attributes.ShortDescription;
                }

                product.PictureId = pictures[0].Id;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                ProductDetail detail = new ProductDetail();
                detail.ProductId = product.Id;
                detail.CodeSku = productEcom.Skus[0].SellerSku;
                detail.CategoryId = 2;
                detail.Longitude = 0;
                detail.Latitude = 0;

                _context.ProductDetails.Add(detail);
                await _context.SaveChangesAsync();
                ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
               .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
               .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
                if (shopAppEcommerce == null)
                {
                    return null;
                }
                AgencyProductEcom agencyProductEcom = new AgencyProductEcom();
                agencyProductEcom.ProductId = product.Id;
                agencyProductEcom.AgencyId = shopAppEcommerce.AgencyId;
                agencyProductEcom.TypeId = 1;
                agencyProductEcom.Price = product.Price;
                agencyProductEcom.SellerSku = productEcom.Skus[0].SellerSku;
                agencyProductEcom.Brand = productEcom.Attributes.Brand;
                _context.AgencyProductEcoms.Add(agencyProductEcom);
                await _context.SaveChangesAsync();
                baseResponse.Data = productEcoms;
                baseResponse.Message = "Đã đồng bộ " + productEcoms.Count() + " sản phẩm";
                baseResponse.Code = 200;
            }
            return baseResponse;
        }
        public async Task<BaseResponse<CreateProductEcom>> CreateProduct(int agencyId, Product product)
        {
            BaseResponse<CreateProductEcom> baseResponse = new BaseResponse<CreateProductEcom>();
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/product/create");
            Picture picture = _context.Pictures.FirstOrDefault(p => p.Id == product.PictureId);
            if (picture == null)
            {
                baseResponse.Code = 404;
                baseResponse.Message = "Không tìm thấy Picture với pictureId=" + product.PictureId;
                return baseResponse;
            }
            BaseResponse<string> responseImage = await MigrateImage(agencyId, picture.Url);
            if (responseImage.Code != 200)
            {
                baseResponse.Code = responseImage.Code;
                baseResponse.Message = responseImage.Message;
                return baseResponse;
            }
            string image = responseImage.Data;
            List<string> images = new List<string>();
            images.Add(image);
            LazadaProductRequest productRequest = ProductToProductRequest(product, images);
            string jsonString = JsonConvert.SerializeObject(productRequest);
            request.AddApiParameter("payload", jsonString);
            await Console.Out.WriteLineAsync(jsonString);
            LazopResponse response = client.Execute(request, accessToken);
            Console.WriteLine(response.IsError());
            JObject jObject = JObject.Parse(response.Body);

            if (!response.IsError())
            {
                PictureEcom pictureEcom = new PictureEcom()
                {
                    PictureId = product.PictureId,
                    LazadaImage = image,

                };
                _context.PictureEcoms.Add(pictureEcom);
                LazadaCreateProductResponse lazadaCreateProductResponse = JsonConvert.DeserializeObject<LazadaCreateProductResponse>(jObject["data"].ToString());
                ProductDetail productDetail = _context.ProductDetails
                                     .FirstOrDefault(pd => pd.ProductId == product.Id);
                AgencyProductEcom agencyProductEcom = new AgencyProductEcom();
                agencyProductEcom.ProductId = product.Id;
                agencyProductEcom.AgencyId = shopAppEcommerce.AgencyId;
                agencyProductEcom.TypeId = 1;
                agencyProductEcom.Price = product.Price;
                agencyProductEcom.SellerSku = productDetail.CodeSku;
                agencyProductEcom.Brand = productRequest.Request.Product.Attributes.Brand;
                _context.AgencyProductEcoms.Add(agencyProductEcom);
                await _context.SaveChangesAsync();
                CreateProductEcom createProductEcom = LazadaCreateProductToEcom(lazadaCreateProductResponse);

                baseResponse.Data = createProductEcom;
                baseResponse.Code = 200;
                baseResponse.Message = "Success";
                Console.WriteLine(JsonConvert.SerializeObject(baseResponse));
            }
            else
            {
                baseResponse.Code = (int)jObject["code"];
                if (jObject["detail"] != null && jObject["detail"].HasValues)
                {
                    baseResponse.Message = jObject["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jObject["message"].ToString();
                }
            }
            return baseResponse;
        }   

        public async Task<BaseResponse<Product>> UpdateProduct(int agencyId, ProductDetail updateProductDetail, string sellerSku)
        {
            BaseResponse<Product> baseResponse = new BaseResponse<Product>();
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            ProductDetail productDetail = _context.ProductDetails.Include(pd=>pd.Pictures).FirstOrDefault(pd => pd.Id == updateProductDetail.Id);
            if (productDetail == null)
            {
                baseResponse.Code = 400;
                baseResponse.Message = "Không tìm thấy ProductDetail với id=" + updateProductDetail.Id;
                return baseResponse;
            }
            productDetail.Name = updateProductDetail.Name;
            productDetail.Quantity = updateProductDetail.Quantity;
            productDetail.Description = updateProductDetail.Description;
            productDetail.PriceNew= updateProductDetail.PriceNew;
            productDetail.PriceOld= updateProductDetail.PriceOld;
            
            if (updateProductDetail.Pictures!=null|| updateProductDetail.Pictures.Count()>0)
            {
             
                foreach (ProductDetailPicture item in updateProductDetail.Pictures)
                {
                    ProductDetailPicture pic = new ProductDetailPicture
                    {
                        PictureId = item.PictureId,
                        ProductId = productDetail.Id,
                        Sort = 0
                    };
                    productDetail.Pictures.Add(pic);
                }
            }
             _context.SaveChanges();
            Product product = _context.Products.FirstOrDefault(p => p.Id == updateProductDetail.ProductId);
            if (product == null)
            {
                baseResponse.Code = 400;
                baseResponse.Message = "Không tìm thấy Product với id=" + updateProductDetail.ProductId;
                return baseResponse;
            }
            if(updateProductDetail.Name!=null)
            {
                product.Name = updateProductDetail.Name;
            }
            if(updateProductDetail.PriceNew!=null)
            {
                product.Price = (decimal)updateProductDetail.PriceNew;
            }
            else if(updateProductDetail.PriceOld!=null)
            {
                product.Price = updateProductDetail.PriceOld;
            }
           
            product.Description = updateProductDetail.Description;
            product.QuantityDay = (int)updateProductDetail.Quantity;

          
            await _context.SaveChangesAsync();
            AgencyProductEcom agencyProductEcom = _context.AgencyProductEcoms.FirstOrDefault(a => a.ProductId == product.Id);
            if (agencyProductEcom == null)//Sản phẩm không có trên Lazada
            {
                baseResponse.Code = 200;
                baseResponse.Data = product;
                baseResponse.Message = "Cập nhật sản phẩm trong kho thành công";
                return baseResponse;

            }
            string codeSku = productDetail.CodeSku;
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest lazopRequest = new LazopRequest();
            lazopRequest.SetApiName("/product/update");
            List<Picture> pictures= pictures = _context.ProductDetailPictures
                          .Where(p =>p.ProductId == productDetail.Id)
                          .Select(p => p.Picture)
                          .ToList();
            List<string> images = new List<string>();
            foreach (Picture p in pictures)
            {
                Picture picture = _context.Pictures.FirstOrDefault(pi => pi.Id == p.Id);
                if (picture == null)
                {
                    baseResponse.Code = 404;
                    baseResponse.Message = "Không tìm thấy Picture với pictureId=" + product.PictureId;
                    return baseResponse;
                }
                PictureEcom pictureEcom = _context.PictureEcoms.FirstOrDefault(pe => pe.PictureId == picture.Id);
              
                if (pictureEcom == null)
                {
                    BaseResponse<string> responseImage = await MigrateImage(agencyId, picture.Url);
                    if (responseImage.Code != 200)
                    {
                        baseResponse.Code = responseImage.Code;
                        baseResponse.Message = responseImage.Message;
                        return baseResponse;
                    }
                    string image = responseImage.Data;
                    images.Add(image);
                    PictureEcom newPictureEcom = new PictureEcom();
                    newPictureEcom.PictureId = picture.Id;
                    newPictureEcom.LazadaImage = image;
                    _context.PictureEcoms.Add(newPictureEcom);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    images.Add(pictureEcom.LazadaImage);
                }
            }
         
            LazadaUpdateProductRootRequest request = ProductToUpdateProductRequest(product, sellerSku, images);
            string jsonString = JsonConvert.SerializeObject(request);
            await Console.Out.WriteLineAsync(jsonString);
            lazopRequest.AddApiParameter("payload", jsonString);
            LazopResponse response = client.Execute(lazopRequest, accessToken);
            Console.WriteLine(response.IsError());

            JObject jObject = JObject.Parse(response.Body);
            if (!response.IsError())
            {
                baseResponse.Data = product;
                baseResponse.Code = 200;
                baseResponse.Message = "Cập nhật sản phẩm trong kho và lazada thành công";
                return baseResponse;
            }
            else
            {
                baseResponse.Code = (int)jObject["code"];
                if (jObject["detail"] != null && jObject["detail"].HasValues)
                {
                    baseResponse.Message = jObject["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jObject["message"].ToString();
                }
            }
            return baseResponse;
        }

        public async Task<BaseResponse<object>> UpdatePrice(int agencyId, string sellerSku, string price)
        {
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/product/price_quantity/update");
            request.AddApiParameter("payload", "<Request>   <Product><Skus><Sku><ItemId></ItemId><SkuId></SkuId><SellerSku>" + sellerSku + "</SellerSku><Price>" + price + "</Price><SalePrice></SalePrice><SaleStartDate></SaleStartDate><SaleEndDate></SaleEndDate><MultiWarehouseInventories><MultiWarehouseInventory><WarehouseCode></WarehouseCode><Quantity></Quantity></MultiWarehouseInventory></MultiWarehouseInventories></Sku></Skus></Product></Request>");
            LazopResponse response = client.Execute(request, accessToken);
            BaseResponse<object> baseResponse = LazopResponseToBaseResponse(response);
            Console.WriteLine("Data:" + baseResponse.Data);
            Console.WriteLine("Code:" + baseResponse.Code);
            Console.WriteLine("Message:" + baseResponse.Message);
            return baseResponse;
        }

        public async Task<BaseResponse<object>> UpdateQuantity(int agencyId, string sellerSku, string quantity)
        {
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/product/stock/sellable/update");
            request.AddApiParameter("payload", "<Request>   <Product>      <Skus>   <!--single warehouse demo-->  <Sku>         <ItemId></ItemId>         <SkuId></SkuId>         <SellerSku>" + sellerSku + "</SellerSku>                                     <SellableQuantity>" + quantity + "</SellableQuantity>    </Sku>       </Skus>   </Product> </Request>");
            LazopResponse response = client.Execute(request, accessToken);
            BaseResponse<object> baseResponse = LazopResponseToBaseResponse(response);
            Console.WriteLine("Data:" + baseResponse.Data);
            Console.WriteLine("Code:" + baseResponse.Code);
            Console.WriteLine("Message:" + baseResponse.Message);
            return baseResponse;
        }
        public async Task<BaseResponse<object>> DeleteProduct(int agencyId, List<string> sellerSkus)
        {
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/product/remove");
            string jsonString = JsonConvert.SerializeObject(sellerSkus);
            request.AddApiParameter("seller_sku_list", jsonString);
            LazopResponse response = client.Execute(request, accessToken);
            Console.WriteLine(response.IsError());
            BaseResponse<object> baseResponse = LazopResponseToBaseResponse(response);
            if (!response.IsError())
            {
                List<AgencyProductEcom> agencyProductEcoms = _context.AgencyProductEcoms.Where(a => sellerSkus.Contains(a.SellerSku)).ToList();
                if (agencyProductEcoms != null)
                {
                    agencyProductEcoms.ForEach(a => _context.AgencyProductEcoms.Remove(a));
                    await _context.SaveChangesAsync();
                }

            }
            Console.WriteLine("Data:" + baseResponse.Data);
            Console.WriteLine("Code:" + baseResponse.Code);
            Console.WriteLine("Message:" + baseResponse.Message);
            return baseResponse;
        }

        public async Task<List<CategorySuggestion>> GetCategorySuggestion(int agencyId, string productName)
        {
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/product/category/suggestion/get");
            request.SetHttpMethod("GET");
            request.AddApiParameter("product_name", productName);
            LazopResponse response = client.Execute(request, accessToken);
            if (!response.IsError())
            {
                var rs = JsonConvert.DeserializeObject<BaseLazadaReponse<DataCategorySuggestion>>(response.Body);
                if (rs != null)
                {
                    return rs.Data.CategorySuggestions;
                }
            }

            return new List<CategorySuggestion>();
        }

        public async Task<List<LazadaCategoryReponse>> GetAllCategory()
        {
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/category/tree/get");
            request.SetHttpMethod("GET");
            request.AddApiParameter("language_code", "vi_VN");
            LazopResponse response = client.Execute(request);
            if (!response.IsError())
            {
                var data = JsonConvert.DeserializeObject<BaseLazadaReponse<List<LazadaCategoryReponse>>>(response.Body);
                return data.Data;
            }
            return new List<LazadaCategoryReponse>();

        }

        public async Task<BaseResponse<OrderEcomResponse>> GetOrders(int agencyId)
        {

            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/orders/get");
            request.SetHttpMethod("GET");
            request.AddApiParameter("created_after", "2017-02-10T09:00:00+08:00");
            LazopResponse response = client.Execute(request, accessToken);
            Console.WriteLine(response.IsError());
            JObject jObject = JObject.Parse(response.Body);
            BaseResponse<OrderEcomResponse> baseResponse = new BaseResponse<OrderEcomResponse>();
            if (!response.IsError())
            {

                LazadaOrderResponse lazadaOrder = JsonConvert.DeserializeObject<LazadaOrderResponse>(jObject["data"].ToString());
                if (lazadaOrder == null)
                {
                    baseResponse.Message = "Chưa có đơn hàng nào";
                    baseResponse.Code = 200;
                    return baseResponse;
                }
                await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(lazadaOrder));
                OrderEcomResponse orderEcomResponse = LazadaOrderResponseToOrderEcom(lazadaOrder);
                baseResponse.Data = orderEcomResponse;
                baseResponse.Code = 200;
                baseResponse.Message = "Success";
                Console.WriteLine(JsonConvert.SerializeObject(baseResponse));
            }
            else
            {
                baseResponse.Code = (int)jObject["code"];
                if (jObject["detail"] != null && jObject["detail"].HasValues)
                {
                    baseResponse.Message = jObject["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jObject["message"].ToString();
                }

            }
            Console.WriteLine("Data:" + baseResponse.Data);
            Console.WriteLine("Code:" + baseResponse.Code);
            Console.WriteLine("Message:" + baseResponse.Message);
            return baseResponse;

        }

        public async Task<BaseResponse<TokenEcomResponse>> GetToken(string code, string shopId)
        {
            ILazopClient client = new LazopClient("https://auth.lazada.com/rest", appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/auth/token/create");
            request.AddApiParameter("code", code);
            LazopResponse response = client.Execute(request);
            BaseResponse<TokenEcomResponse> baseResponse = new BaseResponse<TokenEcomResponse>();
            string json = response.Body;
            JObject jObject = JObject.Parse(json);
            if (!response.IsError())
            {
                LazadaTokenResponse lazadaTokenResponse = JsonConvert.DeserializeObject<LazadaTokenResponse>(jObject.ToString());
                TokenEcomResponse tokenEcomResponse = LazadaTokenResponseToTokenEcomResponse(lazadaTokenResponse);
                var shopAppEcommerces = _context.ShopAppEcommerces.Where(s => s.ShopId == shopId);
                if (shopAppEcommerces == null)
                {
                    return null;
                }
                foreach (var shopAppEcommerce in shopAppEcommerces)
                {
                    shopAppEcommerce.Token = tokenEcomResponse.AccessToken;
                    shopAppEcommerce.RefreshToken = tokenEcomResponse.RefreshToken;
                    shopAppEcommerce.TokenExprided = DateTime.Now.AddSeconds(604800);
                    shopAppEcommerce.RefreshTokenExprided = DateTime.Now.AddSeconds(2592000);

                }
                await _context.SaveChangesAsync();
                baseResponse.Data = tokenEcomResponse;
                baseResponse.Code = 200;
                baseResponse.Message = "Success";
            }
            else
            {
                baseResponse.Code = (int)jObject["code"];
                if (jObject["detail"] != null && jObject["detail"].HasValues)
                {
                    baseResponse.Message = jObject["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jObject["message"].ToString();
                }
            }
            Console.WriteLine(response.IsError());

            Console.WriteLine("Data:" + baseResponse.Data);
            Console.WriteLine("Code:" + baseResponse.Code);
            Console.WriteLine("Message:" + baseResponse.Message);

            return baseResponse;
        }

        public async Task<BaseResponse<TokenEcomResponse>> RefreshAccessToken(int agencyId)
        {
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            string refreshToken = shopAppEcommerce.RefreshToken;
            string appKey = shopAppEcommerce.AppKey;
            string appSecret = shopAppEcommerce.AppSecret;
            ILazopClient client = new LazopClient("https://auth.lazada.com/rest", appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/auth/token/refresh");
            request.AddApiParameter("refresh_token", refreshToken);
            LazopResponse response = client.Execute(request);
            Console.WriteLine(response.IsError());
            string json = response.Body;
            JObject jObject = JObject.Parse(json);
            BaseResponse<TokenEcomResponse> baseResponse = new BaseResponse<TokenEcomResponse>();
            if (!response.IsError())
            {
                LazadaTokenResponse lazadaTokenResponse = JsonConvert.DeserializeObject<LazadaTokenResponse>(jObject.ToString());
                TokenEcomResponse tokenEcomResponse = LazadaTokenResponseToTokenEcomResponse(lazadaTokenResponse);
                shopAppEcommerce.Token = tokenEcomResponse.AccessToken;
                shopAppEcommerce.RefreshToken = tokenEcomResponse.RefreshToken;
                shopAppEcommerce.TokenExprided = DateTime.Now.AddSeconds(604800);

                await _context.SaveChangesAsync();
                baseResponse.Data = tokenEcomResponse;
                baseResponse.Code = 200;
                baseResponse.Message = "Success";

            }
            else
            {
                baseResponse.Code = (int)jObject["code"];
                if (jObject["detail"] != null && jObject["detail"].HasValues)
                {
                    baseResponse.Message = jObject["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jObject["message"].ToString();
                }
            }

            Console.WriteLine("Data:" + baseResponse.Data);
            Console.WriteLine("Code:" + baseResponse.Code);
            Console.WriteLine("Message:" + baseResponse.Message);
            return baseResponse;
        }


        public Task<string> SetStatusToCanceled(string url, string accessToken, long orderItemId)
        {
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/order/cancel");
            request.AddApiParameter("reason_detail", "Out of stock");
            request.AddApiParameter("reason_id", "15");
            request.AddApiParameter("order_item_id", "140168");
            LazopResponse response = client.Execute(request, accessToken);
            Console.WriteLine(response.IsError());
            Console.WriteLine(response.Body);
            throw new NotImplementedException();
        }

        public Task<string> SetStatusToReadyToShip(string url, string accessToken, long[] orderItemIds)
        {
            throw new NotImplementedException();
        }

        public Task<string> SetStatusToSOFFFailedDelivery(string url, string accessToken, long[] orderItemIds)
        {
            throw new NotImplementedException();
        }



        public async Task<BaseResponse<object>> UploadImage(int agencyId, FileItem fileItem)
        {

            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            string accessToken = shopAppEcommerce.Token;

            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/image/upload");
            request.AddFileParameter("image", fileItem);
            LazopResponse response = client.Execute(request, accessToken);

            Console.WriteLine(response.IsError());
            BaseResponse<object> baseResponse = LazopResponseToBaseResponse(response);
            Console.WriteLine("Data:" + baseResponse.Data);
            Console.WriteLine("Code:" + baseResponse.Code);
            Console.WriteLine("Message:" + baseResponse.Message);

            return baseResponse;
        }

        public async Task<BaseResponse<string>> MigrateImage(int agencyId, string imageUrl)
        {
            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces
           .Join(_context.AppEcommerces, sa => sa.AppId, ae => ae.Id, (sa, ae) => new { ShopAppEcommerce = sa, AppEcommerce = ae })
           .FirstOrDefault(sa => sa.ShopAppEcommerce.AgencyId == agencyId && sa.AppEcommerce.AppType == 1).ShopAppEcommerce;
            if (shopAppEcommerce == null)
            {
                return null;
            }
            checkTokenExprided(shopAppEcommerce.TokenExprided, agencyId);
            string accessToken = shopAppEcommerce.Token;
            ILazopClient client = new LazopClient(url, appKey, appSecret);
            LazopRequest request = new LazopRequest();
            request.SetApiName("/image/migrate");
            request.AddApiParameter("payload", "<?xml version=\"1.0\" encoding=\"UTF-8\" ?> <Request>     <Image>         <Url>" + imageUrl + "</Url>     </Image> </Request>");
            LazopResponse response = client.Execute(request, accessToken);

            Console.WriteLine(response.IsError());
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            JObject jObject = JObject.Parse(response.Body);

            if (!response.IsError())
            {
                baseResponse.Code = 200;
                baseResponse.Message = "Upload ảnh lên Lazada thành công";
                baseResponse.Data = jObject["data"]["image"]["url"].ToString();
            }
            else
            {
                baseResponse.Code = (int)jObject["code"];
                if (jObject["detail"] != null && jObject["detail"].HasValues)
                {
                    baseResponse.Message = jObject["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jObject["message"].ToString();
                }
            }
            return baseResponse;
        }
        public void checkTokenExprided(DateTime? tokenExprided, int agencyId)
        {
            DateTime now = DateTime.Now;
            if (tokenExprided.HasValue)
            {
                TimeSpan diff = tokenExprided.Value - now;
                TimeSpan thirtyMinutes = TimeSpan.FromMinutes(30);
                TimeSpan oneDay = TimeSpan.FromDays(1);
                if (diff > thirtyMinutes & diff < oneDay)
                {
                    RefreshAccessToken(agencyId);
                }
            }
        }
        public OrderEcomResponse LazadaOrderResponseToOrderEcom(LazadaOrderResponse lazadaOrderResponse)
        {
            List<OrderEcom> orderEcoms = new List<OrderEcom>();
            foreach (LazadaOrder item in lazadaOrderResponse.Orders)
            {
                AddressBilling addressBilling = new AddressBilling
                {
                    Country = item.AddressBilling.Country,
                    Address3 = item.AddressBilling.Address3,
                    Address2 = item.AddressBilling.Address2,
                    Address4 = item.AddressBilling.Address4,
                    Address1 = item.AddressBilling.Address1,
                    Address5 = item.AddressBilling.Address5,
                    City = item.AddressBilling.Phone,
                    Phone = item.AddressBilling.Phone,
                    PostCode = item.AddressBilling.PostCode,
                    Phone2 = item.AddressBilling.Phone2,
                    Last_name = item.AddressBilling.Last_name,
                    First_name = item.AddressBilling.Last_name,
                };
                AddressShipping addressShipping = new AddressShipping()
                {
                    Country = item.AddressShipping.Country,
                    Address1 = item.AddressShipping.Address1,
                    Address2 = item.AddressShipping.Address2,
                    Address3 = item.AddressShipping.Address3,
                    Address4 = item.AddressShipping.Address4,
                    Address5 = item.AddressShipping.Address5,
                    City = item.AddressShipping.City,
                    Phone = item.AddressShipping.Phone,
                    Phone2 = item.AddressShipping.Phone2,
                    PostCode = item.AddressShipping.PostCode,
                    FirstName = item.AddressShipping.FirstName,
                    LastName = item.AddressShipping.LastName,

                };
                OrderEcom orderEcom = new OrderEcom
                {
                    AddressShipping = addressShipping,
                    AddressBilling = addressBilling,
                    Voucher = item.Voucher,
                    WarehouseCode = item.WarehouseCode,
                    OrderNumber = item.OrderNumber,
                    CreatedAt = item.CreatedAt,
                    VoucherCode = item.VoucherCode,
                    GiftOption = item.GiftOption,
                    ShippingFee = item.ShippingFee,
                    CustomerLastName = item.CustomerLastName,
                    UpdatedAt = item.UpdatedAt,
                    PromisedShippingTimes = item.PromisedShippingTimes,
                    Price = item.Price,
                    NationalRegistrationNumber = item.NationalRegistrationNumber,
                    ShippingFeeOriginal = item.ShippingFeeOriginal,
                    PaymentMethod = item.PaymentMethod,
                    CustomerFirstName = item.CustomerFirstName,
                    ShippingFeeDiscountSeller = item.ShippingFeeDiscountSeller,
                    ShippingFeeDiscountPlatform = item.ShippingFeeDiscountPlatform,
                    BranchNumber = item.BranchNumber,
                    TaxCode = item.TaxCode,
                    ItemsCount = item.ItemsCount,
                    DeliveryInfo = item.DeliveryInfo,
                    Statuses = item.Statuses,
                    ExtraAttributes = item.ExtraAttributes,
                    GiftMessage = item.GiftMessage,
                    Remarks = item.Remarks,
                };
                orderEcoms.Add(orderEcom);
            }
            OrderEcomResponse orderEcomResponse = new OrderEcomResponse
            {
                Count = lazadaOrderResponse.Count,
                CountTotal = lazadaOrderResponse.CountTotal,
                Orders = orderEcoms
            };
            return orderEcomResponse;
        }
        public CreateProductEcom LazadaCreateProductToEcom(LazadaCreateProductResponse lazadaCreateProductResponse)
        {
            List<SkuList> skuLists = new List<SkuList>();
            foreach (LazadaSkuList s in lazadaCreateProductResponse.SkuList)
            {
                SkuList skuListItem = new SkuList
                {
                    SkuId = s.SkuId,
                    SellerSku = s.SellerSku,
                    ShopSku = s.ShopSku
                };
                skuLists.Add(skuListItem);

            }
            CreateProductEcom createProductEcom = new CreateProductEcom
            {
                ItemId = lazadaCreateProductResponse.ItemId,
                SkuList = skuLists,
            };
            return createProductEcom;
        }
        public ProductEcom LazadaProductResponseToProdcuctEcom(LazadaProductResponse response)
        {
            List<Sku> skus = new List<Sku>();
            foreach (LazadaSku sku in response.Skus)
            {
                Sku skuItem = new Sku
                {
                    Status = sku.Status,
                    Quantity = sku.Quantity,
                    SellerSku = sku.SellerSku,
                    ShopSku = sku.ShopSku,
                    Images = sku.Images,
                    Url = sku.Url,
                    PackageHeight = sku.PackageHeight,
                    PackageWidth = sku.PackageWidth,
                    PackageLength = sku.PackageLength,
                    PackageWeight = sku.PackageWeight,
                    Price = sku.Price,
                    SpecialPrice = sku.SpecialPrice,
                    SkuId = sku.SkuId,
                };
                skus.Add(skuItem);
            }

            Attributes attributes = new Attributes
            {
                ShortDescription = response.Attributes.ShortDescription,
                Description = response.Attributes.Description,
                Name = response.Attributes.Name,
                Brand = response.Attributes.Brand,
                GlasswareEverydayType = response.Attributes.GlasswareEverydayType,
                Warranty = response.Attributes.Warranty,
                WarrantyType = response.Attributes.WarrantyType,
                Hazmat = response.Attributes.Hazmat,
                NameEn = response.Attributes.NameEn,
            };
            ProductEcom productEcom = new ProductEcom
            {
                CreatedTime = response.CreatedTime,
                UpdatedTime = response.UpdatedTime,
                Images = response.Images,
                PrimaryCategory = response.PrimaryCategory,
                ItemId = response.ItemId,
                Status = response.Status,
                Skus = skus,
                Attributes = attributes,
            };
            return productEcom;
        }
        public TokenEcomResponse LazadaTokenResponseToTokenEcomResponse(LazadaTokenResponse lazadaTokenResponse)
        {
            List<CountryUserInfo> countryUserInfos = new List<CountryUserInfo>();
            foreach (LazadaCountryUserInfo countryUser in lazadaTokenResponse.CountryUserInfo)
            {
                CountryUserInfo countryUserInfo = new CountryUserInfo
                {
                    Country = countryUser.Country,
                    SellerId = countryUser.SellerId,
                    ShortCode = countryUser.ShortCode,
                    UserId = countryUser.UserId
                };
                countryUserInfos.Add(countryUserInfo);
            }

            TokenEcomResponse tokenEcomResponse = new TokenEcomResponse
            {
                AccessToken = lazadaTokenResponse.AccessToken,
                Country = lazadaTokenResponse.Country,
                RefreshToken = lazadaTokenResponse.RefreshToken,
                AccountId = lazadaTokenResponse.AccountId,
                Code = lazadaTokenResponse.Code,
                Account = lazadaTokenResponse.Account,
                AccountPlatform = lazadaTokenResponse.AccountPlatform,
                RefreshExpiresIn = lazadaTokenResponse.RefreshExpiresIn,
                ExpiresIn = lazadaTokenResponse.ExpiresIn,
                Request_id = lazadaTokenResponse.Request_id,
                CountryUserInfo = countryUserInfos,
            };
            return tokenEcomResponse;
        }
        public BaseResponse<object> LazopResponseToBaseResponse(LazopResponse response)
        {
            JObject jsonResponse = JObject.Parse(response.Body);
            BaseResponse<object> baseResponse = new BaseResponse<object>();
            if (!response.IsError())
            {
                if (jsonResponse["data"] != null && jsonResponse["data"].HasValues)
                {
                    baseResponse.Data = jsonResponse["data"];
                }
                if (jsonResponse["detail"] != null && jsonResponse["detail"].HasValues)
                {
                    baseResponse.Message = jsonResponse["detail"][0]["message"].ToString();
                }
                else if (jsonResponse["message"] != null && jsonResponse["message"].HasValues)
                {
                    baseResponse.Message = jsonResponse["message"].ToString();
                }
                else
                {
                    baseResponse.Message = "Success";
                }
                baseResponse.Code = 200;

            }
            else
            {
                baseResponse.Code = jsonResponse["code"].ToObject<int>();

                if (jsonResponse["detail"] != null && jsonResponse["detail"].HasValues)
                {
                    baseResponse.Message = jsonResponse["detail"][0]["message"].ToString();
                }
                else
                {
                    baseResponse.Message = jsonResponse["message"].ToString();
                }
            }

            return baseResponse;
        }
        public LazadaUpdateProductRootRequest ProductToUpdateProductRequest(Product product, string sellerSku, List<string> image)
        {
            LazadaUpdateProductRequest request = new LazadaUpdateProductRequest();
            LazadaUpdateProductImages images = new LazadaUpdateProductImages()
            {
                Image = image
            };
            LazadaUpdateProductAttributes attributes = new LazadaUpdateProductAttributes()
            {
                Name = product.Name,
                ShortDescription = ""
            };
            List<LazadaUpdateProductSku> sku = new List<LazadaUpdateProductSku>();
            LazadaUpdateProductSku skuItem = new LazadaUpdateProductSku()
            {
                SellerSku = sellerSku,
                Quantity = product.QuantityDay.ToString(),
                Price = product.Price.ToString(),
                SpecialPrice = "",
                SpecialFromDate = "",
                SpecialToDate = "",
                PackageHeight = "10",
                PackageLength = "10",
                PackageWidth = "10",
                PackageWeight = "10",
                Status = "active",
                Images = images,
            };
            sku.Add(skuItem);
            LazadaUpdateProductSkus skus = new LazadaUpdateProductSkus()
            {
                Sku = sku,
            };

            LazadaUpdateProduct productRequest = new LazadaUpdateProduct()
            {
                PrimaryCategory = "10003301",
                AssociatedSku = sellerSku,
                Attributes = attributes,
                Skus = skus

            };

            LazadaUpdateProductRequest updateProductRequest = new LazadaUpdateProductRequest()
            {
                Product = productRequest,
            };
            LazadaUpdateProductRootRequest lazadaUpdateProductRootRequest = new LazadaUpdateProductRootRequest()
            {
                Request = updateProductRequest,
            };
            return lazadaUpdateProductRootRequest;
        }

        public LazadaProductRequest ProductToProductRequest(Product product, List<string> image)
        {
            LazadaProductRequest productRequest = new LazadaProductRequest();
            LazadaCreateProductAttributes attributes = new LazadaCreateProductAttributes();
            attributes.Name = product.Name;
            ProductDetail productDetail = _context.ProductDetails
                                         .FirstOrDefault(pd => pd.ProductId == product.Id);

            if (productDetail == null)
            {
                return null;
            }
            if (product.Description != null)
            { attributes.Description = product.Description; }
            else { attributes.Description = ""; }

            attributes.Brand = "No Brand";
            attributes.Model = "Không";
            attributes.Warranty_type = "No warranty";
            attributes.Warranty = "1 Month";
            attributes.Hazmat = "None";
            attributes.Material = "None";
            attributes.Delivery_option_sof = "No";

            LazadaCreatProductImages images = new LazadaCreatProductImages(image);

            List<LazadaCreateProductSku> sku = new List<LazadaCreateProductSku>();
            LazadaCreateProductSku skuItem = new LazadaCreateProductSku();
            skuItem.SellerSku = productDetail.CodeSku;
            skuItem.Quantity = product.QuantityDay.ToString();
            skuItem.Price = product.Price.ToString();
            skuItem.Special_price = "";
            skuItem.Special_from_date = "";
            skuItem.Special_to_date = "";
            skuItem.Package_height = "10";
            skuItem.Package_length = "10";
            skuItem.Package_width = "10";
            skuItem.Package_weight = "0.5";
            skuItem.Images = images;

            sku.Add(skuItem);
            CreateProductSkus skus = new CreateProductSkus(sku);
            LazadaCreateProduct createProduct = new LazadaCreateProduct("10003301", images, attributes, skus);
            LazadaRequest request = new LazadaRequest(createProduct);
            LazadaProductRequest createProductRequest = new LazadaProductRequest(request);
            Console.WriteLine(JsonConvert.SerializeObject(createProductRequest));
            return createProductRequest;
        }


    }
}
