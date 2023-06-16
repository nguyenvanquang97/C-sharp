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
    public class AgencyTypeController : BaseController
    {
        private readonly ILogger<AgencyTypeController> _logger;
        private IAgencyTypeDa _productDa;
        private readonly ICacheService _cacheService;
        public AgencyTypeController(ILogger<AgencyTypeController> logger, IAgencyTypeDa productDa)
        {
            _logger = logger;
            _productDa = productDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<AgencyTypeItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleByRequest(request);
            return model;
        }
        
        [HttpPost]
        public BaseResponse<AgencyType> Add([FromBody] AgencyType data)
        {
            data.IsDeleted = false;
            data.IsShow = false;
            data.TotalPayment = 0;
            _productDa.Add(data);
            _productDa.Save();
            return BasiResponse.Success(data);
        }

        [HttpPost]
        public BaseResponse<AgencyType> Update([FromBody] AgencyType data)
        {
            var model =  _productDa.Update(data);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _productDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _productDa.Save();
                return BasiResponse.Success(id);
            }
            return BasiResponse.Nodata(0);

        }

    }
    
}
