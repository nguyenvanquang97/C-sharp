using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Wini.Database.Multipe_Channel
{
    [Table("ShopAppEcommerce")]
    public class ShopAppEcommerce
    {
        public int Id { get; set; }
        public int AppId { get; set; }
        public string ShopId { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public int AgencyId { get; set; }
        public string? ShopName { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenExprided { get; set; }
        public DateTime? RefreshTokenExprided { get; set; }
       
    }
}
