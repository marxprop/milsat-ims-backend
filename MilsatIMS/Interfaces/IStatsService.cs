using MilsatIMS.ViewModels.Stats;
using MilsatIMS.ViewModels;

namespace MilsatIMS.Interfaces
{
    public interface IStatsService
    {
        Task<GenericResponse<GetTotalUsersDTO>> GetTotalUsers();

        Task<GenericResponse<GetTeamTotalDTO>> GetTeamTotal();
    }
}
