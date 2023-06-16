using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class UnitItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
