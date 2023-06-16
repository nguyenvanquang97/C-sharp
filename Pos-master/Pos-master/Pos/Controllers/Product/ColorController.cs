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
    public class ColorController : BaseController
    {
        private readonly ILogger<ColorController> _logger;
        private IColorDa _colorDa;
        private readonly ICacheService _cacheService;
        public ColorController(ILogger<ColorController> logger, IColorDa colorDa)
        {
            _logger = logger;
            _colorDa = colorDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<ColorItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _colorDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public BaseResponse<Color> Add([FromBody] Color data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.Description = HttpUtility.UrlDecode(data.Description);
            data.Value = HttpUtility.UrlDecode(data.Value);
            data.IsDeleted = false;
            data.IsShow = true;
            //data.UserCreate = int.Parse(UserId);
            _colorDa.Add(data);
            _colorDa.Save();
                return BasiResponse.Success(data);

        }
        [HttpPost]
        public BaseResponse<Color> Update([FromBody] Color data)
        {
            var model = _colorDa.Update(data);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _colorDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _colorDa.Save();
                return BasiResponse.Success(id);
            }
                                                return BasiResponse.Nodata(0);
        }
    }
}
