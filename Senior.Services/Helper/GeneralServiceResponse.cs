using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senior.Services.Helper
{
    public class GeneralServiceResponse
    {
        public bool IsSuccess { get; }
        public string? ErrorMessage { get; }
        public object? SuccessMessage { get; }
        public StatusCodeEnum StatusCode { get; }

        private GeneralServiceResponse(bool isSuccess, string? errorMessage, object? additionalMessage, StatusCodeEnum statusCode = StatusCodeEnum.OK)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            SuccessMessage = additionalMessage;
            StatusCode = statusCode;
        }

        public static GeneralServiceResponse Success(StatusCodeEnum statusCode, object? additionalMessage = null)
        {
            return new GeneralServiceResponse(
                true,
                null,
                additionalMessage,
                statusCode);
        }

        public static GeneralServiceResponse Error(string errorMessage, StatusCodeEnum statusCode, object? additionalMessage = null)
        {
            return new GeneralServiceResponse(
                false,
                errorMessage,
                additionalMessage,
                statusCode);
        }
    }
}