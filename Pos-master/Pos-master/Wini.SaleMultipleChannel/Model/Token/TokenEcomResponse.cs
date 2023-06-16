using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Model.Token
{
    public class TokenEcomResponse
    {
       
        public string AccessToken { get; set; }
  
        public string Country { get; set; }

        public string RefreshToken { get; set; }
    
        public string AccountId { get; set; }
   
        public string Code { get; set; }
   
        public string AccountPlatform { get; set; }

        public string RefreshExpiresIn { get; set; }
       
        public List<CountryUserInfo> CountryUserInfo { get; set; }
    
        public string ExpiresIn { get; set; }
    
        public string Request_id { get; set; }

        public string Account { get; set; }
    }
}
