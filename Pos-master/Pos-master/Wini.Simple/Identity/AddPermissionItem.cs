using System;

namespace Wini.Simple
{
    public class AddPermissionItem
    {
        public Guid UserId { get; set; }
        public int ModuleId { get; set; }
        public int ActiveId { get; set; }
        public int Check { get; set; }
    }

}
