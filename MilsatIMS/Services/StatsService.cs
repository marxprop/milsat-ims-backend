using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels.Stats;
using MilsatIMS.ViewModels;
using MilsatIMS.Models;
using MilsatIMS.Enums;

namespace MilsatIMS.Services
{
    public class StatsService : IStatsService
    {
        private readonly IAsyncRepository<User> _userRepo;
        private readonly ILogger<StatsService> _logger;
        public StatsService(ILogger<StatsService> logger, IAsyncRepository<User> userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }

        public async Task<GenericResponse<GetTotalUsersDTO>> GetTotalUsers()
        {
            _logger.LogInformation($"Received request to fetch total number of users");
            try
            {
                int n_interns = await _userRepo.CountAsync(u => u.Role == RoleType.Intern);
                int n_mentors = await _userRepo.CountAsync(u => u.Role == RoleType.Mentor);

                var data = new GetTotalUsersDTO { Interns = n_interns, Mentors = n_mentors };
                return new GenericResponse<GetTotalUsersDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while fetching total number of users. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<GetTotalUsersDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }
    }
}
