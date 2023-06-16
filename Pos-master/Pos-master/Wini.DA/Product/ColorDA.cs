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
    
     public interface IColorDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<ColorItem>>> GetListSimpleByRequest(BaseRequest request);
        Color GetbyId(int id);
       BaseResponse<Color> Update(Color item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Color item);
        void Add(Color category);
    }

    public class ColorDa : IColorDa
    {
        readonly ApplicationDbContext _context;


        public ColorDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<ColorItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Colors
                        where (!o.IsDeleted.HasValue || o.IsDeleted == false) && o.IsShow == true
                        orderby o.Id descending
                        select new ColorItem
                        {
                            Id = o.Id,
                            Name = o.Name,

                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<ColorItem>> response = new BaseResponse<IList<ColorItem>>();
            response.Data = query.data.Cast<ColorItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public Color GetbyId(int id)
        {

            var query = from o in _context.Colors
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<Color> Update(Color item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                model.Description = HttpUtility.UrlDecode(item.Description);
                Save();
                return BasiResponse.Success(item);
            }
            return BasiResponse.Error(new Color());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Color item)
        {
            _context.Colors.Remove(item);
        }
        public void Add(Color item)
        {
            _context.Colors.Add(item);
        }
    }
}
