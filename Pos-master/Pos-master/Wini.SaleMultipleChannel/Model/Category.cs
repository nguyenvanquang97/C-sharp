using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SellMultipleChanel.Model
{

    public class LazadaCategoryReponse
    {
        [JsonProperty("children")]
        public List<LazadaCategoryReponse> Children { get; set; }

        [JsonProperty("var")]
        public bool Var { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("leaf")]
        public bool Leaf { get; set; }

        [JsonProperty("category_id")]
        public int CategoryId { get; set; }
    }
    public class CategorySuggestion
    {
        [JsonProperty("categoryName")]
        public string Name { get; set; }
        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }
    }

    public class DataCategorySuggestion
    {
        [JsonProperty("categorySuggestions")]
        public List<CategorySuggestion> CategorySuggestions { get; set; }
    }

    public class BaseLazadaReponse<T>
    {
        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("request_id")]
        public string RequestId { get; set; }
    }

}
