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
        private readonly IAuthentication _authService;
        private readonly IConfiguration _iconfig;

        public MentorService(IConfiguration iconfig, IAsyncRepository<Mentor> mentorRepo,
            ILogger<MentorService> logger, IAuthentication authService,
            IAsyncRepository<User> userRepo)
        {
            _mentorRepo = mentorRepo;
            _logger = logger;
            _authService = authService;
            _userRepo = userRepo;
            _iconfig = iconfig;
        }

        //public async Task<GenericResponse<List<MentorResponseDTO>>> AddMentor(List<CreateMentorVm> vm)
        //{
        //    _logger.LogInformation($"Received a request to add new Mentor(s): Request:{JsonConvert.SerializeObject(vm)}");
        //    try
        //    {
        //        var mentors = new List<Mentor>();
        //        foreach (CreateMentorVm mentor in vm)
        //        {
        //            var user = await _userRepo.GetAll().Where(x => x.Email == mentor.Email).FirstOrDefaultAsync();
        //            if (user != null)
        //            {
        //                return new GenericResponse<List<MentorResponseDTO>>
        //                {
        //                    Successful = false,
        //                    ResponseCode = ResponseCode.INVALID_REQUEST,
        //                    Message = "User with this Email already exists"
        //                };
        //            }
        //            var newUser = new User { 
        //                Email = mentor.Email, Role = RoleType.Mentor,
        //                FullName = mentor.FullName, Gender = mentor.Gender,
        //                PhoneNumber = mentor.PhoneNumber, Team = mentor.Team
        //            };
        //            newUser = _authService.RegisterPassword(newUser, mentor.PhoneNumber);
        //            await _userRepo.AddAsync(newUser);
        //            var singleMentor = new Mentor { UserId = newUser.UserId, Interns = new List<Intern>() };
        //            singleMentor.UserId = newUser.UserId;
        //            mentors.Add(singleMentor);
        //        }
        //        await _mentorRepo.AddRangeAsync(mentors);

        //        //Crete response body
        //        var newMentors = MentorResponseData(mentors);
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = true,
        //            ResponseCode = ResponseCode.Successful,
        //            Data = newMentors
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR
        //        };
        //    }
        //}

        //public async Task<GenericResponse<List<MentorResponseDTO>>> GetAllMentors(int pageNumber, int pageSize)
        //{
        //    _logger.LogInformation($"Received a request to fetch all mentors with : Pagination - (pageNumber):{pageNumber}, (pageSize):{pageSize}");
        //    try
        //    {
        //        var pagedData = await _userRepo.GetAll().Include(x => x.Mentor).ThenInclude(x => x.Interns).ThenInclude(x => x.User)
        //                                              .Where(x => x.Role == RoleType.Mentor)
        //                                              .Skip((pageNumber - 1) * pageSize)
        //                                              .Take(pageSize).ToListAsync();
        //        var collectedMentors = MentorResponseData(pagedData);
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = true,
        //            ResponseCode = ResponseCode.Successful,
        //            Data = collectedMentors
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR
        //        };
        //    }
        //}

        //public async Task<GenericResponse<List<MentorResponseDTO>>> GetMentorById(Guid id)
        //{
        //    _logger.LogInformation($"Received a request to fetch a Mentor: Request(user id):{id}");
        //    try
        //    {
        //        var user = await _userRepo.GetAll()
        //           .Include(x => x.Mentor).ThenInclude(x => x.Interns).ThenInclude(x => x.User)
        //           .Where(x => x.UserId == id).SingleOrDefaultAsync();

        //        if (user == null)
        //        {
        //            return new GenericResponse<List<MentorResponseDTO>>
        //            {
        //                Successful = false,
        //                ResponseCode = ResponseCode.NotFound
        //            };
        //        }
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = true,
        //            ResponseCode = ResponseCode.Successful,
        //            Data = MentorResponseData(new List<User> { user })
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR
        //        };
        //    }
        //}

        //public async Task<GenericResponse<List<MentorResponseDTO>>> GetMentors(GetMentorVm vm, int pageNumber, int pageSize)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Received a request to Fetch Intern(s): Request:{JsonConvert.SerializeObject(vm)}");
        //        var filtered = await _userRepo.GetAll().Include(e => e.Mentor).ThenInclude(e => e.Interns).ThenInclude(e => e.User)
        //                                             .Where(x => x.Role == RoleType.Mentor &&
        //                                                    (vm.name == null || x.FullName.Contains(vm.name)
        //                                                    && vm.Team == null || x.Team == vm.Team))
        //                                             .Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        //        var users = MentorResponseData(filtered);
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = true,
        //            ResponseCode = ResponseCode.Successful,
        //            Data = users
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Exception occured while Fecthing Data Request. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR
        //        };
        //    }
        //}

        //public async Task<GenericResponse<List<MentorResponseDTO>>> UpdateMentor(UpdateMentorVm vm)
        //{
        //    _logger.LogInformation($"Received a request to update Mentor: Request:{JsonConvert.SerializeObject(vm)}");
        //    try
        //    {
        //        var mentor = await _userRepo.GetAll()
        //            .Include(x => x.Mentor)
        //                .ThenInclude(x => x.Interns)
        //                    .ThenInclude(x => x.User)
        //            .Where(x => x.UserId == vm.MentorId)
        //            .FirstOrDefaultAsync();

        //        if (mentor == null)
        //        {
        //            return new GenericResponse<List<MentorResponseDTO>>
        //            {
        //                Successful = false,
        //                ResponseCode = ResponseCode.NotFound,
        //                Message = "User not found" 
        //            };
        //        }
        //        mentor.FullName = vm.FullName ;
        //        mentor.Email = vm.Email;
        //        mentor.PhoneNumber = vm.PhoneNumber;
        //        if (vm.Team != mentor.Team)
        //        {
        //            mentor.Mentor.Interns = new List<Intern> { };

        //            await _userRepo.UpdateAsync(mentor);
        //        }

        //        var updatedIntern = MentorResponseData(new List<User> { mentor });
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = true,
        //            ResponseCode = ResponseCode.Successful,
        //            Data = updatedIntern
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Error occured while updating intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
        //        return new GenericResponse<List<MentorResponseDTO>>
        //        {
        //            Successful = false,
        //            ResponseCode = ResponseCode.EXCEPTION_ERROR
        //        };
        //    }
        //}

        //public List<MentorResponseDTO> MentorResponseData(List<Mentor> source)
        //{
        //    List<MentorResponseDTO> mentors = new();
        //    foreach (var mentor in source)
        //    {
        //        var interns = AssignedIntern(mentor.Interns);
        //        string profilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], mentor.User.ProfilePicture);
        //        mentors.Add(new MentorResponseDTO
        //        {
        //            UserId = mentor.UserId,
        //            Email = mentor.User.Email,
        //            FullName = mentor.User.FullName,
        //            PhoneNumber = mentor.User.PhoneNumber,
        //            Team = mentor.User.Team,
        //            Gender = mentor.User.Gender,
        //            Bio = mentor.User.Bio,
        //            ProfilePicture = profilePicture,
        //            Interns = interns
        //        });
        //    };
        //    return mentors;
        //}

        //public List<MentorResponseDTO> MentorResponseData(List<User> source)
        //{
        //    List<MentorResponseDTO> mentors = new();
        //    foreach (var mentor in source)
        //    {
        //        var interns = AssignedIntern(mentor.Mentor.Interns);
        //        mentors.Add(new MentorResponseDTO
        //        {
        //            UserId = mentor.UserId,
        //            Email = mentor.Email,
        //            FullName = mentor.FullName,
        //            PhoneNumber = mentor.PhoneNumber,
        //            Team = mentor.Team,
        //            Gender = mentor.Gender,
        //            Bio = mentor.Bio,
        //            ProfilePicture = Utils.GetUserPicture(_iconfig["ProfilePicturesPath"], mentor.ProfilePicture),
        //            Interns = interns
        //        });
        //    };
        //    return mentors;
        //}

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

        public Task<GenericResponse<List<MentorResponseDTO>>> AddMentor(List<CreateMentorVm> vm)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<MentorResponseDTO>>> GetAllMentors(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<MentorResponseDTO>>> GetMentorById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<MentorResponseDTO>>> GetMentors(GetMentorVm vm, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<List<MentorResponseDTO>>> UpdateMentor(UpdateMentorVm vm)
        {
            throw new NotImplementedException();
        }
    }
}
