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

    public interface IOrderDa
    {


        /// <summary>
        /// Gets all Order.
        /// </summary>
        /// <returns>danh sách Order.</returns>
        Task<BaseResponse<IList<OrderItem>>> GetListSimpleByRequest(BaseRequest request,int agencyId);
        Order GetbyId(int id);
        OrderItem getOrderbyId(int id);
        
        /// <summary>
        /// save.
        /// </summary>
        void Save();
    }

    public class OrderDa : IOrderDa
    {
        readonly ApplicationDbContext _context;


        public OrderDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<OrderItem>>> GetListSimpleByRequest(BaseRequest request,int agencyId)
        {

            var source = from o in _context.Orders
                         where (!o.IsDelete.HasValue || o.IsDelete == false)  &&
                         (agencyId == 0 || o.AgencyId == agencyId)
                         orderby o.Id descending
                         select new OrderItem
                         {
                             Id = o.Id,
                             DateCreated = o.DateCreated,
                             CustomerName = o.CustomerName,
                             Mobile = o.Mobile,
                             Note = o.Note,
                             TotalPrice = o.TotalPrice,
                             SalePercent = o.SalePercent,
                             Discount = o.Discount,
                         };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<OrderItem>> response = new BaseResponse<IList<OrderItem>>();
            response.Data = query.data.Cast<OrderItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data,"Lấy dữ liệu thành công",response.TotalCount);
        }
        public  OrderItem getOrderbyId(int id)
        {

            var source = from o in _context.Orders
                where o.Id == id
                orderby o.Id descending
                select new OrderItem
                {
                    Id = o.Id,
                    DateCreated = o.DateCreated,
                    CustomerName = o.CustomerName,
                    Mobile = o.Mobile,
                    Note = o.Note,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    SalePercent = o.SalePercent,
                    Discount = o.Discount,
                    OrderDetailItems = o.OrderDetails.Where(a=>a.IsDelete == false).Select(c=> new OrderDetailItem
                    {
                        Quantity = c.Quantity,
                        Price = c.PriceReceipt,
                        Discount = c.Discount,
                        ProductName = c.ProductDetail.Name,
                        TotalPrice = c.Quantity * c.PriceReceipt,
                        Status = c.Status,
                    })
                };
            
            return source.FirstOrDefault();
        }

        
        public Order GetbyId(int id)
        {

            var query = from o in _context.Orders
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
        
    }
}
