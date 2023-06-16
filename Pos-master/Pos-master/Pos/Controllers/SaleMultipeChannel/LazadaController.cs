
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Wini.DA;
using Wini.DA.Cache;
using Wini.Database;
using Wini.Simple;
using Wini.Utils;
using Wini.SaleMultipleChannel;
using System;
using Lazop.Api.Util;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using Autofac;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Xml.Linq;

namespace Pos.Controllers
{

    public class LazadaController : BaseController
    {
        private readonly ISale _saleLazada;

        private readonly ILogger<LazadaController> _logger;
        private readonly ICacheService _cacheService;
        private ILifetimeScope _scope;
        public LazadaController( ILogger<LazadaController> logger, ILifetimeScope scope)
        {
            //_saleLazada = saleLazada;
            _logger = logger;
            _scope = scope;
        }
        [HttpGet]
        public async Task<IActionResult> GetToken(string code, string shopId)
        {
            var result = await _saleLazada.GetToken(code, shopId);
            return Json(result);
        }

        //[HttpGet]
        //public async Task<IActionResult> Test(string[] keys)
        //{
        //    var key = "lazada";
        //   var _saleLazada2 = this._scope.ResolveNamed<ISale>(key);
        //  var  _saleShoppe2 = this._scope.ResolveNamed<ISale>("shoppe");
        //    foreach (var key2 in keys)
        //    {
        //        var _sale = this._scope.ResolveNamed<ISale>(key2);
        //        _sale.CreateProduct();

        //    }
        //    // TODO: tìm item trong database chưa đồng bộ, trả về FE

        //    // loop skus.SellerSku tim trong AgencyProductEcom nếu chưa có thì chưa đồng bộ
        //    //table AgencyProductEcom
        //    return Json(1);
        //}


        /// <summary>
        /// lấy ra danh sách sản phẩm chưa đồng bộ với database
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAllProductAsynchronous()
        {
            var result = _saleLazada.GetAllProductAsynchronous(AgencyId);

            // TODO: tìm item trong database chưa đồng bộ, trả về FE

            // loop skus.SellerSku tim trong AgencyProductEcom nếu chưa có thì chưa đồng bộ
            //table AgencyProductEcom
            return Json(result);
        }
        [HttpPost]
        public async Task<IActionResult> SyncProductFromEcom([FromBody] List<string> sellerSkus)
        {


            var result = await _saleLazada.SyncProductFromEcom(AgencyId, sellerSkus);

            // TODO: tìm item trong database chưa đồng bộ, trả về FE

            // loop skus.SellerSku tim trong AgencyProductEcom nếu chưa có thì chưa đồng bộ
            //table AgencyProductEcom
            return Json(result);
        }

        /// <summary>
        /// lấy ra danh sách sản phẩm chưa đồng bộ với database
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<IActionResult> GetAllProductSynced()
        {
            // lay từ database bảng product đã đồng bộ
            // select * from 

            // table AgencyProductEcom
            /// select sp.Name , ape.SellerSku,* from Shop_Product_Detail sp
            //  left join AgencyProductEcom ape 
            //  on sp.ID  = ape .ProducId 
            ///
            var result = await _saleLazada.GetAllProductSynced(AgencyId);
            return Json(result);
        }



        [HttpPost]
        public async Task<IActionResult> MigrateImage(string imageUrl)
        {
            var result = await _saleLazada.MigrateImage(AgencyId, imageUrl);
            return Json(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var result = await _saleLazada.CreateProduct(AgencyId, product);
            return Json(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDetail productDetail,string sellerSku)
        {
            var result = await _saleLazada.UpdateProduct(AgencyId, productDetail, sellerSku);
            return Json(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePrice(string sellerSku, string price)
        {
            var result = await _saleLazada.UpdatePrice(AgencyId, sellerSku, price);
            return Json(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateQuantity(string sellerSku, string quantity)
        {
            var result = await _saleLazada.UpdateQuantity(AgencyId, sellerSku, quantity);
            return Json(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] List<string> sellerSkus)
        {
            var result = await _saleLazada.DeleteProduct(AgencyId, sellerSkus);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetCategorySuggestion(string name)
        {
            var result = await _saleLazada.GetCategorySuggestion(AgencyId, name);
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var result = await _saleLazada.GetAllCategory();
            return Json(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var result = await _saleLazada.GetOrders(AgencyId);
            return Json(result);
        }
    }
}
