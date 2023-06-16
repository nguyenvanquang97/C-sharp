using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Wini.DA;
using Wini.DA.Cache;
using Wini.Database;
using Wini.Simple;
using Wini.Simple.Storage;
using Wini.Utils;

namespace Pos.Controllers
{
    public class StorageProductRequestController : BaseController
    {
        private readonly ILogger<StorageProductRequestController> _logger;
        private IStorageProductDa _productDa;
        private IExportProductDa _exportProductDa;
        private readonly ICacheService _cacheService;
        public StorageProductRequestController(ILogger<StorageProductRequestController> logger, IStorageProductDa productDa, IExportProductDa exportProductDa)
        {
            _logger = logger;
            _productDa = productDa;
            _exportProductDa = exportProductDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<StorageProductItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleByRequest(request, AgencyId, 1);
            return model;
        }
        public async Task<BaseResponse<IList<ImportProductItem>>> Inventory([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleInventoryByRequest(request, AgencyId);
            return model;
        }
        [HttpPost]
        public BaseResponse<StorageProduct> Add([FromBody] StorageProduct data)
        {
            data.IsDelete = false;
            data.Type = 0; //  tạo phiếu nhập kho.
            data.Status = (int?)StatusApp.Succuess;
            _productDa.Add(data);
            _productDa.Save();
            return BasiResponse.Success(data);

        }

        [HttpPost]
        public BaseResponse<bool> UpdateStatus(int id, int status,decimal payment = 0)
        {
            var model = _productDa.GetbyId(id, AgencyId);
            if (model != null)
            {
                model.Status = status;
                if (status == (int)StatusApp.Succuess)
                {
                    model.Type = 0;
                    var exportErp = new ExportProduct();
                    exportErp.DateCreated = DateTime.Now.TotalSeconds();
                    exportErp.TotalPrice = model.TotalPrice;
                    exportErp.Payment = payment;
                    exportErp.AgencyIdRecive = model.AgencyId;
                    exportErp.AgencyId = 0;
                    exportErp.Code = model.Code;
                    foreach (var item in model.ImportProducts)
                    {
                        var a = new ExportProductDetail()
                        {
                            Date = item.Date,
                            Price = item.Price,
                            IsDelete = false,
                            InportProductId = item.Gid,
                            Quantity =item.Quantity,
                        };
                        exportErp.ExportProductDetails.Add(a);
                    }
                    _exportProductDa.Add(exportErp);
                    _exportProductDa.Save();
                }
                _productDa.Save();
                return BasiResponse.Success(true);
            }
            return BasiResponse.Nodata(false);
        }
        [HttpPost]
        public BaseResponse<StorageProduct> Update([FromBody] StorageProduct data)
        {
            var model = _productDa.Update(data, AgencyId);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _productDa.GetbyId(id, AgencyId);
            if (model != null)
            {
                model.IsDelete = true;
                _productDa.Save();
                return BasiResponse.Success(id);
            }
            return BasiResponse.Nodata(0);
        }
        [HttpPost]
        public HttpResponseMessage ExportExcelbyId(int id)
        {
            var model = _productDa.GetbyIdItem(id, AgencyId);
            var result = Excel.ExportListOrderDetail(model);
            return result;
        }
    }

}
