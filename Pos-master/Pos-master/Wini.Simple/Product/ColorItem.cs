using System;
using System.Collections.Generic;
using System.Text;

namespace Wini.Simple
{
    public class ColorItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsDeleted { get; set; }

    }
}
