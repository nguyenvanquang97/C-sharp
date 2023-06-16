// ---------------------------------------------------
// <copyright file="CustomerDA.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Wini.DA
{
    using DevExtreme.AspNet.Data;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Wini.DA;
    using Wini.DA.Cache;
    using Wini.Database;
    using Wini.Simple;
    using Wini.Utils;

    /// <summary>
    /// ICustomerDa.
    /// </summary>
    public interface IRoleDA
    {
        /// <summary>
        /// save.
        /// </summary>
        void Save();

        /// <summary>
        /// đăng ký.
        /// </summary>
        /// <param name="Roles">thông tin Role.</param>
        /// <returns>trạng thái đăng ký.</returns>
        BaseResponse<bool> Add(Roles Roles);
    }

    /// <summary>
    /// implement IcustomerDA.
    /// </summary>
    public class RoleDA : IRoleDA
    {
        readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDA"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public RoleDA(ApplicationDbContext context)
        {
            _context = context;
        }



        /// <inheritdoc/>
        public BaseResponse<bool> Add(Roles roles)
        {
            try
            {
                _context.Roles.Add(roles);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return BasiResponse.Error(false);
            }
            return BasiResponse.Success(true);
        }

        /// <inheritdoc/>
        public void Save()
        {
            _context.SaveChanges();
        }


    }
}
