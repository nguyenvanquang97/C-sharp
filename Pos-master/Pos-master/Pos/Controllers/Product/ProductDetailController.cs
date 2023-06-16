using System.Collections.Generic;
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
    public class ProductDetailController : BaseController
    {
        private readonly ILogger<ProductDetailController> _logger;
        private IProductDetailDa _productDetailDa;
        private readonly ICacheService _cacheService;
        public ProductDetailController(ILogger<ProductDetailController> logger,  IProductDetailDa productDetailDa)
        {
            _logger = logger;
            _productDetailDa = productDetailDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<ProductDetailItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _productDetailDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public BaseResponse<ProductDetail> Add([FromBody] ProductDetail data)
        {
            data.Name = HttpUtility.UrlDecode(data.Name);
            data.NameAscii = FomatString.Slug(data.Name);
            data.IsDelete = false;
            data.IsShow = true;
            //data.UserCreate = int.Parse(UserId);
            _productDetailDa.Add(data);
            _productDetailDa.Save();
            
            if (!string.IsNullOrEmpty(data.LstPicture))
            {
                var lstInt = FdiUtils.StringToListInt(data.LstPicture);
                foreach (var item in lstInt)
                {
                    var pic = new ProductDetailPicture
                    {
                        PictureId = item,
                        ProductId = data.Id,
                        Sort = 0
                    };
                    _productDetailDa.AddPic(pic);
                }
                _productDetailDa.Save();
            }
            return BasiResponse.Success(data);

        }

        [HttpPost]
        public BaseResponse<ProductDetail> Update([FromBody] ProductDetail data)
        {
            var model =  _productDetailDa.Update(data);
            return model;
        }
        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _productDetailDa.GetbyId(id);
            if (model != null)
            {
                model.IsDelete = true;
                _productDetailDa.Save();
                return BasiResponse.Success(id);
            }
                                                return BasiResponse.Nodata(0);
        }
    }
    
}
