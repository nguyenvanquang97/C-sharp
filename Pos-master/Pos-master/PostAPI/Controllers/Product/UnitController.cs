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
        public async Task<IActionResult> GetAll([FromBody] BaseRequest request)
        {
            var model = await _unitDa.GetListSimpleByRequest(request);
            return Json(model);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Unit data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.IsDeleted = false;
            data.IsShow = true;
            //data.UserCreate = int.Parse(UserId);
            _unitDa.Add(data);
            _unitDa.Save();
            return Json(new BaseResponse<Unit>() { Data = data, Code = (int)ResponseCode.Success, Message = "Thêm mới dữ liệu thành công" });
        }
        [HttpPost]
        public   IActionResult Update([FromBody] Unit data)
        {
            var model =  _unitDa.Update(data);
            return Json(model);
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _unitDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _unitDa.Save();
                return new BaseResponse<int>() { Data = id, Code = (int)ResponseCode.Success, Message = "Xóa dữ liệu thành công" };
            }
            return new BaseResponse<int>() { Data = 0, Code = (int)ResponseCode.Nodata, Message = "Dữ liệu không tồn tài" };
        }
    }
}
