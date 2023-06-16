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
    public class StorageProductController : BaseController
    {
        private readonly ILogger<StorageProductController> _logger;
        private IStorageProductDa _productDa;
        private readonly ICacheService _cacheService;
        public StorageProductController(ILogger<StorageProductController> logger, IStorageProductDa productDa)
        {
            _logger = logger;
            _productDa = productDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<StorageProductItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleByRequest(request, AgencyId, 0);
            return model;
        }
        [HttpPost]
        public BaseResponse<StorageProduct> Add([FromBody] StorageProduct data)
        {
            data.IsDelete = false;

            data.Status = (int?)StatusApp.New;
            data.Type = 1; // 1: yêu cầu nhập kho từ đại lý - > ERP

            _productDa.Add(data);
            _productDa.Save();
            return BasiResponse.Success(data);

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
