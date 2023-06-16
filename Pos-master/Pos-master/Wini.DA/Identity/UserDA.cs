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
    public interface IUserDA
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
        BaseResponse<Users> Login(Users users);
        Users getUsersbyUsername(string username);
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
        BaseResponse<bool> AddUserInModule(Guid userId, string lstModule);
        BaseResponse<bool> AddUserInModuleActive(UserAddModuleActive moduleActive);

        Task<List<int>> GetAllRold(Guid id);
        Task<List<int>> GetAllRold(Guid id, bool isCache);
    }

    /// <summary>
    /// implement IcustomerDA.
    /// </summary>
    public class UserDA : IUserDA
    {
        readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;
        private readonly DateTimeOffset expirationTime = DateTimeOffset.Now.AddMinutes(30.0);

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDA"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public UserDA(ApplicationDbContext context, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _context = context;
        }

        /// <inheritdoc/>
        public BaseResponse<Users> Login(Users users)
        {
            users.UserName = users.UserName.ToLower();
            var userDb = _context.Users.Include(m => m.Agency).ThenInclude(m => m.AgencyType).FirstOrDefault(m => m.UserName == users.UserName);
            if (userDb == null)
            {

                return BasiResponse.Error(new Users(), "Tài khoản không tồn tại");

            }
            var isVerify = PasswordUtils.VerifyHashedPassword(userDb.PassWord, users.PassWord);
            if (!isVerify)
            {

                return BasiResponse.Error(new Users(), "Mật khẩu không đúng");

            }
            return BasiResponse.Success(userDb);

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
                return new BaseResponse<bool>() { Code = (int)ResponseCode.Error, Message = "Tài khoản đã tồn tại" };
            }

            users.UserName = users.UserName.ToLower();
            users.PassWord = PasswordUtils.HashPassword(users.PassWord);

            _context.Users.Add(users);

            return new BaseResponse<bool>() { Code = (int)ResponseCode.Success, Message = "", Data = false };
        }

        /// <inheritdoc/>
        public void Save()
        {
            _context.SaveChanges();
        }

        public async Task<List<int>> GetAllRold(Guid id)
        {
            return await _context.UserInRoles.Where(m => m.UserId == id && m.IsDelete == false).Select(m => m.RoleId.Value).ToListAsync();

        }
        public async Task<List<int>> GetAllRold(Guid id, bool isCache)
        {
            string key = string.Format("{0}-UserRole", id);
            var lstCache = new List<int>();

            if (isCache)
            {
                lstCache = _cacheService.GetData<List<int>>(key);

                if (lstCache != null && lstCache.Count() > 0)
                {
                    return lstCache;
                }
                lstCache = await GetAllRold(id);
                _cacheService.SetData(key, lstCache, expirationTime);

                return lstCache;
            }

            lstCache = await GetAllRold(id);
            return lstCache;
        }

        public BaseResponse<bool>
            AddUserInModule(Guid userId, string lstModule)
        {
            var model = _context.Users.FirstOrDefault(a => a.Id == userId);
            if (model != null)
            {
                model.UserModules.Clear();
                var lst = FdiUtils.StringToListInt(lstModule);
                var models = getModuleBylstId(lst);

                model.UserModules = models;
                return BasiResponse.Success(true);
            }
            return BasiResponse.Nodata(false);
        }

        public BaseResponse<bool> AddUserInModuleActive(UserAddModuleActive model)
        {
            try
            {
                var moduleActive = new List<UserModuleActive>();
                foreach (var module in model.ModuleActiveList)
                {
                    var action = FdiUtils.StringToListInt(module.action);
                    foreach (var i in action)
                    {
                        var item = new UserModuleActive()
                        {
                            ModuleId = module.ModuleId,
                            ActiveId = i,
                            Check = 1,
                            Active = true,
                            UserId = model.UserId,
                        };
                        moduleActive.Add(item);
                    }
                }
                _context.UserModuleActives.AddRange(moduleActive);
                _context.SaveChanges();
                return BasiResponse.Success(true);
            }
            catch (Exception e)
            {
                return BasiResponse.Error(false);
            }
        }


        public List<UserModule> getModuleBylstId(List<int> id)
        {
            return _context.UserModules.Where(m => id.Contains(m.Id)).ToList();
        }

        public Users getUsersbyUsername(string username)
        {
            var souce = from c in _context.Users
                        where c.UserName == username
                        select c;
            return souce.FirstOrDefault();
        }
    }
}
