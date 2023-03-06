using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Interns;
using MilsatIMS.ViewModels.Users;

namespace MilsatIMS.Interfaces
{
    public interface IAuthentication
    {
        User RegisterPassword(User request, string phoneNumber);
        Task<AuthResponseDTO> Login(UserLoginDTO request);
        Task<ForgotPasswordResponse> ForgotPassword(ForgetPasswordVm request);
        Task<ForgotPasswordResponse> ResetPassword(ResetPasswordVm request);
        Task<AuthResponseDTO> RefreshToken(string _token);
        //void SetRefreshToken(RefreshToken refreshToken, User user);
    }
}
