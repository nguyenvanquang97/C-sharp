// ---------------------------------------------------
// <copyright file="HomeController.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------


using Wini.Simple;

namespace Pos.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Data.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Pos.Models;
    using Wini.DA;
    using Wini.Database;

#pragma warning disable SA1600
    /// <summary>
    /// HomeController.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDA _userDa;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IUserDA userDa)
        {
            _logger = logger;
            _userDa = userDa;
        }

        public IActionResult Index(BaseRequest data)
        {
            return Json(1);
        }
        [HttpPost]
        public async Task<IActionResult> PrivacyAsync([FromBody] BaseRequest data)
        {
            var b = await _userDa.GetAll(data);
            return Json(b);
        }

        public async Task<IActionResult> Test(DataSourceLoadOptions data)
        {
            var b = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;


            return Json(b);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    [ModelBinder(BinderType = typeof(DataSourceLoadOptionsBinder))]
    public class DataSourceLoadOptions : DataSourceLoadOptionsBase
    {
    }

    public class DataSourceLoadOptionsBinder : IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var loadOptions = new DataSourceLoadOptions();
            DataSourceLoadOptionsParser.Parse(loadOptions, key =>
            {
                var d = bindingContext.ValueProvider.GetValue(key).FirstOrDefault();
                return bindingContext.ValueProvider.GetValue(key).FirstOrDefault();
            });
            bindingContext.Result = ModelBindingResult.Success(loadOptions);
            return Task.CompletedTask;
        }

    }
}
