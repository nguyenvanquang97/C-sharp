using Lazop.Api.Util;
using da = Wini.Database;
using Wini.SaleMultipleChannel.Lazop;
using Newtonsoft.Json.Linq;
using Wini.Database.Multipe_Channel;
using Newtonsoft.Json;
using Wini.SaleMultipleChannel.CreateProductRequest;
using System.Xml.Serialization;
using Wini.Simple;
using Wini.SaleMultipleChannel.Model.CreateProduct;
using Wini.Database;
using Wini.SellMultipleChanel.Model;
using Wini.SaleMultipleChannel.Model.Order;
using Wini.SaleMultipleChannel.Model.Token;

namespace Wini.SaleMultipleChannel
{
    public class SaleShopee : ISale
    {
        public Task<BaseResponse<CreateProductEcom>> CreateProduct(int agencyId, Product product)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<object>> DeleteProduct(int agencyId, List<string> sellerSkus)
        {
            throw new NotImplementedException();
        }

        public Task<List<LazadaCategoryReponse>> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public BaseResponse<List<Model.Product.SaleLazada.ProductEcom>> GetAllProductAsynchronous(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<Product>>> GetAllProductSynced(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategorySuggestion>> GetCategorySuggestion(int agencyId, string productName)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<OrderEcomResponse>> GetOrders(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TokenEcomResponse>> GetToken(string code, string shopId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<string>> MigrateImage(int agencyId, string url)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<TokenEcomResponse>> RefreshAccessToken(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Task<string> SetStatusToCanceled(string url, string accessToken, long orderItemId)
        {
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

        public Task<BaseResponse<List<Model.Product.SaleLazada.ProductEcom>>> SyncProductFromEcom(int agencyId, List<string> sellerSkus)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<object>> UpdatePrice(int agencyId, string sellerSku, string price)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<Product>> UpdateProduct(int agencyId, ProductDetail productDetail, string sellerSku)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<object>> UpdateQuantity(int agencyId, string sellerSku, string quantity)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<object>> UploadImage(int agencyId, FileItem fileItem)
        {
            throw new NotImplementedException();
        }
    }
    //    {
    //        readonly da.ApplicationDbContext _context;
    //        private string appKey = "117002";
    //        private string appSecret = "yNXHZs88Pj36kEZ6xxhUBFuROOIY23Ui";
    //        private string url = "https://api.lazada.vn/rest";
    //        public SaleShopee(da.ApplicationDbContext context)
    //        {
    //            _context = context;
    //            //this.appKey = appKey;
    //            //this.appSecret = appSecret;
    //        }


    //        public async Task<LazopResponse> CreateProduct(string userId, string shopId, ProductRequest root)
    //        {

    //            Guid id = new Guid(userId);
    //            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces.FirstOrDefault(s => s.ShopId == shopId && s.UserId == id);
    //            if (shopAppEcommerce == null)
    //            {
    //                return null;
    //            }
    //            string accessToken = shopAppEcommerce.Token;
    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest();
    //            request.SetApiName("/product/create");
    //            string jsonString = JsonConvert.SerializeObject(root);
    //            request.AddApiParameter("payload", jsonString);

    //            Console.WriteLine(jsonString);
    //            LazopResponse response = client.Execute(request, accessToken);
    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);



    //            return response;
    //        }

    //        public async Task<LazopResponse> UpdateProduct(string userId, string shopId, Wini.SaleMultipleChannel.UpdateProductRequest.Request request)
    //        {
    //            // return BaseResponse<int>
    //            Guid id = new Guid(userId);
    //            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces.FirstOrDefault(s => s.ShopId == shopId && s.UserId == id);
    //            if (shopAppEcommerce == null)
    //            {
    //                return null;
    //            }
    //            string accessToken = shopAppEcommerce.Token;
    //            //Console.WriteLine("ISale");
    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest lazopRequest = new LazopRequest();
    //            lazopRequest.SetApiName("/product/update");



    //            XmlSerializer serializer = new XmlSerializer(typeof(Wini.SaleMultipleChannel.UpdateProductRequest.Request), new XmlAttributeOverrides());
    //            using (StringWriter writer = new StringWriter())
    //            {
    //                serializer.Serialize(writer, request);
    //                lazopRequest.AddApiParameter("payload", writer.ToString());
    //            }

    //            LazopResponse response = client.Execute(lazopRequest, accessToken);
    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);
    //            return response;
    //        }
    //        public async Task<LazopResponse> GetCategorySuggestion(string userId, string shopId, string productName)
    //        {

    //            Guid id = new Guid(userId);
    //            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces.FirstOrDefault(s => s.ShopId == shopId && s.UserId == id);
    //            if (shopAppEcommerce == null)
    //            {
    //                return null;
    //            }
    //            string accessToken = shopAppEcommerce.Token;
    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest();
    //            request.SetApiName("/product/category/suggestion/get");
    //            request.SetHttpMethod("GET");
    //            request.AddApiParameter("product_name", productName);
    //            LazopResponse response = client.Execute(request, accessToken);
    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);

    //            return response;
    //        }
    //        public void GetAllCategory(string accessToken)
    //        {
    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest();
    //            request.SetApiName("/category/tree/get");
    //            request.SetHttpMethod("GET");
    //            request.AddApiParameter("language_code", "vi_VN");
    //            LazopResponse response = client.Execute(request, accessToken);
    //            var d = 1;

    //        }

    //        public async Task<LazopResponse> GetOrders(string userId,string shopId)
    //        {
    //            Guid id = new Guid(userId);
    //            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces.FirstOrDefault(s => s.ShopId == shopId && s.UserId == id);
    //            if (shopAppEcommerce == null)
    //            {
    //                return null;
    //            }
    //            string accessToken = shopAppEcommerce.Token;
    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest();
    //            request.SetApiName("/orders/get");
    //            request.SetHttpMethod("GET");
    //            request.AddApiParameter("created_after", "2017-02-10T09:00:00+08:00");
    //            LazopResponse response = client.Execute(request, accessToken);
    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);


    //            return response;
    //        }

    //        public async Task<LazopResponse> GetProducts( string userId, string shopId)
    //        {
    //            Guid id =new Guid(userId);
    //            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces.FirstOrDefault(s => s.ShopId == shopId && s.UserId==id);
    //            if (shopAppEcommerce == null)
    //            {
    //                return null;
    //            }
    //            string accessToken = shopAppEcommerce.Token;
    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest();
    //            request.SetApiName("/products/get");
    //            request.SetHttpMethod("GET");
    //            LazopResponse response = client.Execute(request, accessToken);


    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);

