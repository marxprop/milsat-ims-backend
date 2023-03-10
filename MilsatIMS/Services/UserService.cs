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
        private readonly IAsyncRepository<Intern> _internRepo;
        private readonly ILogger<InternService> _logger;
        private readonly IConfiguration _iconfig;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IAsyncRepository<Session> _sessionRepo;
        private readonly IAsyncRepository<InternMentorSession> _imsRepo;

        public UserService(IConfiguration iconfig, IAsyncRepository<Session> sessionRepo, IAsyncRepository<InternMentorSession> imsRepo,
            ILogger<InternService> logger, IAsyncRepository<User> userRepo, IHttpContextAccessor httpContext, IAsyncRepository<Intern> internRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
            _iconfig = iconfig;
            _httpContext = httpContext;
            _sessionRepo = sessionRepo;
            _imsRepo = imsRepo;
            _internRepo = internRepo;
        }

        public async Task<Guid?> CheckSession(Guid? sessionId)
        {
            if (sessionId == null)
            {
                var _currentSession = await _sessionRepo.GetAll().Where(x => x.Status == Status.Current).SingleOrDefaultAsync();
                var id = _currentSession?.SessionId;
                return id;
            }
            else
            {
                var _currentSession = await _sessionRepo.GetAll().Where(x => x.SessionId == sessionId).SingleOrDefaultAsync();
                var id = _currentSession?.SessionId;
                return id;
            }
        }
        public async Task<GenericResponse<List<UserResponseDTO>>> GetAllUsers(Guid? sessionid, int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to fetch paginated User(s): Request: pageNumber:{pageNumber}, pageSize:{pageSize}");
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<UserResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var pagedData = await _userRepo.GetAll()
                    .Include(x => x.Intern.IMS.Where(y => y.SessionId == sessionid))
                    .Include(x => x.Mentor.IMS.Where(y => y.SessionId == sessionid))
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

        public async Task<GenericResponse<List<UserResponseDTO>>> GetUserById(Guid? sessionid, Guid id)
        {
            _logger.LogInformation($"Received a request to fetch a User: Request(user id):{id}");
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<UserResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var user = await _userRepo.GetAll()
                    .Include(x => x.Intern.IMS.Where(y => y.SessionId == sessionid))
                    .Include(x => x.Mentor.IMS.Where(y => y.SessionId == sessionid))
                    .Where(x => x.UserId == id).FirstOrDefaultAsync();
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

        public async Task<GenericResponse<List<UserResponseDTO>>> FilterUsers(GetUserVm vm, Guid? sessionid, int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to Fetch User(s): Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<UserResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var filtered = await _userRepo.GetAll().Include(x => x.Intern.IMS.Where(y => y.SessionId == sessionid))
                                                .Include(x => x.Mentor.IMS.Where(y => y.SessionId == sessionid))
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


        public async Task<GenericResponse<UserResponseDTO>> UpdateInternProfile([FromForm] UpdateInternVm vm)
        {
            _logger.LogInformation($"Received to update user profile: Request:{JsonConvert.SerializeObject(vm)}");
            try
            {

                var _sessionId = await CheckSession(null);
                if (_sessionId == null)
                    return new GenericResponse<UserResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "You can not update when there is no ongoing session"
                    };

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

                Guid user_id;
                Guid.TryParse(user_claim.Value, out user_id);

                var user = await _userRepo.GetAll()
                    .Include(x => x.Intern.IMS.Where(y => y.SessionId == user_id))
                    .Include(x => x.Mentor.IMS.Where(y => y.SessionId == user_id))
                    .Where(x => x.UserId == user_id).FirstOrDefaultAsync(); ;

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

                var intern = await _internRepo.GetAll()
                    .Include(x => x.IMS.Where(y => y.SessionId == _sessionId))
                    .Where(i => i.UserId == user.UserId).FirstOrDefaultAsync();

                if (intern == null)
                {
                    return new GenericResponse<UserResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "Intern was not found"
                    };
                }

                intern.CourseOfStudy = vm.CourseOfStudy;
                intern.Institution = vm.Institution;
                await _userRepo.UpdateAsync(user);
                await _internRepo.UpdateAsync(intern);

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

        public async Task<GenericResponse<UserResponseDTO>> UpdateMentorProfile([FromForm] UpdateMentorVm vm)
        {
            _logger.LogInformation($"Received to update user profile: Request:{JsonConvert.SerializeObject(vm)}");
            try
            {

                var _sessionId = await CheckSession(null);
                if (_sessionId == null)
                    return new GenericResponse<UserResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "You can not update when there is no ongoing session"
                    };

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

                Guid user_id;
                Guid.TryParse(user_claim.Value, out user_id);

                var user = await _userRepo.GetAll()
                    .Include(x => x.Intern.IMS.Where(y => y.SessionId == user_id))
                    .Include(x => x.Mentor.IMS.Where(y => y.SessionId == user_id))
                    .Where(x => x.UserId == user_id).FirstOrDefaultAsync(); ;

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
