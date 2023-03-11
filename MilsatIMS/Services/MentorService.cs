using Microsoft.EntityFrameworkCore;
using MilsatIMS.Common;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Interns;
using MilsatIMS.ViewModels.Mentors;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MilsatIMS.Services
{
    public class MentorService : IMentorService
    {
        private readonly ILogger<MentorService> _logger;
        private readonly IAsyncRepository<User> _userRepo;
        private readonly IAsyncRepository<Mentor> _mentorRepo;
        private readonly IAsyncRepository<InternMentorSession> _imsRepo;
        private readonly IAsyncRepository<Session> _sessionRepo;
        private readonly IAuthentication _authService;
        private readonly IConfiguration _iconfig;

        public MentorService(IConfiguration iconfig, IAsyncRepository<Mentor> mentorRepo,
            ILogger<MentorService> logger, IAuthentication authService, IAsyncRepository<User> userRepo,
            IAsyncRepository<InternMentorSession> imsRepo, IAsyncRepository<Session> sessionRepo)
        {
            _mentorRepo = mentorRepo;
            _logger = logger;
            _authService = authService;
            _userRepo = userRepo;
            _iconfig = iconfig;
            _imsRepo = imsRepo;
            _sessionRepo = sessionRepo;
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

        public async Task<GenericResponse<MentorResponseDTO>> AddMentor(CreateMentorVm vm)
        {
            _logger.LogInformation($"Received a request to add new Mentor(s): Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var _sessionId = await CheckSession(null);
                if (_sessionId == null)
                {
                    return new GenericResponse<MentorResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "You can not add a new mentor when there are no ongoing sessions"
                    };
                }

                var user = await _userRepo.GetAll().Where(x => x.Email == vm.Email).FirstOrDefaultAsync();
                if (user != null)
                {
                    return new GenericResponse<MentorResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "User with this Email already exists"
                    };
                }

                var newUser = new User
                {
                    Email = vm.Email,
                    Role = RoleType.Mentor,
                    FullName = vm.FullName,
                    Gender = vm.Gender,
                    PhoneNumber = vm.PhoneNumber,
                    Team = vm.Team
                };

                newUser = _authService.RegisterPassword(newUser, vm.PhoneNumber);
                await _userRepo.AddAsync(newUser);

                var singleMentor = new Mentor { UserId = newUser.UserId };
                singleMentor.UserId = newUser.UserId;
                await _mentorRepo.AddAsync(singleMentor);

                var newMentors = MentorResponseData(newUser, new List<Intern>{}, (Guid)_sessionId);
                return new GenericResponse<MentorResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Mentor has been successfully created.",
                    Data = newMentors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while creating mentor. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<MentorResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while creating mentor"
                };
            }
        }

        public async Task<GenericResponse<List<MentorResponseDTO>>> GetAllMentors(Guid? sessionid, int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to fetch all mentors with : Pagination - (pageNumber):{pageNumber}, (pageSize):{pageSize}");
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<MentorResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };
                 
                var pagedData = await _mentorRepo.GetAll()
                    .Include(u => u.User)
                    .Include(m => m.IMS.Where(y => y.SessionId == (Guid)_sessionId))
                        .ThenInclude(i => i.Intern).ThenInclude(j => j.User)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var allMentors = MentorResponseData(pagedData, (Guid)_sessionId);
                return new GenericResponse<List<MentorResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = allMentors,
                    Message = "Successfully fetched all mentors"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<MentorResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while fetching data request"
                };
            }
        }

        public async Task<GenericResponse<List<MentorResponseDTO>>> GetMentorById(Guid? sessionid, Guid id)
        {
            _logger.LogInformation($"Received a request to fetch a Mentor: Request(user id):{id}");
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<MentorResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var mentor = await _mentorRepo.GetAll()
                    .Where(x => x.UserId == id)
                    .Include(u => u.User)
                    .Include(m => m.IMS.Where(y => y.SessionId == (Guid)_sessionId))
                        .ThenInclude(i => i.Intern).ThenInclude(j => j.User)
                    .ToListAsync();

                if (mentor == null)
                {
                    return new GenericResponse<List<MentorResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "This mentor does not exist"
                    };
                }
                return new GenericResponse<List<MentorResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = MentorResponseData(new List<Mentor>(mentor), (Guid)_sessionId)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<MentorResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while fetching data request"
                };
            }
        }

        public async Task<GenericResponse<List<MentorResponseDTO>>> GetMentors(GetMentorVm vm, Guid? sessionid, int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation($"Received a request to Fetch Intern(s): Request:{JsonConvert.SerializeObject(vm)}");
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<MentorResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var filtered = await _mentorRepo.GetAll()
                    .Where(m => m.User.Role == RoleType.Mentor 
                    && (vm.name == null || m.User.FullName.Contains(vm.name))
                    && (vm.Team == null || m.User.Team == vm.Team))
                    .Include(u => u.User)
                    .Include(m => m.IMS.Where(y => y.SessionId == (Guid)_sessionId))
                        .ThenInclude(i => i.Intern).ThenInclude(j => j.User)
                    .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

                var mentors = MentorResponseData(filtered, (Guid)_sessionId);
                return new GenericResponse<List<MentorResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully searched for mentors",
                    Data = mentors
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<MentorResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public async Task<GenericResponse<MentorResponseUpdateDTO>> UpdateMentor(UpdateMentorVm vm)
        {
            _logger.LogInformation($"Received a request to update Mentor: Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var _sessionId = await CheckSession(null);
                if (_sessionId == null)
                    return new GenericResponse<MentorResponseUpdateDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "You can not update when there is no ongoing session"
                    };

                var mentor = await _userRepo.GetAll()
                    .Include(x => x.Mentor)
                    .Where(x => x.UserId == vm.MentorId)
                    .FirstOrDefaultAsync();

                if (mentor == null)
                {
                    return new GenericResponse<MentorResponseUpdateDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "User not found"
                    };
                }

                var IMS = await _imsRepo.GetAll().Where(ims => ims.MentorId == mentor.Mentor.MentorId && ims.SessionId == (Guid)_sessionId).ToListAsync();
                if (IMS == null)
                {
                    return new GenericResponse<MentorResponseUpdateDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "Something is not right about your update request"
                    };
                }

                mentor.FullName = vm.FullName;
                mentor.Email = vm.Email;
                mentor.PhoneNumber = vm.PhoneNumber;
                if (vm.Team != mentor.Team)
                {
                    foreach (var ims in IMS)
                    {
                        ims.MentorId = null;
                    }
                    await _imsRepo.UpdateRangeAsync(IMS);
                }

                await _userRepo.UpdateAsync(mentor);

                var updatedMentor = new MentorResponseUpdateDTO
                {
                    UserId = mentor.UserId,
                    FullName = mentor.FullName,
                    Email = mentor.Email,
                    PhoneNumber = mentor.PhoneNumber,
                    Gender = mentor.Gender,
                    Bio = mentor.Bio,
                    Team = mentor.Team,
                    ProfilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], mentor.ProfilePicture),
                };
                return new GenericResponse<MentorResponseUpdateDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = updatedMentor,
                    Message = "The mentor has been updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while updating intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<MentorResponseUpdateDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public List<MentorResponseDTO> MentorResponseData(List<Mentor> mentors, Guid sessionid)
        {
            var allMentors = new List<MentorResponseDTO>();
            foreach (Mentor mentor in mentors)
            {
                var interns = AssignedIntern(mentor.IMS);
                string profilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], mentor.User.ProfilePicture);
                var mentordto = new MentorResponseDTO
                {
                    UserId = mentor.UserId,
                    Email = mentor.User.Email,
                    FullName = mentor.User.FullName,
                    PhoneNumber = mentor.User.PhoneNumber,
                    Team = mentor.User.Team,
                    Gender = mentor.User.Gender,
                    Bio = mentor.User.Bio,
                    ProfilePicture = profilePicture,
                    Interns = interns,
                    SessionId = sessionid
                };
                allMentors.Add(mentordto);
            }
            return allMentors;
        }

        public MentorResponseDTO MentorResponseData(User mentor, List<Intern> interns, Guid sessionid)
        {
            var internsdto = AssignedIntern(interns);
            var mentordto = new MentorResponseDTO
            {
                UserId = mentor.UserId,
                Email = mentor.Email,
                FullName = mentor.FullName,
                PhoneNumber = mentor.PhoneNumber,
                Team = mentor.Team,
                Gender = mentor.Gender,
                Bio = mentor.Bio,
                ProfilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], mentor.ProfilePicture),
                Interns = internsdto,
                SessionId = sessionid
            };
            return mentordto;
        }

        public static List<InternMiniDTO> AssignedIntern(List<Intern> interns)
        {
            List<InternMiniDTO> internIDs = new();
            foreach (var intern in interns)
            {
                var _intern = new InternMiniDTO { UserId = intern.UserId, FullName = intern.User.FullName };
                internIDs.Add(_intern );
            }
            return internIDs;
        }

        public static List<InternMiniDTO> AssignedIntern(List<InternMentorSession> IMS)
        {
            List<InternMiniDTO> internIDs = new();
            foreach (var ims in IMS)
            {
                var _intern = new InternMiniDTO { UserId = ims.Intern.User.UserId, FullName = ims.Intern.User.FullName };
                internIDs.Add(_intern);
            }
            return internIDs;
        }
    }
}
