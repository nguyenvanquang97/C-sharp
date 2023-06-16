#nullable enable
using System;

namespace Wini.Simple
{
    public class BaseResponse<T>
    {
        public T? Data;
        public int Code { get; set; } = 200;
        public string? Message { get; set; }
        public int TotalCount { get; set; }
    }
    public class BasiResponse
    {
        public static BaseResponse<T> Success<T>(T data , string mes = "Thao tác thành công.",int totalcount = 0)
        {
            return new BaseResponse<T>() { Code = (int)ResponseCode.Success, Message = mes, Data = data,TotalCount = totalcount};
        }
        public static BaseResponse<T> Error<T>(T data, string mes = "Có lỗi xảy ra.")
        {
            return new BaseResponse<T>() { Code = (int)ResponseCode.Error, Message = mes, Data = data };
        }
        public static BaseResponse<T> Nodata<T>(T data, string mes = "Dữ liệu không tồn tại.")
        {
            return new BaseResponse<T>() { Code = (int)ResponseCode.Nodata, Message = mes, Data = data };
        }
    }
    public enum ResponseCode
    {
        Success = 200,
        Nodata = 202,
        Error = 404
    }

}


