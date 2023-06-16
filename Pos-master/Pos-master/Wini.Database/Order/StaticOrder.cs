using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database
{
    
    public class StaticOrder
    {
        public int I { get; set; }
        public int? Value { get; set; }
    }
}
