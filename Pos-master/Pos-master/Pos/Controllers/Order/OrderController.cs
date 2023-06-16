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
using System.Collections.Generic;

namespace Pos.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;
        private IOrderDa _productDa;
        private readonly ICacheService _cacheService;
        public OrderController(ILogger<OrderController> logger, IOrderDa productDa)
        {
            _logger = logger;
            _productDa = productDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<OrderItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleByRequest(request, AgencyId);
            return model;
        }
        [HttpPost]
        public BaseResponse<OrderItem> Detail(int Id)
        {
            var model = _productDa.getOrderbyId(Id);
                return BasiResponse.Success(model);

        }
        [HttpPost]
        public BaseResponse<int> UpdateStatus(int orderId, int status)
        {
            var model = _productDa.GetbyId(orderId);
            if (model != null)
            {
                model.Status = status;
                _productDa.Save();
            }
                        return BasiResponse.Nodata(0);
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _productDa.GetbyId(id);
            if (model != null)
            {
                model.IsDelete = true;
                _productDa.Save();
                return BasiResponse.Success(id);
            }
            return BasiResponse.Nodata(0);

        }


    }

}
