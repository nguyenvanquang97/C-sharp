using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Token
{
    public class LazadaTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("account_id")]
        public string AccountId { get; set; }
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("account_platform")]
        public string AccountPlatform { get; set; }
        [JsonProperty("refresh_expires_in")]
        public string RefreshExpiresIn { get; set; }
        [JsonProperty("country_user_info")]
        public List<LazadaCountryUserInfo> CountryUserInfo { get; set; }
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        [JsonProperty("request_id")]
        public string Request_id { get; set; }
        [JsonProperty("account")]
        public string Account { get; set; }
    }
}
