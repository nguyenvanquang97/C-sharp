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

    public interface IAgencyTypeDa
    {


        /// <summary>
        /// Gets all AgencyType.
        /// </summary>
        /// <returns>danh sách AgencyType.</returns>
        Task<BaseResponse<IList<AgencyTypeItem>>> GetListSimpleByRequest(BaseRequest request);
        AgencyType GetbyId(int id);
        BaseResponse<AgencyType> Update(AgencyType item);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(AgencyType item);
        void Add(AgencyType AgencyType);
    }

    public class AgencyTypeDa : IAgencyTypeDa
    {
        readonly ApplicationDbContext _context;


        public AgencyTypeDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<AgencyTypeItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.AgencyTypes
                         where (!o.IsDeleted.HasValue || o.IsDeleted == false) && o.IsShow == true
                         orderby o.Id descending
                         select new AgencyTypeItem
                         {
                             Id = o.Id,
                             Name = o.Name,
                             Description = o.Description,
                             Discount = o.Discount,
                             PriceDebt = o.PriceDebt,
                         };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<AgencyTypeItem>> response = new BaseResponse<IList<AgencyTypeItem>>();
            response.Data = query.data.Cast<AgencyTypeItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return response;
        }
        public AgencyType GetbyId(int id)
        {

            var query = from o in _context.AgencyTypes
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<AgencyType> Update(AgencyType item)
        {
            var model = GetbyId(item.Id);
            if (model != null)
            {
                Save();
                return BasiResponse.Success(model);
            }
            return BasiResponse.Nodata(new AgencyType());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(AgencyType item)
        {
            _context.AgencyTypes.Remove(item);
        }
        public void Add(AgencyType item)
        {
            _context.AgencyTypes.Add(item);
        }
    }
}
