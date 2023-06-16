using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DevExtreme.AspNet.Data;
using Pos;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;

namespace Wini.DA
{
    
     public interface IProductDetailDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<ProductDetailItem>>> GetListSimpleByRequest(BaseRequest request);
        ProductDetail GetbyId(int id);
       BaseResponse<ProductDetail> Update(ProductDetail item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(ProductDetail item);
        void Add(ProductDetail category);
        void AddPic(ProductDetailPicture category);
    }


    
    public class ProductDetailDa : IProductDetailDa
    {
        readonly ApplicationDbContext _context;


        public ProductDetailDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<ProductDetailItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.ProductDetails
                        where (!o.IsDelete.HasValue || o.IsDelete == false) && o.IsShow == true
                        orderby o.Id descending
                        select new ProductDetailItem
                        {
                            Id = o.Id,
                            Name = o.Name,
                            NameAscii = o.NameAscii,
                            Description = o.Description,
                            CodeSku = o.CodeSku,
                            SizeName = o.Size.Name,
                            ColorName = o.Color.Name,
                            UrlPicture = o.Picture.Folder + o.Picture.Url,
                            LstPictures = o.Pictures.Select(c=> new PictureItem
                            {
                                Name = c.Picture.Name,
                                Url = c.Picture.Url,
                                Folder = c.Picture.Folder,
                            })

                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<ProductDetailItem>> response = new BaseResponse<IList<ProductDetailItem>>();
            response.Data = query.data.Cast<ProductDetailItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public ProductDetail GetbyId(int id)
        {

            var query = from o in _context.ProductDetails
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<ProductDetail> Update(ProductDetail item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                item.Pictures.Clear();
                if (!string.IsNullOrEmpty(item.LstPicture))
                {
                    var lstInt = FdiUtils.StringToListInt(item.LstPicture);
                    foreach (var items in lstInt)
                    {
                        var pic = new ProductDetailPicture
                        {
                            PictureId = items,
                            ProductId = item.Id,
                            Sort = 0
                        };
                        item.Pictures.Add(pic);
                    }
                }
                Save();
                return BasiResponse.Success(item);

            }
            return BasiResponse.Error(new ProductDetail());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(ProductDetail item)
        {
            _context.ProductDetails.Remove(item);
        }
        public void Add(ProductDetail item)
        {
            _context.ProductDetails.Add(item);
        }
        public void AddPic(ProductDetailPicture item)
        {
            _context.ProductDetailPictures.Add(item);
        }
    }
}
