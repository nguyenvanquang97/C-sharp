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
    using Pos.Helpers;
    using Pos.Models;
    using Wini.DL;
    using System.Linq;
    using DocumentFormat.OpenXml.Bibliography;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Text;
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Authorization;
    using DocumentFormat.OpenXml.Office2010.ExcelAc;
    using System.Collections.Generic;
    using Wini.Database;
    using Wini.DA;

    [TypeFilter(typeof(CustomAuthenticationAttribute))]
    public class BaseController : Controller
    {
        private const string Issuer = "wini";

        private readonly AppSetting _appSettings;

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
        public int AgencyId
        {
            get
            {
                return int.Parse(User.FindFirst("AgencyId")?.Value ?? "0");
            }
        }
        public decimal PriceDebt
        {
            get
            {
                return decimal.Parse(User.FindFirst("PriceDebt")?.Value ?? "0");
            }
        }
        public string GenerateToken(Users user, AppSetting _appSetting)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSetting.JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim("Id",user.Id.ToString()),


            };

            if (user.Agency != null)
            {

                claims.Add(new Claim("AgencyId", user.Agency?.Id.ToString()));

                if (user.Agency.AgencyType != null)
                {
                    claims.Add(new Claim("PriceDebt", user.Agency?.AgencyType?.PriceDebt?.ToString()));
                }

            }
            var token = new JwtSecurityToken(Issuer,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);

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
            bool hasAllowAnonymous = context.ActionDescriptor.EndpointMetadata
                                .Any(em => em.GetType() == typeof(AllowAnonymousAttribute));

            if (hasAllowAnonymous) return;

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
