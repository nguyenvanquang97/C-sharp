﻿using System;
using System.Runtime.Serialization;

namespace Wini.SaleMultipleChannel.Lazop
{
    /// <summary>
    /// Lazada Open Platform Client Exception.
    /// </summary>
    public class IopException : Exception
    {
        private string errorCode;
        private string errorMsg;

        public IopException()
            : base()
        {
        }

        public IopException(string message)
            : base(message)
        {
        }

        protected IopException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IopException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public IopException(string errorCode, string errorMsg)
            : base(errorCode + ":" + errorMsg)
        {
            this.errorCode = errorCode;
            this.errorMsg = errorMsg;
        }

        public string ErrorCode
        {
            get { return errorCode; }
        }

        public string ErrorMsg
        {
            get { return errorMsg; }
        }
    }
}
