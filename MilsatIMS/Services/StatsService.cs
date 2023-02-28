using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels.Stats;
using MilsatIMS.ViewModels;
using MilsatIMS.Models;
using MilsatIMS.Enums;
using Microsoft.EntityFrameworkCore;

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

        public async Task<GenericResponse<GetTeamTotalDTO>> GetTeamTotal()
        {
            _logger.LogInformation($"Received request to fetch total number of interns in each team");
            try
            {
                var interns = await _userRepo.GetAll().Where(u => u.Role == RoleType.Intern).ToListAsync();
                var teamCounts = interns.GroupBy(u => u.Team)
                                        .Select(g => new { Team = g.Key, Count = g.Count() });

                var data = new GetTeamTotalDTO
                {
                    Backend = teamCounts.FirstOrDefault(tc => tc.Team == TeamType.Backend)?.Count ?? 0,
                    Branding = teamCounts.FirstOrDefault(tc => tc.Team == TeamType.Branding)?.Count ?? 0,
                    Community = teamCounts.FirstOrDefault(tc => tc.Team == TeamType.Community)?.Count ?? 0,
                    Frontend = teamCounts.FirstOrDefault(tc => tc.Team == TeamType.Frontend)?.Count ?? 0,
                    Mobile = teamCounts.FirstOrDefault(tc => tc.Team == TeamType.Mobile)?.Count ?? 0,
                    UIUX = teamCounts.FirstOrDefault(tc => tc.Team == TeamType.UIUX)?.Count ?? 0,
                };
                return new GenericResponse<GetTeamTotalDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while fetching total number of interns in each team. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<GetTeamTotalDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
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
