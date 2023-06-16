using System;
using System.Collections.Generic;
using Lazop.Api.Util;

namespace Wini.SaleMultipleChannel.Lazop
{
    /// <summary>
    /// Lazada Open Platform basic request.
    /// </summary>
    public class LazopRequest
    {
        /// <summary>
        /// API name
        /// </summary>
        private string apiName;

        /// <summary>
        /// API parameters
        /// </summary>
        private LazopDictionary apiParams;

        /// <summary>
        /// File parameters
        /// </summary>
        private IDictionary<string, FileItem> fileParams;
        /// <summary>
        /// HTTP header parameters
        /// </summary>
        private LazopDictionary headerParams;

        /// <summary>
        /// HTTP method: GET or POST, default is POST
        /// </summary>
        private string httpMethod = Constants.METHOD_POST;

        public LazopRequest()
        {

        }

        public LazopRequest(string apiName)
        {
            this.apiName = apiName;
        }

        public void AddApiParameter(string key, string value)
        {
            if (apiParams == null)
            {
                apiParams = new LazopDictionary();
            }
            apiParams.Add(key, value);
        }

        public void AddFileParameter(string key, FileItem file)
        {
            if (fileParams == null)
            {
                fileParams = new Dictionary<string, FileItem>();
            }
            fileParams.Add(key, file);
        }

        public void AddHeaderParameter(string key, string value)
        {
            if (headerParams == null)
            {
                headerParams = new LazopDictionary();
            }
            headerParams.Add(key, value);
        }

        public string GetApiName()
        {
            return apiName;
        }

        public void SetApiName(string apiName)
        {
            this.apiName = apiName;
        }

        public string GetHttpMethod()
        {
            return httpMethod;
        }

        public void SetHttpMethod(string httpMethod)
        {
            this.httpMethod = httpMethod;
        }

        public IDictionary<string, string> GetParameters()
        {
            if (apiParams == null)
            {
                apiParams = new LazopDictionary();
            }
            return apiParams;
        }

        public IDictionary<string, FileItem> GetFileParameters()
        {
            return fileParams;
        }

        public IDictionary<string, string> GetHeaderParameters()
        {
            return headerParams;
        }
    }
}
