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

    public interface IBrandDa
    {


        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
        Task<BaseResponse<IList<BrandItem>>> GetListSimpleByRequest(BaseRequest request);
        Brand GetbyId(int id);
        BaseResponse<Brand> Update(Brand item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Brand item);
        void Add(Brand category);
    }

    public class BrandDa : IBrandDa
    {
        readonly ApplicationDbContext _context;


        public BrandDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<BrandItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Brands
                         where (!o.IsDeleted.HasValue || o.IsDeleted == false) && o.IsShow == true
                         orderby o.Id descending
                         select new BrandItem
                         {
                             Id = o.Id,
                             Name = o.Name,

                         };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<BrandItem>> response = new BaseResponse<IList<BrandItem>>();
            response.Data = query.data.Cast<BrandItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public Brand GetbyId(int id)
        {

            var query = from o in _context.Brands
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<Brand> Update(Brand item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                Save();
                return BasiResponse.Success(item);
            }
            return BasiResponse.Error(new Brand());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Brand item)
        {
            _context.Brands.Remove(item);
        }
        public void Add(Brand item)
        {
            _context.Brands.Add(item);
        }
    }
}
