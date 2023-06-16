using Lazop.Api.Util;
using Wini.SaleMultipleChannel.Lazop;

using Wini.SellMultipleChanel.Model;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Wini.SaleMultipleChannel.CreateProductRequest;
using System;
using System.Xml;
using Wini.SaleMultipleChannel.UpdateProductRequest;
using Wini.Simple;
using static Wini.SaleMultipleChannel.SaleLazada;
using static Wini.SaleMultipleChannel.Model.Product.SaleLazada;
using Wini.Database;
using Product = Wini.Database.Product;
using Wini.SaleMultipleChannel.Model.CreateProduct;
using Wini.SaleMultipleChannel.Model.Token;
using Wini.SaleMultipleChannel.Model.Order;

namespace Wini.SaleMultipleChannel
{

    public interface ISale
    {

        /// <summary>
        /// get token from.
        /// </summary>
        /// <param name="url">https://auth.lazada.com/rest</param>
        /// <param name="appKey">appKey.</param>
        /// <param name="appSecret">AppSecret.</param>
        /// <param name="code">code.</param>
        /// <returns>token</returns>
        Task<BaseResponse<TokenEcomResponse>> GetToken(string code, string shopId);

        /// <summary>
        /// get all product
        /// </summary>
        /// <param name="url"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="accessToken"></param>
        /// <returns>all product</returns>
        /// 
        Task<BaseResponse<TokenEcomResponse>> RefreshAccessToken(int agencyId);
    

        /// <summary>
        /// upload anh product
        /// </summary>
        /// <param name="url"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="fileItem">from lazada sdk</param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        /// 
        BaseResponse<List<ProductEcom>> GetAllProductAsynchronous(int agencyId);
        Task<BaseResponse<List<Product>>> GetAllProductSynced(int agencyId);
        Task<BaseResponse<object>> UploadImage(int agencyId, FileItem fileItem);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="accessToken"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        Task<List<CategorySuggestion>> GetCategorySuggestion(int agencyId, string productName);


        Task<BaseResponse<CreateProductEcom>> CreateProduct(int agencyId, Wini.Database.Product product);
        Task<BaseResponse<Product>> UpdateProduct(int agencyId, ProductDetail productDetail,string sellerSku);

        Task<BaseResponse<object>> DeleteProduct(int agencyId, List<string> sellerSkus);
        Task<BaseResponse<OrderEcomResponse>> GetOrders(int agencyId);

        Task<string> SetStatusToCanceled(string url, string accessToken, long orderItemId);

        Task<string> SetStatusToReadyToShip(string url, string accessToken, long[] orderItemIds);

        Task<string> SetStatusToSOFFFailedDelivery(string url, string accessToken, long[] orderItemIds);
        Task<BaseResponse<string>> MigrateImage(int agencyId, string url);
        Task<BaseResponse<object>> UpdatePrice(int agencyId, string sellerSku,string price);
        Task<BaseResponse<object>> UpdateQuantity(int agencyId, string sellerSku, string quantity);
        Task<BaseResponse<List<ProductEcom>>> SyncProductFromEcom(int agencyId, List<string> sellerSkus);
        Task<List<LazadaCategoryReponse>> GetAllCategory();

    }
}