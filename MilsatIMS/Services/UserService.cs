using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilsatIMS.Common;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Users;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MilsatIMS.Services
{
    public class UserService : IUserService
    {
        private readonly IAsyncRepository<User> _userRepo;
        private readonly ILogger<InternService> _logger;
        private readonly IConfiguration _iconfig;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IConfiguration iconfig,
            ILogger<InternService> logger, IAsyncRepository<User> userRepo, IHttpContextAccessor httpContext)
        {
            _logger = logger;
            _userRepo = userRepo;
            _iconfig = iconfig;
            _httpContext = httpContext;
        }


        public async Task<GenericResponse<List<UserResponseDTO>>> GetAllUsers(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to fetch paginated User(s): Request: pageNumber:{pageNumber}, pageSize:{pageSize}");
            try
            {
                var pagedData = await _userRepo.GetAll()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var users = UserResponseData(pagedData);
                return new GenericResponse<List<UserResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = users
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<UserResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public async Task<GenericResponse<List<UserResponseDTO>>> GetUserById(Guid id)
        {
            _logger.LogInformation($"Received a request to fetch a User: Request(user id):{id}");
            try
            {
                var user = await _userRepo.GetByIdAsync(id);
                if (user == null)
                {
                    return new GenericResponse<List<UserResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound
                    };
                }
                return new GenericResponse<List<UserResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = UserResponseData(new List<User> { user })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Fetching User. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<UserResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public async Task<GenericResponse<List<UserResponseDTO>>> FilterUsers(GetUserVm vm, int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to Fetch User(s): Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var filtered = await _userRepo.GetAll()
                                                 .Where(x =>
                                                         (vm.name == null || x.FullName.Contains(vm.name))
                                                         && (vm.Team == null || x.Team == vm.Team)
                                                         && (vm.role == null || x.Role == vm.role))
                                                 .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                var users = UserResponseData(filtered);
                return new GenericResponse<List<UserResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = users
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<UserResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }


        public async Task<GenericResponse<UserResponseDTO>> UpdateProfile([FromForm] UpdateUserVm vm)
        {
            _logger.LogInformation($"Received to update user profile: Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var user_claim = _httpContext?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
                if (user_claim == null)
                {
                    return new GenericResponse<UserResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.EXCEPTION_ERROR,
                        Message = "Error occured while authenticating user"
                    };
                }

                string user_id = user_claim.Value;

                var user = await _userRepo.GetByIdAsync(Guid.Parse(user_id));

                if (user == null)
                {
                    return new GenericResponse<UserResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "Unsuccessful update."
                    };
                }

                if (vm.ProfilePicture.Length > 0)
                {
                    var fileName = user.ProfilePicture;
                    if (String.IsNullOrEmpty(fileName))
                    {
                        fileName = Path.GetRandomFileName(); 
                    }
                    Directory.CreateDirectory(_iconfig["ProfilePicturesPath"]);
                    var filePath = Path.Combine(_iconfig["ProfilePicturesPath"], fileName);

                    using (var stream = File.Create(filePath))
                    {
                        await vm.ProfilePicture.CopyToAsync(stream);
                    }
                    user.ProfilePicture = fileName;
                }

                user.Bio = vm.Bio;
                await _userRepo.UpdateAsync(user);

                return new GenericResponse<UserResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Update is Successful"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while updating user profile. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<UserResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public async Task<GenericResponse<UserResponseDTO>> RemoveUser(Guid id)
        {
            _logger.LogInformation($"Received a request to delete a User: Request(user id):{id}");
            try
            {
                var user = await _userRepo.GetByIdAsync(id);
                if (user == null)
                {
                    return new GenericResponse<UserResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound
                    };
                }

                await _userRepo.DeleteAsync(user);
                return new GenericResponse<UserResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Deleting User. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<UserResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        private List<UserResponseDTO> UserResponseData(List<User> pagedData)
        {
            List<UserResponseDTO> users = new();
            foreach (var user in pagedData)
            {
                string profilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], user.ProfilePicture);
                users.Add(new UserResponseDTO
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Bio = user.Bio,
                    ProfilePicture = profilePicture,
                    Team = user.Team,
                    Role = user.Role,
                });
            };
            return users;
        }
    }
}
