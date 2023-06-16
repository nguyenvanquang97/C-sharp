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

    public interface ICategoryDa
    {


        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
        Task<BaseResponse<IList<CategoryItem>>> GetListSimpleByRequest(BaseRequest request);
        Category GetbyId(int id);
        BaseResponse<Category> Update(Category item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Category item);
        void Add(Category category);
    }

    public class CategoryDa : ICategoryDa
    {
        readonly ApplicationDbContext _context;


        public CategoryDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<CategoryItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Categories
                         where (!o.IsDeleted.HasValue || o.IsDeleted == false) && o.IsShow == true
                         orderby o.Id descending
                         select new CategoryItem
                         {
                             Id = o.Id,
                             Name = o.Name,
                             Slug = o.Slug,
                             CreatedDate = o.CreatedDate,
                         };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<CategoryItem>> response = new BaseResponse<IList<CategoryItem>>();
            response.Data = query.data.Cast<CategoryItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public Category GetbyId(int id)
        {

            var query = from o in _context.Categories
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<Category> Update(Category item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                model.Slug = FomatString.Slug(item.Name);
                model.Description = HttpUtility.UrlDecode(item.Description);
                Save();
                return BasiResponse.Success(item);

            }
            return BasiResponse.Error(new Category());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Category item)
        {
            _context.Categories.Remove(item);
        }
        public void Add(Category item)
        {
            _context.Categories.Add(item);
        }
    }
}
