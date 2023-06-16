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
    public class SizeController : BaseController
    {
        private readonly ILogger<SizeController> _logger;
        private ISizeDa _sizeDa;
        private readonly ICacheService _cacheService;
        public SizeController(ILogger<SizeController> logger,  ISizeDa sizeDa)
        {
            _logger = logger;
            _sizeDa = sizeDa;
        }
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] BaseRequest request)
        {
            var model = await _sizeDa.GetListSimpleByRequest(request);
            return Json(model);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Size data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.IsDeleted = false;
            data.IsShow = true;
            //data.UserCreate = int.Parse(UserId);
            _sizeDa.Add(data);
            _sizeDa.Save();
            return Json(new BaseResponse<Size>() { Data = data, Code = (int)ResponseCode.Success, Message = "Thêm mới dữ liệu thành công" });
        }
        [HttpPost]
        public   IActionResult Update([FromBody] Size data)
        {
            var model =  _sizeDa.Update(data);
            return Json(model);
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _sizeDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _sizeDa.Save();
                return new BaseResponse<int>() { Data = id, Code = (int)ResponseCode.Success, Message = "Xóa dữ liệu thành công" };
            }
            return new BaseResponse<int>() { Data = 0, Code = (int)ResponseCode.Nodata, Message = "Dữ liệu không tồn tài" };
        }
    }
}
