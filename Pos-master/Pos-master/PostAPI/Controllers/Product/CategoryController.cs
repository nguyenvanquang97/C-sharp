using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;
        private ICategoryDa _categoryDa;
        private readonly ICacheService _cacheService;
        public CategoryController(ILogger<CategoryController> logger, ICategoryDa categoryDa)
        {
            _logger = logger;
            _categoryDa = categoryDa;
        }
        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] BaseRequest request)
        {
            var model = await _categoryDa.GetListSimpleByRequest(request);
            return Json(model);
        }
        [HttpPost]
        public IActionResult Add([FromBody] Category data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.Slug = FomatString.Slug(data.Name);
            data.Description = HttpUtility.UrlDecode(data.Description);
            data.IsDeleted = false;
            data.IsShow = true;
            data.CreatedDate = DateTime.Now;
            //data.UserCreate = int.Parse(UserId);
            _categoryDa.Add(data);
            _categoryDa.Save();
            return Json(new BaseResponse<Category>() { Data = data, Code = (int)ResponseCode.Success, Message = "Thêm mới dữ liệu thành công" });
        }
        [HttpPost]
        public   IActionResult Update([FromBody] Category data)
        {
            var model =  _categoryDa.Update(data);
            return Json(model);
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _categoryDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _categoryDa.Save();
                return new BaseResponse<int>() { Data = id, Code = (int)ResponseCode.Success, Message = "Xóa dữ liệu thành công" };
            }
            return new BaseResponse<int>() { Data = 0, Code = (int)ResponseCode.Nodata, Message = "Dữ liệu không tồn tài" };
        }
    }
}
