using MilsatInternAPI.Models;
using MilsatInternAPI.ViewModels;
using MilsatInternAPI.ViewModels.Interns;
using MilsatInternAPI.ViewModels.Users;

namespace MilsatInternAPI.Interfaces
{
    public interface IAuthentication
    {
        User RegisterPassword(User request, string phoneNumber);
        Task<AuthResponseDTO> Login(UserLoginDTO request);
        Task<ForgotPasswordResponse> ForgotPassword(ForgetPasswordVm request);
        Task<ForgotPasswordResponse> ResetPassword(ResetPasswordVm request);
        Task<AuthResponseDTO> RefreshToken();
        //void SetRefreshToken(RefreshToken refreshToken, User user);
    }
}
