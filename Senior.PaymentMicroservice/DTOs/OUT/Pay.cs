using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.PaymentMicroservice.DTOs.OUT
{
    public class Pay
    {
        public Pay(bool isSuccess, string message, StatusCodeEnum statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public Boolean IsSuccess { get; }
        public string Message { get; }
        public StatusCodeEnum StatusCode { get; }
    }

    public enum StatusCodeEnum
    {
        OK = 200,
        BadRequest = 400,
        NotFound = 404
    }
}