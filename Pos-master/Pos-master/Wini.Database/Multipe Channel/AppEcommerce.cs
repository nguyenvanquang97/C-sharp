using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database.Multipe_Channel
{
    [Table("AppEcommerce")]
    public class AppEcommerce
    {
        public int Id {  get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public int AppType { get; set; }
    }
}
