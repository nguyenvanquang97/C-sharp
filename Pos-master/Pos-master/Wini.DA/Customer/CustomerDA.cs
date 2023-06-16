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

    public interface ICustomerDa
    {


        /// <summary>
        /// Gets all Customer.
        /// </summary>
        /// <returns>danh sách Customer.</returns>
        Task<BaseResponse<IList<CustomerItem>>> GetListSimpleByRequest(BaseRequest request,int agencyId);
        BaseResponse<CustomerItem> GetbyCustomerItem(int id);
        Customer GetbyId(int id);
        
        
        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Customer item);
        void Add(Customer Customer);
    }

    public class CustomerDA : ICustomerDa
    {
        readonly ApplicationDbContext _context;


        public CustomerDA(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<CustomerItem>>> GetListSimpleByRequest(BaseRequest request, int agencyId)
        {

            var source = from o in _context.Customers
                         where (!o.IsDelete.HasValue || o.IsDelete == false)  &&
                         (agencyId == 0 || o.AgencyId == agencyId)
                         orderby o.ID descending
                         select new CustomerItem
                         {
                             ID = o.ID,
                             Phone = o.Phone,
                             Address = o.Address,
                             FullName = o.FullName,
                             Email = o.Email,
                             PassWord = o.PassWord,
                             AvatarUrl = o.AvatarUrl,
                             Birthday = o.Birthday,
                             Gender = o.Gender,
                             STK = o. STK,
                             BankName = o.BankName,
                             Branchname = o.Branchname,
                         };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<CustomerItem>> response = new BaseResponse<IList<CustomerItem>>();
            response.Data = query.data.Cast<CustomerItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }

            response.Code = (int)ResponseCode.Success;
            return BasiResponse.Success(response.Data, "Lẫy dữ liệu thành công", response.TotalCount);
        }
        public BaseResponse<CustomerItem> GetbyCustomerItem( int Id)
        {

            var source = from o in _context.Customers
                where (!o.IsDelete.HasValue || o.IsDelete == false) &&
                    o.ID == Id
                orderby o.ID descending
                select new CustomerItem
                {
                    ID = o.ID,
                    Phone = o.Phone,
                    Address = o.Address,
                    FullName = o.FullName,
                    Email = o.Email,
                    PassWord = o.PassWord,
                    AvatarUrl = o.AvatarUrl,
                    Birthday = o.Birthday,
                    Gender = o.Gender,
                    STK = o.STK,
                    BankName = o.BankName,
                    Branchname = o.Branchname,
                };
          
            var data = source.FirstOrDefault();
            return BasiResponse.Success(data);

        }
        public Customer GetbyId(int id)
        {
            var query = from o in _context.Customers
                        where o.ID == id
                        orderby o.ID descending
                        select o;
            return query.FirstOrDefault();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Customer item)
        {
            _context.Customers.Remove(item);
        }
        public void Add(Customer item)
        {
            _context.Customers.Add(item);
        }

    
    }
}
