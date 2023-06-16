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

namespace Pos.Controllers
{

    [Route("[controller]")]
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private IProductDa _productDa;
        private readonly ICacheService _cacheService;
        public ProductController(ILogger<ProductController> logger,  IProductDa productDa)
        {
            _logger = logger;
            _productDa = productDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<ProductItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDa.GetListSimpleByRequest(request);
            return model;
        }
        //[HttpPost]
        //public IActionResult Add([FromBody] Product data)
        //{
        //    data.Name = HttpUtility.UrlDecode(data.Name);
        //    data.NameAscii = FomatString.Slug(data.Name);
        //    data.IsDelete = false;
        //    data.IsShow = true;
        //    //data.UserCreate = int.Parse(UserId);
        //    _productDa.Add(data);
        //    _productDa.Save();
            
           
        //    return Json(new BaseResponse<Product>() { Data = data, Code = (int)ResponseCode.Success, Message = "Thêm mới dữ liệu thành công" });
        //}
        
        //[HttpPost]
        //public   IActionResult Update([FromBody] Product data)
        //{
        //    var model =  _productDa.Update(data);
        //    return Json(model);
        //}
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _productDa.GetbyId(id);
            if (model != null)
            {
                model.IsDelete = true;
                _productDa.Save();
                return new BaseResponse<int>() { Data = id, Code = (int)ResponseCode.Success, Message = "Xóa dữ liệu thành công" };
            }
            return new BaseResponse<int>() { Data = 0, Code = (int)ResponseCode.Nodata, Message = "Dữ liệu không tồn tài" };
        }
    }
    
}
