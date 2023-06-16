using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class RoleAddModuleItem
    {
        public int ModuleId { get; set; }
        public int ActiveId { get; set; }
        public bool Check { get; set; }

    }

    public class UserAddModuleActive
    {
        public Guid UserId { get; set; }
        public List<ModuleActive> ModuleActiveList { get; set; }
    }

    public class ModuleActive
    {
        public int ModuleId { get; set; }
        public string action { get; set; }
    }
}
