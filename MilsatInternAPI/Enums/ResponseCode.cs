using System.ComponentModel;

namespace MilsatInternAPI.Enums
{
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
