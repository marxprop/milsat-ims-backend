using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MilsatIMS.Data;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Interns;
using MilsatIMS.ViewModels.Users;
using MimeKit;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MilsatIMS.Services
{
    public class AuthService : IAuthentication
    {
        private readonly IConfiguration _iconfig;
        private readonly IAsyncRepository<User> _User;
        private readonly ILogger<AuthService> _logger;
        private readonly IEmailService _Email;
        private readonly IHttpContextAccessor _httpContext;

        public AuthService(
            IConfiguration config, IAsyncRepository<User> user, IEmailService emailService,
            ILogger<AuthService> logger, IHttpContextAccessor httpContext)
        {
            _iconfig = config;
            _logger = logger;
            _User = user;
            _httpContext = httpContext;
            _Email = emailService;
        }
        public async Task<AuthResponseDTO> Login(UserLoginDTO request)
        {
            try
            {
                _logger.LogInformation($"Received a request to login a user: Request:{JsonConvert.SerializeObject(request)}");
                var user = await _User.GetAll().Where(x => x.Email == request.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new AuthResponseDTO()
                    {
                        Success = false,
                        responseCode = ResponseCode.NotFound,
                        Message = "Either the email or password is incorrect"
                    };
                }
                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return new AuthResponseDTO()
                    {
                        Success = false,
                        responseCode = ResponseCode.INVALID_REQUEST,
                        Message = "Either the email or password is incorrect"
                    };
                }
                string token = CreateToken(user);
                var refreshToken = CreateRefreshToken();
                await SetRefreshToken(refreshToken, user);

                return new AuthResponseDTO
                {
                    Success = true,
                    responseCode = ResponseCode.Successful,
                    Token = token,
                    RefreshToken = refreshToken.Token,
                    TokenExpires = refreshToken.Expires
                };
            } 
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to login user. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new AuthResponseDTO()
                {
                    Success = false,
                    responseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Unable to login at this moment"
                };
            }
        }

        public async Task<AuthResponseDTO> RefreshToken(string _token)
        {
            try
            {
                //var refreshToken = _httpContext?.HttpContext?.Request.Cookies["refreshToken"];
                _logger.LogInformation($"Received a request to refresh user credentials: Request:{_token}");
                var user = await _User.GetAll().Where(u => u.RefreshToken == _token).FirstOrDefaultAsync();
                if (user == null)
                {
                    return new AuthResponseDTO { Success = false, Message = "Invalid Refresh Token" };
                }
                else if (user.TokenExpires < DateTime.UtcNow)
                {
                    return new AuthResponseDTO { Success = false, Message = "Token Expired." };
                }

                string token = CreateToken(user);
                var newRefreshToken = CreateRefreshToken();
                await SetRefreshToken(newRefreshToken, user);
                return new AuthResponseDTO
                {
                    Success = true,
                    Token = token,
                    RefreshToken = newRefreshToken.Token,
                    TokenExpires = newRefreshToken.Expires
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to refresh user credentials. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new AuthResponseDTO { Success = false, Message = "Unable to refresh user credentials at the moment" };
            }
        }

        public User RegisterPassword(User user, string defaultPassword)
        {
            CreatePasswordHash(defaultPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return user;
        }

        public async Task<ForgotPasswordResponse> ForgotPassword(ForgetPasswordVm request)
        {
            try
            {
                _logger.LogInformation($"Received a request to initialise forgot password: Request:{request}");
                var user = await _User.GetAll().Where(u => u.Email == request.Email).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.PasswordResetToken = CreateRandomToken();
                    user.PasswordTokenExpires = DateTime.UtcNow.AddDays(1);
                    await _User.UpdateAsync(user);
                    string subject = _iconfig.GetSection("ForgotPassword:subject").Value;
                    string body = $"{_iconfig.GetSection("ForgotPassword:body").Value} <a href={user.PasswordResetToken}>link</a></p>";
                    _Email.SendEmail(user.Email, subject, body);
                    //_Email.SendEmail("matthewoke.ai@gmail.com", subject, body);
                }
                return new ForgotPasswordResponse
                {
                    Success = true,
                    Message = "You can now reset your password by visiting your email address"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while initialising forgot password. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new ForgotPasswordResponse
                {
                    Success = false,
                    Message = "Error occured while trying to initialise forgot password."
                };
            }
        }

        public async Task<ForgotPasswordResponse> ResetPassword(ResetPasswordVm request)
        {
            try
            {
                _logger.LogInformation($"Received a request to Reset User Password: Request:{JsonConvert.SerializeObject(request)}");
                var user = await _User.GetAll().Where(u => u.PasswordResetToken == request.Token).FirstOrDefaultAsync();
                if (user == null || user.PasswordTokenExpires < DateTime.Now)
                {
                    return new ForgotPasswordResponse
                    {
                        Success = false,
                        Message = "Invalid Password Reset Token"
                    };
                }

                user = RegisterPassword(user, request.Password);
                user.PasswordResetToken = CreateRandomToken(); // Allow a token to be used only once.
                await _User.UpdateAsync(user);
                return new ForgotPasswordResponse
                {
                    Success = true,
                    Message = "Password Reset Successful"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to reset Password. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new ForgotPasswordResponse
                {
                    Success = false,
                    Message = "Unsuccessful Password Reset"
                };
            }
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _iconfig.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5.00),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        private RefreshToken CreateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
            };
            return refreshToken;
        }

        private async Task SetRefreshToken(RefreshToken refreshToken, User user)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires
            };

            _httpContext?.HttpContext?.Response
                .Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;

            await _User.UpdateAsync(user);
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
