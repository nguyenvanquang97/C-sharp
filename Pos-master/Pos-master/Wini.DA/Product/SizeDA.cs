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
    
     public interface ISizeDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<SizeItem>>> GetListSimpleByRequest(BaseRequest request);
        Size GetbyId(int id);
       BaseResponse<Size> Update(Size item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Size item);
        void Add(Size category);
    }

    public class SizeDa : ISizeDa
    {
        readonly ApplicationDbContext _context;


        public SizeDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<SizeItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Sizes
                        where (!o.IsDeleted.HasValue || o.IsDeleted == false) && o.IsShow == true
                        orderby o.Id descending
                        select new SizeItem
                        {
                            Id = o.Id,
                            Name = o.Name,
                            Unitname = o.Unit.Name,
                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<SizeItem>> response = new BaseResponse<IList<SizeItem>>();
            response.Data = query.data.Cast<SizeItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public Size GetbyId(int id)
        {

            var query = from o in _context.Sizes
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<Size> Update(Size item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                Save();
                return BasiResponse.Success(item);

            }
            return BasiResponse.Error(new Size());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Size item)
        {
            _context.Sizes.Remove(item);
        }
        public void Add(Size item)
        {
            _context.Sizes.Add(item);
        }
    }
}
