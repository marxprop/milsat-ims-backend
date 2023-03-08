using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Sessions;

namespace MilsatIMS.Interfaces
{
    public interface ISessionService
    {
        Task<GenericResponse<SessionDTO>> CreateSession(SessionVm sessionvm);
        Task<GenericResponse<List<SessionDTO>>> GetSessions();
        Task<GenericResponse<SessionDTO>> GetSessionById(Guid id);
        Task<GenericResponse<SessionDTO>> GetCurrentSession();
        Task<GenericResponse<SessionDTO>> CloseSession();

    }
}
