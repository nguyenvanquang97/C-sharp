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
    public class BrandController : BaseController
    {
        private readonly ILogger<BrandController> _logger;
        private IBrandDa _brandDa;
        private readonly ICacheService _cacheService;
        public BrandController(ILogger<BrandController> logger,  IBrandDa brandDa)
        {
            _logger = logger;
            _brandDa = brandDa;
        }
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] BaseRequest request)
        {
            var model = await _brandDa.GetListSimpleByRequest(request);
            return Json(model);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Brand data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.IsDeleted = false;
            data.IsShow = true;
            //data.UserCreate = int.Parse(UserId);
            _brandDa.Add(data);
            _brandDa.Save();
            return Json(new BaseResponse<Brand>() { Data = data, Code = (int)ResponseCode.Success, Message = "Thêm mới dữ liệu thành công" });
        }
        [HttpPost]
        public   IActionResult Update([FromBody] Brand data)
        {
            var model =  _brandDa.Update(data);
            return Json(model);
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _brandDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _brandDa.Save();
                return new BaseResponse<int>() { Data = id, Code = (int)ResponseCode.Success, Message = "Xóa dữ liệu thành công" };
            }
            return new BaseResponse<int>() { Data = 0, Code = (int)ResponseCode.Nodata, Message = "Dữ liệu không tồn tài" };
        }
    }
}
