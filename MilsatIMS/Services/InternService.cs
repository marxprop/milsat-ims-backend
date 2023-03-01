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
        private readonly IAsyncRepository<User> _userRepo;
        private readonly IAuthentication _authService;
        private readonly ILogger<InternService> _logger;
        private readonly IConfiguration _iconfig;
        public InternService(IAsyncRepository<Intern> internRepo, IAsyncRepository<Mentor> mentorRepo,
            ILogger<InternService> logger, IAuthentication authService, IAsyncRepository<User> userRepo,
            IConfiguration iconfig)
        {
            _internRepo = internRepo;
            _mentorRepo = mentorRepo;
            _userRepo = userRepo;
            _logger = logger;
            _authService = authService;
            _iconfig = iconfig;
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
                    UserId = newUser.UserId,
                    CourseOfStudy = request.CourseOfStudy,
                    Institution = request.Institution,
                };

                var selectedMentor = await SelectMentor(newUser.Team);

                if (selectedMentor != null)
                {
                    newIntern.MentorId = selectedMentor.UserId;
                }
                await _userRepo.AddAsync(newUser);
                newIntern.UserId = newUser.UserId;
                await _internRepo.AddAsync(newIntern);

                
                //Crete response body
                var newInterns = InternResponseData( newUser , selectedMentor);
                return new GenericResponse<InternResponseDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = newInterns
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<InternResponseDTO>
                { 
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
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

        public async Task<GenericResponse<List<InternResponseDTO>>> GetAllInterns(int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to fetch paginated Intern(s): Request: pageNumber:{pageNumber}, pageSize:{pageSize}");
            try
            {
                var pagedData = await _userRepo.GetAll()
                    .Include(x => x.Intern).ThenInclude(x => x.Mentor.User)
                    .Where(x => x.Role == RoleType.Intern)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var interns = InternResponseData(pagedData);
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = interns
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

        public async Task<GenericResponse<List<InternResponseDTO>>> GetInternById(Guid id)
        {
            _logger.LogInformation($"Received a request to fetch an Intern: Request(user id):{id}");
            try
            {
                var user = await _userRepo.GetAll()
                    .Include(x => x.Intern).ThenInclude(x => x.Mentor.User)
                    .Where(x => x.UserId == id).SingleOrDefaultAsync();
                if (user == null)
                {
                    return new GenericResponse<List<InternResponseDTO>>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound
                    };
                }
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = InternResponseData(new List<User> { user })
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Fetching Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }

        }

        public async Task<GenericResponse<List<InternResponseDTO>>> FilterInterns(GetInternVm vm, int pageNumber, int pageSize)
        {
            _logger.LogInformation($"Received a request to Fetch Intern(s): Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var filtered = await _userRepo.GetAll().Include(e => e.Intern).ThenInclude(e => e.Mentor.User)
                                                 .Where(x => x.Role == RoleType.Intern &&
                                                        (vm.name == null || x.FullName.Contains(vm.name)
                                                        && vm.Team == null || x.Team == vm.Team))
                                                 .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
                var users = InternResponseData(filtered);
                return new GenericResponse<List<InternResponseDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = users
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


        public async Task<GenericResponse<InternResponseDTO>> UpdateIntern(UpdateInternVm vm)
        {
            _logger.LogInformation($"Received a request to update Intern: Request:{JsonConvert.SerializeObject(vm)}");
            try
            {
                var user = await _userRepo.GetAll()
                    .Include(x => x.Intern)
                    .Where(x => x.UserId == vm.UserId)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound
                    };
                }

                user.Team = vm.Team;
                user.FullName = vm.FullName;
                user.Email = vm.Email;
                user.PhoneNumber = vm.PhoneNumber;

                if (vm.MentorId != Guid.Empty)
                {
                    var selectedMentor = await _userRepo.GetAll().Where(x => x.UserId == vm.MentorId
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

                    user.Intern.MentorId = selectedMentor.UserId;
                    await _userRepo.UpdateAsync(user);
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.Successful,
                        Data = InternResponseData(user, selectedMentor)
                    };
                }
                else
                {
                    Mentor? selectedMentor = null;
                    user.Intern.MentorId = null;
                    await _userRepo.UpdateAsync(user);
                    return new GenericResponse<InternResponseDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.Successful,
                        Data = InternResponseData(user, selectedMentor)
                    };
                }

                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while updating intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<InternResponseDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public List<InternResponseDTO> InternResponseData(List<User> source)
        {
            List<InternResponseDTO> interns = new();
            foreach (var user in source)
            {
                var attachedMentor = new MentorMiniDTO { MentorId = user.Intern.MentorId, FullName = user.Intern.Mentor?.User?.FullName };

                string profilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], user.ProfilePicture);
                interns.Add(new InternResponseDTO
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    Team = user.Team,
                    CourseOfStudy = user.Intern.CourseOfStudy,
                    Institution = user.Intern.Institution,
                    Gender = user.Gender,
                    Year = user.Intern.Year,
                    Bio = user.Bio,
                    ProfilePicture = profilePicture, 
                    Mentor = attachedMentor,
                });
            };
            return interns;
        }

        public InternResponseDTO InternResponseData(User user, Mentor? mentor)
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
                Year = user.Intern.Year,
                Bio = user.Bio,
                ProfilePicture = profilePicture,
                Mentor = attachedMentor,
            };
            return intern;
        }

        public InternResponseDTO InternResponseData(User user, User mentor)
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
                Year = user.Intern.Year,
                Bio = user.Bio,
                ProfilePicture = profilePicture,
                Mentor = attachedMentor,
            };
            return intern;
        }
    }

}
