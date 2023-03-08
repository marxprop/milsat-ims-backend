using MilsatIMS.Interfaces;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Sessions;

namespace MilsatIMS.Services
{
    public class SessionService : ISessionService
    {
        public SessionService()
        {

        }

        public Task<GenericResponse<SessionDTO>> CreateSession()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<SessionDTO>> GetCurrentSession()
        {
            throw new NotImplementedException();
        }

        public Task<GenericResponse<SessionDTO>> GetSessions()
        {
            throw new NotImplementedException();
        }
    }
}
