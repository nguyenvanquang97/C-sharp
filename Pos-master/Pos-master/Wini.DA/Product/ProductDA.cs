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
    
     public interface IProductDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<ProductItem>>> GetListSimpleByRequest(BaseRequest request);
        Product GetbyId(int id);
       BaseResponse<Product> Update(Product item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Product item);
        void Add(Product category);
    }

    public class ProductDa : IProductDa
    {
        readonly ApplicationDbContext _context;


        public ProductDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<ProductItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Products
                        where (!o.IsDelete.HasValue || o.IsDelete == false) && o.IsShow == true
                        orderby o.Id descending
                        select new ProductItem
                        {
                            Id = o.Id,
                            Name = o.Name,
                            NameAscii = o.NameAscii,
                            Description = o.Description,
                            Price = o.Price,
                            UrlPicture = o.Picture.Folder + o.Picture.Url,
                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<ProductItem>> response = new BaseResponse<IList<ProductItem>>();
            response.Data = query.data.Cast<ProductItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public Product GetbyId(int id)
        {

            var query = from o in _context.Products
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<Product> Update(Product item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                Save();
                return BasiResponse.Success(item);

            }
            return BasiResponse.Error(new Product());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Product item)
        {
            _context.Products.Remove(item);
        }
        public void Add(Product item)
        {
            _context.Products.Add(item);
        }
        
    }
}
