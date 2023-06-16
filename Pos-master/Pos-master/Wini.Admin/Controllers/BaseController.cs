using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Wini.Admin.Models;
using Wini.DA;
using Wini.Database;
using Wini.DL;
using Wini.Simple.Identity;

namespace Wini.Admin.Controllers
{

    [TypeFilter(typeof(CustomAuthenticationAttribute))]
    public class BaseController<T> : Controller where T : BaseController<T>
    {

        public Guid UserId
        {
            get
            {
                if (User.FindFirst("Id") == null)
                {
                    return new Guid();
                }

                return Guid.Parse(User.FindFirst("Id")?.Value);
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