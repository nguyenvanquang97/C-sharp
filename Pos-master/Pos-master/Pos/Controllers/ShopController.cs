// ---------------------------------------------------
// <copyright file="ProductController.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Pos.Controllers
{
    using System;
    using System.Collections.Generic;
    using DevExtreme.AspNet.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Wini.DA;
    using Wini.DA.Cache;
    using Wini.Database;

    public class ShopController : BaseController
    {
        private readonly ICacheService _cacheService;
        IShopDa _iShopDa;
        public ShopController(IShopDa iShopDa)
        {
            _iShopDa = iShopDa;
        }

        public IActionResult Index(DataSourceLoadOptionsBase loadOptionsBase)
        {
            var tmo = HttpContext.Request;
            var lst = _iShopDa.GetAll();

            return Json(lst);
        }

        [Authorize]
        public IActionResult Index2()
        {
            return Json(UserId);
        }
    }
}
