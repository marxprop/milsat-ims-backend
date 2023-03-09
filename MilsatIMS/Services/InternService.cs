using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilsatIMS.Common;
using MilsatIMS.Data;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Interns;
using MilsatIMS.ViewModels.Mentors;
using Newtonsoft.Json;

namespace MilsatIMS.Services
{
    public class InternService : IInternService
    {
        private readonly IAsyncRepository<Intern> _internRepo;
        private readonly IAsyncRepository<Mentor> _mentorRepo;
        private readonly IAsyncRepository<Session> _sessionRepo;
        private readonly IAsyncRepository<InternMentorSession> _imsRepo;
        private readonly IAsyncRepository<User> _userRepo;
        private readonly IAuthentication _authService;
        private readonly ILogger<InternService> _logger;
        private readonly IConfiguration _iconfig;
        public InternService(IAsyncRepository<Intern> internRepo, IAsyncRepository<Mentor> mentorRepo,
            ILogger<InternService> logger, IAuthentication authService, IAsyncRepository<User> userRepo,
            IConfiguration iconfig, IAsyncRepository<Session> sessionRepo, IAsyncRepository<InternMentorSession> imsRepo)
        {
            _internRepo = internRepo;
            _mentorRepo = mentorRepo;
            _userRepo = userRepo;
            _logger = logger;
            _authService = authService;
            _iconfig = iconfig;
            _sessionRepo = sessionRepo;
            _imsRepo = imsRepo;
        }

        public async Task<GenericResponse<InternResponseDTO>> AddIntern(CreateInternDTO request)
        {
            _logger.LogInformation($"Received a request to add new Intern(s): Request:{JsonConvert.SerializeObject(request)}");
            try
            {
                var user = await _userRepo.GetAll().Where(x => x.Email == request.Email).FirstOrDefaultAsync();
                if (user != null)
                {
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "User with this Email already exists"
                    };
                }

                var newUser = new User {
                    Email = request.Email, Role = RoleType.Intern,
                    FullName = request.FullName, Gender = request.Gender,
                    PhoneNumber = request.PhoneNumber, Team = request.Team
                };
                newUser = _authService.RegisterPassword(newUser, request.PhoneNumber);

                var newIntern = new Intern 
                { 
                    CourseOfStudy = request.CourseOfStudy,
                    Institution = request.Institution,
                };

                var ims = new InternMentorSession { };

                var selectedMentor = await SelectMentor(newUser.Team);
                if (selectedMentor != null)
                {
                    ims.MentorId = selectedMentor.UserId;
                }

                var session = await _sessionRepo.GetAll().Where(s => s.Status == Status.Current).SingleOrDefaultAsync();
                if (session == null)
                {
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.Successful,
                        Message = "You can't add an intern when there is no live session"
                    };
                }

                await _userRepo.AddAsync(newUser);

                newIntern.UserId = newUser.UserId;
                await _internRepo.AddAsync(newIntern);

                ims.InternId = newIntern.InternId;
                ims.SessionId = session.SessionId;
                await _imsRepo.AddAsync(ims);

