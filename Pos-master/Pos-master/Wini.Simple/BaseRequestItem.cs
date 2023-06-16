using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExtreme.AspNet.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Wini.Simple
{
   
    public class BaseRequest
    {
        public DataSourceLoadOptionsBase LoadOptions { get; set; }
    }  
}
