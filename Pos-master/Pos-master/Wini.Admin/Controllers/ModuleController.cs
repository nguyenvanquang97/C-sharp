using Microsoft.AspNetCore.Mvc;
using Wini.DA;
using Wini.Database;
using Wini.Simple;
using Wini.Simple.Identity;

namespace Wini.Admin.Controllers
{
    public class ModuleController : BaseController<ModuleController>
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IModuleDA _moduleDA;

        public ModuleController(ILogger<HomeController> logger, IModuleDA moduleDA) 
        {
            _moduleDA = moduleDA;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddPermission([FromBody] AddPermissionItem data)
        {
            return Json(data);
        }

        [HttpPost]
        public IActionResult AddModule([FromBody] Module data)
        {
            _moduleDA.Add(data);
            _moduleDA.Save();
            return Json(BasiResponse.Success(string.Empty));
        }


        [HttpPost]
        public IActionResult AddRole([FromBody] Roles data)
        {
            data.LoweredRoleName = data.RoleName.ToLower();
            data.IsDeleted = false;
            var rs = _moduleDA.AddRole(data);

            if (rs.Code != 200)
            {
                return Json(rs);
            }

            _moduleDA.Save();
            return Json(BasiResponse.Success(string.Empty));
        }

        [HttpPost]
        public IActionResult RoleAddModule([FromBody] List<RoleAddModuleItem> modules, int id)
        {
            _moduleDA.RoleAddModule(id, modules);
            return Json(BasiResponse.Success(string.Empty ));
        }

        [HttpPost]
        public async Task<IActionResult> UserAddRole([FromBody] List<Roles> roles, Guid id)
        {
            await _moduleDA.UserAddRoleAsync(id, roles);
            return Json(BasiResponse.Success(string.Empty));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserPermission([FromBody] AddPermissionItem data)
        {
            await _moduleDA.UpdateUserPermission(data);
            return Json(BasiResponse.Success(string.Empty));
        }

    }
}