using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using Wini.DA;
using Wini.DA.Cache;
using Wini.Simple;
using Wini.Utils;
using System.Collections.Generic;
using System.Globalization;
using System;
using Wini.Database;
using Wini.DA;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace Pos.Controllers
{
    public class AgencyController : BaseController
    {
        private readonly ILogger<AgencyController> _logger;
        private IAgencyDa _agencyDa;
        private IUserDA _userDa;
        private IRoleDA _iRoleDa;
        private readonly ICacheService _cacheService;
        public AgencyController(ILogger<AgencyController> logger, IAgencyDa agencyDa, IUserDA userDa, IRoleDA iRoleDa)
        {
            _logger = logger;
            _agencyDa = agencyDa;
            _userDa = userDa;
            _iRoleDa = iRoleDa;
        }
        [HttpPost]
        public async Task<BaseResponse<IList<AgencyItem>>> GetAll([FromBody] BaseRequest request)
        {
            var model = await _agencyDa.GetListSimpleByRequest(request);
            return model;
        }

        [HttpPost]
        public async Task<BaseResponse<bool>> Add([FromBody] Agency data)
        {
            var date = DateTime.Now.TotalSeconds();
            data.IsDelete = false;
            data.CreateDate = date;
            _agencyDa.Add(data);
            _agencyDa.Save();
            if (data.GroupID > 0)
            {
                await _agencyDa.InsertDNModule(data.GroupID, data.Id, false);
            }
            var user = new Users()
            {
                Id = Guid.NewGuid(),
                PassWord = data.PassWord,
                UserName = data.UserName,
                Address = data.Address,
                LoweredUserName = data.Name,
                Email = data.Email,
                IsApproved = true,
                CreateDate = date,
                IsLockedOut = true,
                Mobile = data.Phone,
            };
            var checkAdd = _userDa.Registor(user);
            if (checkAdd.Code == (int)ResponseCode.Success)
            {
                var role = new Roles() { RoleName = "Admin", LoweredRoleName = "admin", AgencyID = data.Id, Description = "Quản trị" };
                var dnUsersInRoles = new UserInRole
                {
                    UserId = user.Id,
                    AgencyID = data.Id,
                    DateCreated = date,
                    IsDelete = false
                };
                role.userInRoles.Add(dnUsersInRoles);
                var result = _iRoleDa.Add(role);
                return result;

            }
            else
            {
                
                return checkAdd;
            }
        }

        [HttpPost]
        public async Task<BaseResponse<Agency>> Update([FromBody] Agency data)
        {
            var model = _agencyDa.GetbyId(data.Id);
            if (model != null)
            {
                try
                {
                    _agencyDa.Save();
                    if (data.GroupID > 0 && data.GroupID != model.GroupID)
                    {
                        await _agencyDa.InsertDNModule(data.GroupID, data.Id, false);
                    }

                    var user = _userDa.getUsersbyUsername(data.UserName);
                    if (user != null)
                    {
                        user.UserName = data.UserName;
                        user.Address = data.Address;
                        user.LoweredUserName = data.Name;
                        user.Email = data.Email;
                        user.Address = data.Address;
                        if (string.IsNullOrEmpty(data.PassWord))
                        {
                            user.PassWord = PasswordUtils.HashPassword(data.PassWord);
                        }
                        _userDa.Save();
                    }
                    
                    return BasiResponse.Success(data);

                }
                catch (Exception e)
                {
                    return BasiResponse.Error(new Agency());
                }
            }
            return BasiResponse.Nodata(new Agency());
        }

        [HttpPost]

        public BaseResponse<int> Delete(int id)
        {
            var model = _agencyDa.GetbyId(id);
            if (model != null)
            {
                model.IsDelete = true;
                _agencyDa.Save();
                return BasiResponse.Success(id);
            }

            return BasiResponse.Error(0, mes: "Dữ liệu không tồn tại");
        }

    }

}
