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
    
     public interface IExportProductDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<ExportProductItem>>> GetListSimpleByRequest(BaseRequest request,int agencyId);
        ExportProduct GetbyId(int id, int agencyId);
        ExportProductItem GetbyIdItem(int id, int agencyId);
        BaseResponse<ExportProduct> Update(ExportProduct item, int agencyId,decimal PriceDebt);
        BaseResponse<ExportProduct> AddErp(ExportProduct item, decimal PriceDebt, int agencyId);
        decimal GetTotalDebtAgency(int agencyId);

        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(ExportProduct item);
        void Add(ExportProduct category);
    }

    public class ExportProductDa : IExportProductDa
    {
        readonly ApplicationDbContext _context;


        public ExportProductDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<ExportProductItem>>> GetListSimpleByRequest(BaseRequest request, int agencyId)
        {
            var source = from o in _context.ExportProducts
                        where (!o.IsDeleted.HasValue || o.IsDeleted == false) 
                        && (agencyId == 0 || o.AgencyId == agencyId)
                        orderby o.Id descending
                        select new ExportProductItem
                        {
                            Id = o.Id,
                            Code = o.Code,
                            TotalPrice = o.TotalPrice,
                            DateCreated = o.DateCreated,
                            AgencyId = o.AgencyId,
                            Note = o.Note,
                            IsOrder = o.IsOrder,
                            Payment = o.Payment,
                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<ExportProductItem>> response = new BaseResponse<IList<ExportProductItem>>();
            response.Data = query.data.Cast<ExportProductItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public ExportProductItem GetbyIdItem(int id,int agencyId)
        {

            var source = from o in _context.ExportProducts
                where (!o.IsDeleted.HasValue || o.IsDeleted == false)
                      && (agencyId == 0 || o.AgencyId == agencyId)
                      && o.Id == id
                         orderby o.Id descending
                select new ExportProductItem
                {
                    Id = o.Id,
                    Code = o.Code,
                    TotalPrice = o.TotalPrice,
                    DateCreated = o.DateCreated,
                    AgencyId = o.AgencyId,
                    Note = o.Note,
                    Payment = o.Payment,
                    ExportProductDetailItems = o.ExportProductDetails.Where(a=>a.IsDelete == false).Select(c=> new ExportProductDetailItem
                    {
                        ProductName = c.ImportProduct.ProductDetail.Name,
                        Barcode = c.ImportProduct.BarCode,
                        Sizename = c.ImportProduct.ProductDetail.Size.Name,
                        Color = c.ImportProduct.ProductDetail.Color.Name,
                        Price = c.Price,
                        Quantity = c.Quantity,
                        
                    })
                };
            return source.FirstOrDefault();
        }

        public decimal GetTotalDebtAgency(int id)
        {
            var souce= _context.Debts.Where(a=>a.AgencyId == id).Sum(a=>a.Price - a.Payment);
            return souce ?? 0;
        }
        public ExportProduct GetbyId(int id,int agencyId)
        {

            var query = from o in _context.ExportProducts
                        where o.Id == id
                              && (agencyId == 0 || o.AgencyId == agencyId)

                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<ExportProduct> Update(ExportProduct item,int agencyId,decimal PriceDebt)
        {
            var debt = GetTotalDebtAgency(item.AgencyIdRecive ?? 0);
            if (debt + (item.TotalPrice - item.Payment) >= PriceDebt)
            {
                
                return BasiResponse.Error(new ExportProduct(),"Công nợ của đại lý vượt quá hạn mức.");

            }
            else
            {
                var model = GetbyId(item.Id, agencyId);
                if (model != null)
                {
                    var lst = model.ExportProductDetails.Where(c => c.IsDelete == false).ToList();
                    var result1 = lst.Where(p =>
                        item.ExportProductDetails.All(p2 => p2.InportProductId != p.InportProductId)).ToList();
                    foreach (var i in result1)
                    {
                        i.IsDelete = true;
                    }

                    //sửa
                    foreach (var i in lst)
                    {
                        var j = item.ExportProductDetails.FirstOrDefault(c => c.InportProductId == i.InportProductId);
                        if (j == null) continue;
                        i.Quantity = j.Quantity;
                        i.Date = j.Date;
                        i.Price = j.Price;
                    }

                    //thêm mới
                    var result2 = item.ExportProductDetails
                        .Where(p => lst.All(p2 => p2.InportProductId != p.InportProductId)).ToList();
                    result2.ForEach(c => model.ExportProductDetails.Add(c));
                    Save();
                    return BasiResponse.Success(model);

                }
                
                return BasiResponse.Error(new ExportProduct());

            }


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(ExportProduct item)
        {
            _context.ExportProducts.Remove(item);
        }
        public BaseResponse<ExportProduct> AddErp(ExportProduct data,decimal PriceDebt,int AgencyId)
        {
            var debt = GetTotalDebtAgency(data.AgencyIdRecive ?? 0);
            if (debt + (data.TotalPrice - data.Payment) >= PriceDebt)
            {
                return BasiResponse.Error(new ExportProduct(), "Công nợ của đại lý vượt quá hạn mức.");
            }
            else
            {
                data.IsDeleted = false;
                if (AgencyId > 0) data.AgencyId = AgencyId;
                _context.ExportProducts.Add(data);
                Save();
                return BasiResponse.Success(data);
            }
        }

        public void Add(ExportProduct item)
        {
            _context.ExportProducts.Add(item);
        }
        
    }
}
