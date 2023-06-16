// ---------------------------------------------------
// <copyright file="CustomerController.cs" company="Wini">
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
    using DocumentFormat.OpenXml.Office2010.Excel;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Pos.Helpers;
    using Wini.DA;
    using Wini.Database;
    using Wini.Simple;

    public class CustomerController : BaseController
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ICustomerDa _CustomerDa;

        public CustomerController(ILogger<HomeController> logger, ICustomerDa CustomerDa)
        {
            _logger = logger;
            _CustomerDa = CustomerDa;
        }

        [HttpPost]
        public async Task<BaseResponse<IList<CustomerItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _CustomerDa.GetListSimpleByRequest(request, AgencyId);
            return model;
        }
        [HttpPost]
        public BaseResponse<CustomerItem> GetbyId(int id)
        {
            var result = _CustomerDa.GetbyCustomerItem(id);
            return result;
        }

        [HttpPost]
        public BaseResponse<Customer> Add(Customer item)
        {
            try
            {
                item.IsDelete = false;
                item.DateCreated = DateTime.Now.TotalSeconds();
                item.IsActive = true;

                _CustomerDa.Add(item);
                _CustomerDa.Save();
                return BasiResponse.Success(item);

            }
            catch (Exception e)
            {
                return BasiResponse.Error(new Customer());

            }

        }
        [HttpPost]
        public BaseResponse<Customer> Update(Customer item)
        {
            try
            {
                _CustomerDa.Save();
                return BasiResponse.Success(item);

            }
            catch (Exception e)
            {
                return BasiResponse.Error(new Customer());

            }

        }
        [HttpPost]
        public BaseResponse<int> Delete(int id)
        {
            try
            {
                var customer = _CustomerDa.GetbyId(id);
                customer.IsDelete = true;
                _CustomerDa.Save();
                return BasiResponse.Success(id);
            }
            catch (Exception e)
            {
                return BasiResponse.Error(0);
            }

        }


    }


}
