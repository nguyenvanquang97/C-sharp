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
using Wini.Simple.Storage;
using Wini.Utils;

namespace Wini.DA
{

    public interface IDebtDa
    {


        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
        Task<BaseResponse<IList<DebtItem>>> GetListSimpleByRequest(BaseRequest request);

        Debt GetbyId(int id);
        DebtItem GetbyIdItem(int id);
        //BaseResponse<Debt> Update(Debt item);

        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(Debt item);
        void Add(Debt category);
    }

    public class DebtDa : IDebtDa
    {
        readonly ApplicationDbContext _context;


        public DebtDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<DebtItem>>> GetListSimpleByRequest(BaseRequest request)
        {
            var source = from o in _context.Debts
                         orderby o.Id descending
                         select new DebtItem
                         {
                             Id = o.Id,
                             Price = o.Price,
                             Payment = o.Payment,
                             //DateCreate = o.DateCreate,
                             AgencyId = o.AgencyId,
                             Note = o.Note,

                         };
            //request.LoadOptions.TotalSummary = new SummaryInfo[] { new SummaryInfo() { Selector = "Price", SummaryType = "sum" } };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<DebtItem>> response = new BaseResponse<IList<DebtItem>>();
            response.Data = query.data.Cast<DebtItem>().ToList();
            response.TotalCount = query.totalCount;
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }

        public DebtItem GetbyIdItem(int id)
        {

            var source = from o in _context.Debts
                             //where (!o.IsDelete.HasValue || o.IsDelete == false)
                             where o.Id == id
                         orderby o.Id descending
                         select new DebtItem
                         {
                             Id = o.Id,
                             Price = o.Price,
                             Payment = o.Payment,
                             DateCreate = o.DateCreate,
                             AgencyId = o.AgencyId,
                             Note = o.Note,
                             DebtDetailItems = o.DebtDetails.Select(c => new DebtDetailItem
                             {

                                 Price = c.Price,
                             })
                         };
            return source.FirstOrDefault();
        }
        
        public Debt GetbyId(int id)
        {
            var query = from o in _context.Debts
                        where o.Id == id
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        //public BaseResponse<Debt> Update(Debt item)
        //{
        //    var model = GetbyId(item.Id);
        //    if (model != null)
        //    {
        //        Save();
        //        return new BaseResponse<Debt>() { Data = model, Code = (int)ResponseCode.Success, Message = "Thêm mới dữ liệu thành công" };
        //    }
        //    return new BaseResponse<Debt>() { Data = null, Code = (int)ResponseCode.Nodata, Message = "Dữ liệu không tồn tại" };
        //}
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(Debt item)
        {
            _context.Debts.Remove(item);
        }
        public void Add(Debt item)
        {
            _context.Debts.Add(item);
        }

    }
}
