using System.ComponentModel;

namespace MilsatIMS.Enums
{
    /// <summary>
    /// 0=Successful, 1=Not_Found, 2=Invalid_Request, 3=Exception_Error
    /// </summary>
    public enum ResponseCode
    {

    [Description("Success")]
    Successful = 00,
    [Description("Not Found")]
    NotFound = 01,
    [Description("Invalid Request")]
    INVALID_REQUEST = 02,
    [Description("Exception Occured")]
    EXCEPTION_ERROR = 03
    }
}
