using MilsatIMS.Enums;

namespace MilsatIMS.ViewModels
{
    public class AuthResponseDTO
    {
        public bool Success { get; set; } = false;
        public ResponseCode responseCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenExpires { get; set; }

    }
}
