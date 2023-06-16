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
        public async Task<BaseResponse<IList<CategoryItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _categoryDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public BaseResponse<Category> Add([FromBody] Category data)
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
            return BasiResponse.Success(data);

        }
        [HttpPost]
        public BaseResponse<Category> Update([FromBody] Category data)
        {
            var model =  _categoryDa.Update(data);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _categoryDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _categoryDa.Save();
                return BasiResponse.Success(id);
            }
                                                return BasiResponse.Nodata(0);
        }
    }
}