    //            return (response);
    //        }

    //        public async Task<LazopResponse> GetToken(string url, string code, string shopId)
    //        {
    //            // config done table AppEcommerce
    //            // done ShopAppEcommerce

    //            // TODO: 
    //            // getToken(String url, string code,int shopId)

    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest();
    //            request.SetApiName("/auth/token/create");
    //            request.AddApiParameter("code", code);
    //            LazopResponse response = client.Execute(request);


    //            //step1: lấy ShopAppEcommerce by shopid
    //            // step2 update ShopAppEcommerce token, refreshtoke, exprided token,

    //            if (response.IsError() ==false) {
    //                string json = response.Body;
    //                JObject jsonResponse = JObject.Parse(json);

    //                var shopAppEcommerces = _context.ShopAppEcommerces.Where(s => s.ShopId == shopId);
    //                if (shopAppEcommerces == null)
    //                {
    //                    return null;
    //                }
    //                foreach (var shopAppEcommerce in shopAppEcommerces)
    //                {
    //                    shopAppEcommerce.Token = jsonResponse.GetValue("access_token").ToString();
    //                    shopAppEcommerce.RefreshToken = jsonResponse.GetValue("refresh_token").ToString();
    //                    shopAppEcommerce.TokenExprided = DateTime.Now.AddSeconds(604800);
    //                    shopAppEcommerce.RefreshTokenExprided = DateTime.Now.AddSeconds(2592000);

    //                }
    //                await _context.SaveChangesAsync();
    //            }



    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);
    //            return response;
    //        }



    //        public Task<string> SetStatusToCanceled(string url, string accessToken, long orderItemId)
    //        {
    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest();
    //            request.SetApiName("/order/cancel");
    //            request.AddApiParameter("reason_detail", "Out of stock");
    //            request.AddApiParameter("reason_id", "15");
    //            request.AddApiParameter("order_item_id", "140168");
    //            LazopResponse response = client.Execute(request, accessToken);
    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);
    //            throw new NotImplementedException();
    //        }

    //        public Task<string> SetStatusToReadyToShip(string url, string accessToken, long[] orderItemIds)
    //        {
    //            throw new NotImplementedException();
    //        }

    //        public Task<string> SetStatusToSOFFFailedDelivery(string url, string accessToken, long[] orderItemIds)
    //        {
    //            throw new NotImplementedException();
    //        }



    //        public string UploadImage( FileItem fileItem,string userId,string shopId)
    //        {
    //            Guid id = new Guid(userId);
    //            ShopAppEcommerce shopAppEcommerce = _context.ShopAppEcommerces.FirstOrDefault(s => s.ShopId == shopId && s.UserId == id);
    //            if (shopAppEcommerce == null)
    //            {
    //                return null;
    //            }
    //            string accessToken = shopAppEcommerce.Token;

    //            ILazopClient client = new LazopClient(url, appKey, appSecret);
    //            LazopRequest request = new LazopRequest(); 
    //            request.SetApiName("/image/upload"); 
    //            request.AddFileParameter("image", fileItem);
    //            LazopResponse response = client.Execute(request, accessToken); 
    //            Console.WriteLine(response.IsError());
    //            Console.WriteLine(response.Body);
    //            return response.Body;
    //        }


}
}