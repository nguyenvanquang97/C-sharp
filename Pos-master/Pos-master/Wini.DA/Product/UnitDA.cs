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
    
     public interface IUnitDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<UnitItem>>> GetListSimpleByRequest(BaseRequest request);
        Unit GetbyId(int id);
       BaseResponse<Unit> Update(Unit item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Unit item);
        void Add(Unit category);
    }

    public class UnitDa : IUnitDa
    {
        readonly ApplicationDbContext _context;


        public UnitDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<UnitItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Units
                        where (!o.IsDeleted.HasValue || o.IsDeleted == false) && o.IsShow == true
                        orderby o.Id descending
                        select new UnitItem
                        {
                            Id = o.Id,
                            Name = o.Name,

                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<UnitItem>> response = new BaseResponse<IList<UnitItem>>();
            response.Data = query.data.Cast<UnitItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public Unit GetbyId(int id)
        {

            var query = from o in _context.Units
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<Unit> Update(Unit item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                model.Name = HttpUtility.UrlDecode(item.Name);
                Save();
                return BasiResponse.Success(item);

            }
            return BasiResponse.Error(new Unit());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Unit item)
        {
            _context.Units.Remove(item);
        }
        public void Add(Unit item)
        {
            _context.Units.Add(item);
        }
    }
}
