// ---------------------------------------------------
// <copyright file="NewsController.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------


using Wini.Utils;

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
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Pos.Helpers;
    using Wini.DA;
    using Wini.Database;
    using Wini.Simple;

    public class NewsController : BaseController
    {

        private readonly ILogger<HomeController> _logger;
        private readonly INewsDa _NewsDa;

        public NewsController(ILogger<HomeController> logger, INewsDa NewsDa)
        {
            _logger = logger;
            _NewsDa = NewsDa;
        }

        [HttpPost]
        public async Task<BaseResponse<IList<NewsItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _NewsDa.GetListSimpleByRequest(request);
            return model;
        }
        [HttpPost]
        public BaseResponse<NewsItem> GetbyId(int id)
        {
            var result = _NewsDa.GetbyNewsItem(id);
            return result;
        }

        [HttpPost]
        public BaseResponse<News> Add(News item)
        {
            try
            {
                item.IsDeleted = false;
                item.DateCreated = DateTime.Now;
                item.IsShow = true;

                _NewsDa.Add(item);
                _NewsDa.Save();
                return BasiResponse.Success(item);

            }
            catch (Exception e)
            {
                return BasiResponse.Error(new News());
            }

        }
        [HttpPost]
        public BaseResponse<News> Update(News item)
        {
            try
            {
                _NewsDa.Save();
                return BasiResponse.Success(item);

            }
            catch (Exception e)
            {
                return BasiResponse.Error(new News());

            }

        }
        [HttpPost]
        public BaseResponse<int> Delete(int id)
        {
            try
            {
                var News = _NewsDa.GetbyId(id);
                News.IsDeleted = true;
                _NewsDa.Save();
                return BasiResponse.Success(id);

            }
            catch (Exception e)
            {
                return BasiResponse.Error(0);

            }

        }
        

    }


}
