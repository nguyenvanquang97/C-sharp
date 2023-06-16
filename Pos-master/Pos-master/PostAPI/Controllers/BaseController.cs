// ---------------------------------------------------
// <copyright file="BaseController.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Pos.Controllers
{
    using System;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Options;
    using Wini.DA;
    using System.Linq;
    using PosAPI.Helpers;
    using Wini.DL;
    using PosAPI.Models;

    [TypeFilter(typeof(CustomAuthenticationAttribute))]
    public class BaseController : ControllerBase
    {

        /// <summary>
        /// Gets currentid.
        /// </summary>
        public string Username
        {
            get
            {
                return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
        public string UserId
        {
            get
            {
                return User.FindFirst("Id")?.Value;
            }
        }
        public int AgencyId
        {
            get
            {
                return int.Parse(User.FindFirst("AgencyId")?.Value ?? "0");
            }
        }

    }

    public class CustomAuthenticationAttribute : IAsyncAuthorizationFilter
    {
        private readonly IModuleDL _moduleDl;
        private readonly AppSetting _appSettings;
        private readonly IUserDA _userDA;
        public CustomAuthenticationAttribute(
            IModuleDL _moduleDl, IUserDA userDA, IOptions<AppSetting> appSettings)
        {
            this._moduleDl = _moduleDl;
            this._userDA = userDA;
            _appSettings = appSettings.Value;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!Guid.TryParse(context.HttpContext.User.FindFirst("Id")?.Value, out Guid userId))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

            // lay all role current user
            var lst = await _userDA.GetAllRold(userId, true);

            // kiem tra co role admin k
            if (lst.Any(m => _appSettings.RoldAdminIds.Any(n => n == m)))
            {
                return;
            }

            var action = context.RouteData.Values["action"].ToString();
            var controller = context.RouteData.Values["controller"].ToString();

            ActionType actionType;
            if (!Enum.TryParse<ActionType>(action, out actionType))
            {
                actionType = ActionType.View;
            }


            var permissions = await _moduleDl.GetPermissionByTagUserId(controller, userId);

            if (!permissions.Contains((int)actionType))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
        }
    }

}
