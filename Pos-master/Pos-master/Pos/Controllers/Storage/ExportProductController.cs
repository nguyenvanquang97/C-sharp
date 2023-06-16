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
using Wini.Utils;

namespace Pos.Controllers
{
    public class ExportProductController : BaseController
    {
        private readonly ILogger<ExportProductController> _logger;
        private IExportProductDa _productDa;
        private readonly ICacheService _cacheService;
        public ExportProductController(ILogger<ExportProductController> logger, IExportProductDa productDa)
        {
            _logger = logger;
            _productDa = productDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<ExportProductItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleByRequest(request, AgencyId);
            return model;
        }
        [HttpPost]
        public BaseResponse<ExportProduct> Add([FromBody] ExportProduct data)
        {
            return _productDa.AddErp(data, PriceDebt, AgencyId);
        }
        [HttpPost]
        public BaseResponse<ExportProduct> Update([FromBody] ExportProduct data)
        {
            var model =  _productDa.Update(data, AgencyId,PriceDebt);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _productDa.GetbyId(id, AgencyId);
            if (model != null)
            {
                model.IsDeleted = true;
                _productDa.Save();
                return BasiResponse.Success(id);
            }
                                                return BasiResponse.Nodata(0);
        }
        //[HttpPost]
        //public HttpResponseMessage ExportExcelbyId(int Id)
        //{
        //    var model = _ProductDa.GetbyIdItem(Id);
        //    var result = Excel.ExportListOrderDetail(model);
        //    return result;
        //}

    }
    
}
