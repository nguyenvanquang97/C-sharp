// ---------------------------------------------------
// <copyright file="CustomerDA.cs" company="Wini">
// Copyright (c) Wini. All rights reserved.
// author : phuocnh
// </copyright>
// ---------------------------------------------------

using Wini.Simple;

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
    using Wini.Database;
    using Wini.Simple;
    using Wini.Utils;

    /// <summary>
    /// ICustomerDa.
    /// </summary>
    public interface IModuleDA
    {
        /// <summary>
        /// Gets all customer.
        /// </summary>
        /// <returns>danh sách user.</returns>
        Task<BaseResponse<IList<Users>>> GetAll(BaseRequest request);

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

        Task<BaseResponse<bool>> UpdateUserPermission(AddPermissionItem data);

        void Add(Module module);
        BaseResponse<bool> AddRole(Roles module);
        BaseResponse<bool> RoleAddModule(int roleId, List<RoleAddModuleItem> module);
        Task<BaseResponse<bool>> UserAddRoleAsync(Guid userId, List<Roles> roles);

        Task<List<int>> GetPermissionByTagUserId(string tag, Guid userId, List<int> roldIds);

    }

    /// <summary>
    /// implement IcustomerDA.
    /// </summary>
    public class ModuleDA : IModuleDA
    {
        readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;
        private readonly DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(30.0);
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDA"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public ModuleDA(ApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
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
        public async Task<BaseResponse<IList<Users>>> GetAll(BaseRequest request)
        {
            var source = _context.Users
              .Select(s => new Users()
              {
                  Id = s.Id,
                  UserName = s.UserName
              });
            var query = await DataSourceLoader.LoadAsync(source, request.LoadOptions);
            BaseResponse<IList<Users>> response = new BaseResponse<IList<Users>>();
            response.Data = query.data.Cast<Users>().ToList();
            response.TotalCount = query.totalCount;
            return response;
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

            return new BaseResponse<bool>() { Code = 1, Message = "", Data = false };
        }

        /// <inheritdoc/>
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<BaseResponse<bool>> UpdateUserPermission(AddPermissionItem data)
        {
            var current = _context.UserModuleActives.FirstOrDefault(m => m.UserId == data.UserId && m.ModuleId == data.ModuleId && m.ActiveId == data.ActiveId);
            if (current == null)
            {
                _context.UserModuleActives.Add(new UserModuleActive() { ModuleId = data.ModuleId, UserId = data.UserId, ActiveId = data.ActiveId, Check = data.Check });
            }
            else
            {
                current.Check = data.Check;
            }

            await _context.SaveChangesAsync();
            return BasiResponse.Success(true);
        }

        public void Add(Module module)
        {
            _context.Modules.Add(module);
        }
        public BaseResponse<bool> AddRole(Roles module)
        {
            if (_context.Roles.Any(m => m.LoweredRoleName == module.LoweredRoleName))
            {
                return new BaseResponse<bool>() { Code = 1, Message = "Role name is exist" };
            }
            _context.Roles.Add(module);

            return new BaseResponse<bool>() { Code = 200 };
        }

        public BaseResponse<bool> RoleAddModule(int roleId, List<RoleAddModuleItem> modules)
        {
            var query = _context.RoleModuleActives.Where(m => m.RoleId == roleId);
            var b = query.ToQueryString();
            var lstModuleActiveDb = _context.RoleModuleActives.Where(m => m.RoleId == roleId).ToList();


            foreach (var module in modules)
            {
                var pemission = lstModuleActiveDb.FirstOrDefault(m => m.RoleId == roleId && m.ModuleId == module.ModuleId && m.ActiveId == module.ActiveId);

                // add module nếu chưa có
                if (pemission == null)
                {
                    _context.RoleModuleActives.Add(new RoleModuleActive()
                    {
                        RoleId = roleId,
                        ModuleId = module.ModuleId,
                        ActiveId = module.ActiveId,
                        Check = module.Check
                    });
                }
                else // update field check 
                {
                    pemission.Check = module.Check;
                }
            }
            _context.SaveChanges();
            return BasiResponse.Success(true);
        }

        public async Task<BaseResponse<bool>> UserAddRoleAsync(Guid userId, List<Roles> roles)
        {
            //get all role of user
            var roleDb = _context.UserInRoles.Where(m => m.UserId == userId).ToList();


            foreach (var role in roles)
            {
                var roleUserCurrent = roleDb.FirstOrDefault(m => m.RoleId == role.Id);

                // add role nếu chưa có
                if (roleUserCurrent == null)
                {
                    _context.UserInRoles.Add(new UserInRole()
                    {
                        UserId = userId,
                        RoleId = role.Id,
                        IsDelete = false,
                        DateCreated = DateTime.Now.TotalSeconds()
                    });
                }
                else // neu co roi update isdeleted = false
                {
                    roleUserCurrent.IsDelete = false;
                }
            }

            // remove role old
            var roleNeedRemove = roleDb.Where(m => !roles.Any(n => n.Id == m.RoleId)).ToList();
            foreach (var item in roleNeedRemove)
            {
                item.IsDelete = true;
            }

            await _context.SaveChangesAsync();
            return BasiResponse.Success(true);
        }

        public async Task<List<int>> GetPermissionByTagUserId(string tag, Guid userId, List<int> roldIds)
        {
            var module = await _context.Modules.Include(m => m.Children).FirstOrDefaultAsync(m => m.Link == tag && m.IsDelete == false);

            // lay all module theo role support 2 tầng cha con
            //var moduleChild = module.Children.Select(m => m.Id).ToList();

            // không có mudule name trả vê []
            if (module == null)
            {
                return new List<int>();
            }

            var query = from m in _context.RoleModuleActives
                        where m.Check == true && m.ModuleId == module.Id && roldIds.Contains(m.RoleId)
                        select new { m.ModuleId, m.ActiveId };
            var moduleUser = query.ToList();
            // permission with tag
            var moduleWithTag = moduleUser.Where(m => m.ModuleId == module.Id).Select(m => m.ActiveId).ToList();

            // add permission with child of tag
            //if (moduleUser.Any(m => m.ModuleId != module.Id))
            //{
            //    moduleWithTag.Add((int)ActionType.View);
            //}

            // get all permission config for user
            query = from m in _context.UserModuleActives
                    where m.ModuleId == module.Id && m.UserId == userId
                    select new { m.ModuleId, m.ActiveId };

            var allUserPermissions = query.ToList();

            // add all permission config for user
            moduleWithTag.AddRange(allUserPermissions.Where(m => m.ModuleId == module.Id).Select(m => m.ActiveId).ToList());

            //if (allUserPermissions.Any(m => m.ModuleId != module.Id))
            //{
            //    moduleWithTag.Add(5);
            //}

            return moduleWithTag.Distinct().ToList();
        }
    }
}
