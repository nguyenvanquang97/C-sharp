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
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Wini.DA.Cache;
    using Wini.Database;

    public class ProductController : BaseController
    {
        private readonly ICacheService _cacheService;

        public ProductController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public IActionResult Index()
        {
            var lst = new List<Users>();

            lst.Add(new Users() { Id = Guid.NewGuid() });
            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            _cacheService.SetData<IEnumerable<Users>>("Users", lst, expirationTime);

            var cacheData = _cacheService.GetData<IEnumerable<Users>>("Users");

            if (cacheData != null)
            {
                
            }

            return View();
        }

        [Authorize]
        public IActionResult Index2()
        {
            return Json(UserId);
        }
    }
}
