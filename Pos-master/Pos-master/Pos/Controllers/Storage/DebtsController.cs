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
    public class DebtsController : BaseController
    {
        private readonly ILogger<DebtsController> _logger;
        private IDebtDa _productDa;
        private readonly ICacheService _cacheService;
        public DebtsController(ILogger<DebtsController> logger, IDebtDa productDa)
        {
            _logger = logger;
            _productDa = productDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<DebtItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public BaseResponse<DebtItem> Detail(int id)
        {
            var model =  _productDa.GetbyIdItem(id);
            return BasiResponse.Success(model);

        }

        [HttpPost]
        public BaseResponse<Debt> Add([FromBody] Debt data)
        {
            //data.IsDelete = false;
            _productDa.Add(data);
            _productDa.Save();
            return BasiResponse.Success(data);

        }

        //[HttpPost]
        //public IActionResult Update([FromBody] Debt data)
        //{
        //    var model = _ProductDa.Update(data);
        //    return Json(model);
        //}
        [HttpPost]
        public BaseResponse<int> Delete(int id)
        {
            var model = _productDa.GetbyId(id);
            if (model != null)
            {
                _productDa.Remove(model);
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
