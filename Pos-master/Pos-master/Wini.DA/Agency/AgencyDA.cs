using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DevExtreme.AspNet.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pos;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;

namespace Wini.DA
{

    public interface IAgencyDa
    {


        /// <summary>
        /// Gets all Agency.
        /// </summary>
        /// <returns>danh sách Agency.</returns>
        Task<BaseResponse<IList<AgencyItem>>> GetListSimpleByRequest(BaseRequest request);
        Agency GetbyId(int id);
        
        Task InsertDNModule(int? groupId, int id, bool isDelete);
        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Agency item);
        void Add(Agency Agency);
    }

    public class AgencyDa : IAgencyDa
    {
        readonly ApplicationDbContext _context;


        public AgencyDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<AgencyItem>>> GetListSimpleByRequest(BaseRequest request)
        {

            var source = from o in _context.Agencies
                         where (!o.IsDelete.HasValue || o.IsDelete == false) && o.IsShow == true
                         orderby o.Id descending
                         select new AgencyItem
                         {
                             Id = o.Id,
                             Name = o.Name,
                             Phone = o.Phone,
                             Address = o.Address,
                             CreateDate = o.CreateDate,
                             FullName = o.FullName,
                             Email = o.Email,
                             TypeName = o.AgencyType.Name,

                         };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<AgencyItem>> response = new BaseResponse<IList<AgencyItem>>();
            response.Data = query.data.Cast<AgencyItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return response;
        }
        public Agency GetbyId(int id)
        {

            var query = from o in _context.Agencies
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        
        public Task  InsertDNModule(int? groupId, int id, bool isDelete)
        {
            var p1 = new SqlParameter("@GroupId", groupId);
            var p2 = new SqlParameter("@Agencyid", id);
            var p3 = new SqlParameter("@IsDelete", isDelete);
            _context.Set<StaticOrder>().FromSqlRaw("EXECUTE InsertDNModule", p1, p2, p3);
            return Task.CompletedTask;
        }

        
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Agency item)
        {
            _context.Agencies.Remove(item);
        }
        public void Add(Agency item)
        {
            _context.Agencies.Add(item);
        }

    
    }
}
