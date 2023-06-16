using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.Response.Lazada.Product
{
    public class LazadaProductResponse
    {
        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }
        [JsonProperty("updated_time")]
        public string UpdatedTime { get; set; }
        [JsonProperty("images")]
        public List<string> Images { get; set; }
        [JsonProperty("skus")]
        public List<LazadaSku> Skus { get; set; }
        [JsonProperty("item_id")]
        public long ItemId { get; set; }
        [JsonProperty("trialProduct")]
        public bool TrialProduct { get; set; }
        [JsonProperty("primary_category")]
        public int PrimaryCategory { get; set; }
        [JsonProperty("marketImages")]
        public List<object> MarketImages { get; set; }
        [JsonProperty("attributes")]
        public LazadaAttributesResponse Attributes { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
