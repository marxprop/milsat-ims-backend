using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Sessions;

namespace MilsatIMS.Interfaces
{
    public interface ISessionService
    {
        Task<GenericResponse<SessionDTO>> CreateSession();
        Task<GenericResponse<SessionDTO>> GetSessions();
        Task<GenericResponse<SessionDTO>> GetCurrentSession();
    }
}
