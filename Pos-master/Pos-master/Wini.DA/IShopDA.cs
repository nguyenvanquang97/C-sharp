// ---------------------------------------------------
// <copyright file="IShopDA.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

namespace Wini.DA
{
    using System.Collections.Generic;
    using System.Linq;
    using Wini.Database;
    using Wini.Simple;
    using Wini.Utils;

    /// <summary>
    /// IShopDA.
    /// </summary>
    public interface IShopDa
    {
        /// <summary>
        /// Gets all customer.
        /// </summary>
        /// <returns>danh sách user.</returns>
        IList<ShopItem> GetAll();

        /// <summary>
        /// login.
        /// </summary>
        /// <param name="users">thông tin user.</param>
        /// <returns>trạng thái đăng nhập.</returns>
        BaseResponse<bool> Login(Users users);

        /// <summary>
        /// save.
        /// </summary>
        void Save();

        /// <summary>
        /// đăng ký.
        /// </summary>
        /// <param name="users">thông tin user.</param>
        /// <returns>trạng thái đăng ký.</returns>
        BaseResponse<bool> Registor(Users users);
    }

    /// <summary>
    /// implement IcustomerDA.
    /// </summary>
    public class ShopDa : IShopDa
    {
        readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopDa"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public ShopDa(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public BaseResponse<bool> Login(Users users)
        {
            users.UserName = users.UserName.ToLower();
            var userDb = _context.Users.FirstOrDefault(m => m.UserName == users.UserName);
            if (userDb == null)
            {
                return new BaseResponse<bool>() { Code = -1, Data = false, Message = "Tài khoản không tồn tại" };
            }

            var isVerify = PasswordUtils.VerifyHashedPassword(userDb.PassWord, users.PassWord);
            if (!isVerify)
            {
                return new BaseResponse<bool>() { Code = -2, Data = false, Message = "Mật khẩu không đúng" };
            }

            
            _context.Users.Add(users);
            return new BaseResponse<bool>() { Code = 200, Data = true };
        }

        /// <inheritdoc/>
        public IList<ShopItem> GetAll()
        {
            
            return _context.Shops.Where(m => m.Name != string.Empty).Select(m => new ShopItem()
            {
                Name = m.Name,
                Employments = m.Employments.Select(n => new EmploymentItem() { Name = n.Name }),
            }).ToList();
        }

        /// <inheritdoc/>
        public BaseResponse<bool> Registor(Users users)
        {
            if (_context.Users.Any(m => m.UserName == users.UserName))
            {
                return new BaseResponse<bool>() { Code = 0, Message = "Tài khoản đã tồn tại" };
            }

            users.UserName = users.UserName.ToLower();
            users.PassWord = PasswordUtils.HashPassword(users.PassWord);

            _context.Users.Add(users);

            return new BaseResponse<bool>() { Code = 1, Message = string.Empty, Data = false };
        }

        /// <inheritdoc/>
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
