using System.Collections.Generic;
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
    public class UnitController : BaseController
    {
        private readonly ILogger<UnitController> _logger;
        private IUnitDa _unitDa;
        private readonly ICacheService _cacheService;
        public UnitController(ILogger<UnitController> logger,  IUnitDa unitDa)
        {
            _logger = logger;
            _unitDa = unitDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<UnitItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _unitDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public BaseResponse<Unit> Add([FromBody] Unit data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.IsDeleted = false;
            data.IsShow = true;
            //data.UserCreate = int.Parse(UserId);
            _unitDa.Add(data);
            _unitDa.Save();
            return BasiResponse.Success(data);

        }
        [HttpPost]
        public BaseResponse<Unit> Update([FromBody] Unit data)
        {
            var model =  _unitDa.Update(data);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _unitDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _unitDa.Save();
                return BasiResponse.Success(id);
            }
                                                return BasiResponse.Nodata(0);
        }
    }
}