                //Crete response body
                var newInterns = InternResponseData( newUser , selectedMentor, session.SessionId);
                return new GenericResponse<InternResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Intern has been created successfully",
                    Data = newInterns
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<InternResponseDTO>
                { 
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while creating intern"
                };
            }
        }

        public async Task<Mentor?> SelectMentor(TeamType Team)
        {
            var availableMentors = await _mentorRepo.GetAll().Include(x => x.User).Where(x => x.User.Team == Team).ToListAsync();
            int totalAvailableMentors = availableMentors.Count();
            if (totalAvailableMentors > 0)
            {
                Random random = new Random();
                var mentor_idx = random.Next(totalAvailableMentors);
                var mentor = availableMentors[mentor_idx];
                return mentor;
            }
            return null;
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

        public async Task<GenericResponse<List<InternResponseDTO>>> GetAllInterns(Guid? sessionId, int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to fetch paginated Intern(s): Request: pageNumber:{pageNumber}, pageSize:{pageSize}");
            try
            {
                var _sessionId = await CheckSession(sessionId);
                if (_sessionId==null) 
                    return new GenericResponse<List<InternResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };
                
                var pagedData = await _imsRepo.GetAll()
                    .Include(ims => ims.Intern)
                        .ThenInclude(intern => intern.User)
                    .Include(ims => ims.Mentor)
                        .ThenInclude(mentor => mentor.User)
                    .Where(ims => ims.SessionId == _sessionId)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(ims => new InternResponseDTO
                    {
                        UserId = ims.Intern.User.UserId,
                        Email = ims.Intern.User.Email,
                        PhoneNumber = ims.Intern.User.PhoneNumber,
                        FullName = ims.Intern.User.FullName,
                        Team = ims.Intern.User.Team,
                        CourseOfStudy = ims.Intern.CourseOfStudy,
                        Institution = ims.Intern.Institution,
                        Gender = ims.Intern.User.Gender,
                        SessionId = ims.SessionId,
                        Bio = ims.Intern.User.Bio,
                        ProfilePicture = ims.Intern.User.ProfilePicture,
                        Mentor = new MentorMiniDTO
                        {
                            MentorId = ims.Mentor.User.UserId,
                            FullName = ims.Mentor.User.FullName
                        }
                    })
                    .ToListAsync();

                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully fetched all interns",
                    Data = pagedData
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public async Task<GenericResponse<List<InternResponseDTO>>> GetInternById(Guid? sessionid, Guid id)
        {
            _logger.LogInformation($"Received a request to fetch an Intern: Request(user id):{id}");
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<InternResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var user = await _imsRepo.GetAll()
                   .Include(ims => ims.Intern)
                       .ThenInclude(intern => intern.User)
                   .Include(ims => ims.Mentor)
                       .ThenInclude(mentor => mentor.User)
                   .Where(ims => ims.SessionId == _sessionId && ims.Intern.UserId == id)
                   .Select(ims => new InternResponseDTO
                   {
                       UserId = ims.Intern.User.UserId,
                       Email = ims.Intern.User.Email,
                       PhoneNumber = ims.Intern.User.PhoneNumber,
                       FullName = ims.Intern.User.FullName,
                       Team = ims.Intern.User.Team,
                       CourseOfStudy = ims.Intern.CourseOfStudy,
                       Institution = ims.Intern.Institution,
                       Gender = ims.Intern.User.Gender,
                       SessionId = ims.SessionId,
                       Bio = ims.Intern.User.Bio,
                       ProfilePicture = ims.Intern.User.ProfilePicture,
                       Mentor = new MentorMiniDTO
                       {
                           MentorId = ims.Mentor.User.UserId,
                           FullName = ims.Mentor.User.FullName
                       }
                   })
                   .ToListAsync();

                if (user == null)
                {
                    return new GenericResponse<List<InternResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "Intern not found"
                    };
                }
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully found intern by id",
                    Data = user
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Fetching Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while fetching intern with id"
                };
            }

        }

        public async Task<GenericResponse<List<InternResponseDTO>>> FilterInterns(GetInternVm vm, Guid? sessionid, int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to Fetch Intern(s): Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var _sessionId = await CheckSession(sessionid);
                if (_sessionId == null)
                    return new GenericResponse<List<InternResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "There is no ongoing session or sessionId does not exist"
                    };

                var users = await _imsRepo.GetAll()
                   .Include(ims => ims.Intern)
                       .ThenInclude(intern => intern.User)
                   .Include(ims => ims.Mentor)
                       .ThenInclude(mentor => mentor.User)
                   .Where(ims => ims.SessionId == _sessionId &&
                         (ims.Intern.User.Role == RoleType.Intern)
                         && (vm.name == null || ims.Intern.User.FullName.Contains(vm.name))
                         && (vm.Team == null || ims.Intern.User.Team == vm.Team))
                   .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                   .Select(ims => new InternResponseDTO
                   {
                       UserId = ims.Intern.User.UserId,
                       Email = ims.Intern.User.Email,
                       PhoneNumber = ims.Intern.User.PhoneNumber,
                       FullName = ims.Intern.User.FullName,
                       Team = ims.Intern.User.Team,
                       CourseOfStudy = ims.Intern.CourseOfStudy,
                       Institution = ims.Intern.Institution,
                       Gender = ims.Intern.User.Gender,
                       SessionId = ims.SessionId,
                       Bio = ims.Intern.User.Bio,
                       ProfilePicture = ims.Intern.User.ProfilePicture,
                       Mentor = new MentorMiniDTO
                       {
                           MentorId = ims.Mentor.User.UserId,
                           FullName = ims.Mentor.User.FullName
                       }
                   }).ToListAsync();
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully filtered interns",
                    Data = users
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while fetching data request"
                };
            }
        }


        public async Task<GenericResponse<InternResponseDTO>> UpdateIntern(UpdateInternVm vm)
        {
            _logger.LogInformation($"Received a request to update Intern: Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var _sessionId = await CheckSession(null);
                if (_sessionId == null)
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "You can not update when there is no ongoing session"
                    };

                Guid _currentSessionId = (Guid)_sessionId;

                var user = await _userRepo.GetAll()
                    .Include(x => x.Intern)
                    .Where(x => x.UserId == vm.UserId && x.Role == RoleType.Intern)
                    .FirstOrDefaultAsync();


                if (user == null)
                {
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "The user you are trying to update was not found"
                    };
                }

                var ims = await _imsRepo.GetAll().Where(ims => ims.InternId == user.Intern.InternId && ims.SessionId == _currentSessionId).FirstOrDefaultAsync();
                if (ims == null)
                {
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "Something is not right about your update request"
                    };
                }

                user.Team = vm.Team;
                user.FullName = vm.FullName;
                user.Email = vm.Email;
                user.PhoneNumber = vm.PhoneNumber;

                if (vm.MentorId != Guid.Empty)
                {
                    var selectedMentor = await _userRepo.GetAll().Include(x => x.Mentor)
                                                                 .Where(x => x.UserId == vm.MentorId
                                                                 && x.Team == vm.Team
                                                                 && x.Role == RoleType.Mentor)
                                                                 .FirstOrDefaultAsync();
                    if (selectedMentor == null)
                    {
                        return new GenericResponse<InternResponseDTO>
                        {
                            Successful = false,
                            ResponseCode = ResponseCode.NotFound,
                            Message = "No Mentor with this Id was found"
                        };
                    }


                    ims.MentorId = selectedMentor.Mentor.MentorId;

                    await _userRepo.UpdateAsync(user);
                    await _imsRepo.UpdateAsync(ims);
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.Successful,
                        Data = InternResponseData(user, selectedMentor, _currentSessionId)
                    };
                }
                else
                {
                    Mentor? selectedMentor = null;
                    ims.MentorId = null;
                    await _userRepo.UpdateAsync(user);
                    await _imsRepo.UpdateAsync(ims);
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.Successful,
                        Data = InternResponseData(user, selectedMentor, _currentSessionId)
                    };
                }


            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while updating intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<InternResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = "Error occured while updating intern."
                };
            }
        }

        public InternResponseDTO InternResponseData(User user, Mentor? mentor, Guid sessionId)
        {
            var attachedMentor = new MentorMiniDTO { };
            if (mentor != null)
            {
                attachedMentor.MentorId = mentor.UserId;
                attachedMentor.FullName = mentor.User.FullName;
            }
            else
            {
                attachedMentor = null;
            }
            string profilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], user.ProfilePicture);
            var intern = new InternResponseDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Team = user.Team,
                CourseOfStudy = user.Intern.CourseOfStudy,
                Institution = user.Intern.Institution,
                Gender = user.Gender,
                SessionId = sessionId,
                Bio = user.Bio,
                ProfilePicture = profilePicture,
                Mentor = attachedMentor,
            };
            return intern;
        }

        public InternResponseDTO InternResponseData(User user, User? mentor, Guid sessionId)
        {
            var attachedMentor = new MentorMiniDTO { };
            if (mentor != null)
            {
                attachedMentor.MentorId = mentor.UserId;
                attachedMentor.FullName = mentor.FullName;
            }
            else
            {
                attachedMentor = null;
            }
            string profilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], user.ProfilePicture);
            var intern = new InternResponseDTO
            {
                UserId = user.UserId,
                Email = user.Email,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Team = user.Team,
                CourseOfStudy = user.Intern.CourseOfStudy,
                Institution = user.Intern.Institution,
                Gender = user.Gender,
                SessionId = sessionId,
                Bio = user.Bio,
                ProfilePicture = profilePicture,
                Mentor = attachedMentor,
            };
            return intern;
        }
    }
}
