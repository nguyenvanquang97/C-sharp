// ---------------------------------------------------
// <copyright file="CustomerController.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------


using Wini.Database;

namespace Pos.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using DevExtreme.AspNet.Data;
    using DevExtreme.AspNet.Data.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Pos.Helpers;
    using Wini.DA;
    using Wini.Simple;

    public class UserController : BaseController
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IUserDA _userDa;
        private readonly AppSetting _appSettings;

        public UserController(ILogger<HomeController> logger, IUserDA userDa, IOptions<AppSetting> appSettings)
        {
            _logger = logger;
            _userDa = userDa;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost]
        public BaseResponse<string> Login([FromBody] Users user)
        {
            var b = _userDa.Login(user);
            if (b.Code != 200)
            {
                return new BaseResponse<string>() { Code = -1, Message = b.Message };
            }
            var token = GenerateToken(b.Data,_appSettings);
            return new BaseResponse<string>() { Code = 200, Message = token };
        }

        [HttpPost]
        public BaseResponse<bool> UserModule(Guid userId, string lstModule)
        {
            var data = _userDa.AddUserInModule(userId, lstModule);
            return data;
        }
        [HttpPost]
        public BaseResponse<bool> AddUserInModuleActive([FromBody] UserAddModuleActive model)
        {
            var data = _userDa.AddUserInModuleActive(model);
            return data;
        }



    }


}
