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
        public async Task<BaseResponse<IList<SizeItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _sizeDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public BaseResponse<Size> Add([FromBody] Size data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.IsDeleted = false;
            data.IsShow = true;
            //data.UserCreate = int.Parse(UserId);
            _sizeDa.Add(data);
            _sizeDa.Save();
            return BasiResponse.Success(data);

        }
        [HttpPost]
        public BaseResponse<Size> Update([FromBody] Size data)
        {
            var model =  _sizeDa.Update(data);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _sizeDa.GetbyId(id);
            if (model != null)
            {
                model.IsDeleted = true;
                _sizeDa.Save();
                return BasiResponse.Success(id);
            }
                                                return BasiResponse.Nodata(0);
        }
    }
}
