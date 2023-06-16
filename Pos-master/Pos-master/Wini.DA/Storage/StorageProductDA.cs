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
    
     public interface IStorageProductDa
    {
         

        /// <summary>
        /// Gets all category.
        /// </summary>
        /// <returns>danh sách category.</returns>
       Task<BaseResponse<IList<StorageProductItem>>> GetListSimpleByRequest(BaseRequest request,int agencyId,int type);
       Task<BaseResponse<IList<ImportProductItem>>> GetListSimpleInventoryByRequest(BaseRequest request, int agencyId);
        StorageProduct GetbyId(int id, int agencyId);
        StorageProductItem GetbyIdItem(int id, int agencyId);
        BaseResponse<StorageProduct> Update(StorageProduct item,int agencyId);


        /// <summary>
        /// save.
        /// </summary>
        void Save();
        void Remove(StorageProduct item);
        void Add(StorageProduct category);
    }

    public class StorageProductDa : IStorageProductDa
    {
        readonly ApplicationDbContext _context;


        public StorageProductDa(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<IList<StorageProductItem>>> GetListSimpleByRequest(BaseRequest request,int agencyId, int type)
        {

            var source = from o in _context.StorageProducts
                        where (!o.IsDelete.HasValue || o.IsDelete == false)  &&
                        (agencyId == 0 || o.AgencyId == agencyId)
                        && o.Type == type
                        orderby o.Id descending
                        select new StorageProductItem
                        {
                            Id = o.Id,
                            Code = o.Code,
                            TotalPrice = o.TotalPrice,
                            DateCreated = o.DateCreated,
                            DateImport = o.DateImport,
                            AgencyId = o.AgencyId,
                            Status = o.Status,
                            Type = o.Type,
                            Quantity = o.Quantity,
                            Note = o.Note,
                        };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<StorageProductItem>> response = new BaseResponse<IList<StorageProductItem>>();
            response.Data = query.data.Cast<StorageProductItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public async Task<BaseResponse<IList<ImportProductItem>>> GetListSimpleInventoryByRequest(BaseRequest request,int agencyId)
        {

            var source = from o in _context.ImportProducts
                where (!o.IsDelete.HasValue || o.IsDelete == false)
                      &&
                      (agencyId == 0 || o.AgencyId == agencyId)
                         orderby o.DateEx descending
                select new ImportProductItem
                {
                    Name = o.ProductDetail.Name,
                    Code = o.ProductDetail.Code,
                    CodeSku = o.ProductDetail.CodeSku,
                    ColorName = o.ProductDetail.Color.Name,
                    BarCode = o.BarCode,
                    DateEnd = o.DateEnd,
                    Date = o.CreateDate,
                    AgencyId = o.AgencyId,
                    Quantity = o.Quantity,
                    QuantityOut = o.QuantityOut,
                    SizeName = o.ProductDetail.Size.Name,
                    UnitName = o.ProductDetail.Product.Unit.Name,
                    UrlPicture = o.ProductDetail.Picture.Folder + o.ProductDetail.Picture.Url,
                    PriceNew = o.PriceNew,
                    Price = o.Price,
                };
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<ImportProductItem>> response = new BaseResponse<IList<ImportProductItem>>();
            response.Data = query.data.Cast<ImportProductItem>().ToList();
            if (request.LoadOptions.RequireTotalCount)
            {
                response.TotalCount = query.totalCount;
            }
            return BasiResponse.Success(response.Data, "Lấy dữ liệu thành công", response.TotalCount);

        }
        public StorageProductItem GetbyIdItem(int id,int agencyId)
        {

            var source = from o in _context.StorageProducts
                where (!o.IsDelete.HasValue || o.IsDelete == false)
                      &&
                      (agencyId == 0 || o.AgencyId == agencyId) && o.Id == id
                         orderby o.Id descending
                select new StorageProductItem
                {
                    Id = o.Id,
                    Code = o.Code,
                    TotalPrice = o.TotalPrice,
                    DateCreated = o.DateCreated,
                    DateImport = o.DateImport,
                    AgencyId = o.AgencyId,
                    Quantity = o.Quantity,
                    Note = o.Note,
                    ImportProductItem = o.ImportProducts.Where(a=>a.IsDelete == false).Select(c=> new ImportProductItem
                    {
                        Name = c.ProductDetail.Name,
                        CodeSku = c.ProductDetail.CodeSku,
                        BarCode = c.BarCode,
                        Price = c.Price,
                        PriceNew = c.PriceNew,
                        ProductId = c.ProductId,
                        Quantity = c.Quantity,
                        QuantityOut = c.QuantityOut,
                        DateEnd = c.DateEnd,
                        ColorName = c.ProductDetail.Color.Name,
                        SizeName = c.ProductDetail.Size.Name,
                    })
                };
            return source.FirstOrDefault();
        }
        public StorageProduct GetbyId(int id,int agencyId)
        {

            var query = from o in _context.StorageProducts 
                                 
                        where o.Id == id
                              &&
                              (agencyId == 0 || o.AgencyId == agencyId)
                        orderby o.Id descending
                        select o;
            return query.FirstOrDefault();
        }
        public BaseResponse<StorageProduct> Update(StorageProduct item,int agencyId)
        {
            var model = GetbyId(item.Id, agencyId);
            if (model != null)
            {
                var lst = model.ImportProducts.Where(c => c.IsDelete == false).ToList();
                var result1 = lst.Where(p => item.ImportProducts.All(p2 => p2.ProductId != p.ProductId)).ToList();
                foreach (var i in result1)
                {
                    i.IsDelete = true;
                }
                //sửa
                foreach (var i in lst)
                {
                    var j = item.ImportProducts.FirstOrDefault(c => c.ProductId == i.ProductId);
                    if (j == null) continue;
                    i.Quantity = j.Quantity;
                    i.DateEnd = j.DateEnd;
                    i.Date = j.Date;
                    i.BarCode = j.BarCode;
                    i.Value = j.Value;
                    i.Price = j.Price;
                    i.PriceNew = j.PriceNew;
                }
                //thêm mới
                var result2 = item.ImportProducts.Where(p => lst.All(p2 => p2.ProductId != p.ProductId)).ToList();
                result2.ForEach(c=> model.ImportProducts.Add(c));
                Save();
                return BasiResponse.Success(model);

            }
            return BasiResponse.Error(new StorageProduct());


        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Remove(StorageProduct item)
        {
            _context.StorageProducts.Remove(item);
        }
        public void Add(StorageProduct item)
        {
            _context.StorageProducts.Add(item);
        }
        
    }
}
