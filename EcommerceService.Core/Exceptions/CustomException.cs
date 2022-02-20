using EcommerceService.Core.DTOs.Base;

namespace EcommerceService.Core.Exceptions;

public class CustomException : Exception
{
    public ErrorMessage ErrorMessage = new();

    public CustomException(string errorDescription,string errorCode) : base(errorDescription)
    {
        ErrorMessage.ErrorDescription = errorDescription;
        ErrorMessage.ErrorCode = errorCode;
    }
}