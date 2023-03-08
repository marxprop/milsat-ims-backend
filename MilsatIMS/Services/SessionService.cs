using Microsoft.EntityFrameworkCore;
using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Sessions;

namespace MilsatIMS.Services
{
    public class SessionService : ISessionService
    {
        private readonly ILogger<SessionService> _logger;
        private readonly IAsyncRepository<Session> _sessionRepo;
        public SessionService(ILogger<SessionService> logger, IAsyncRepository<Session> sessionRepo)
        {
            _logger = logger;
            _sessionRepo = sessionRepo;
        }

        public async Task<GenericResponse<SessionDTO>> CloseSession()
        {
            try
            {
                _logger.LogInformation($"Received request to end the current session");
                var session = await _sessionRepo.GetAll().Where(s => s.Status == Status.Current).SingleOrDefaultAsync();
                if (session == null)
                {
                    return new GenericResponse<SessionDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "No current live session.",
                    };
                }
                session.Status = Status.Complete;
                session.EndDate = DateTime.UtcNow;
                await _sessionRepo.UpdateAsync(session);
                var session_dto = CreateSessionDTO(session);
                return new GenericResponse<SessionDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "The current live session has been ended successfully",
                    Data = session_dto
                };
            }

            catch (Exception ex)
            {
                string message = "Error occured while ending the current session";
                _logger.LogError($"{message}. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<SessionDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = message
                };
            }
        }

        public async Task<GenericResponse<SessionDTO>> CreateSession(SessionVm sessionvm)
        {
            try
            {
                _logger.LogInformation($"Received request to create a new session");
                var current = await _sessionRepo.GetAll().Where(s => s.Status == Status.Current).ToListAsync();
                if (current.Any())
                {
                    _logger.LogInformation($"Trying to create a new session on an ongoing session");
                    return new GenericResponse<SessionDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.INVALID_REQUEST,
                        Message = "Cannot create a new session during a current session. The current session has to come to an end first."
                    };
                }

                var session = new Session { Name = sessionvm.Name, Status = Status.Current };
                await _sessionRepo.AddAsync(session);
                var session_dto = CreateSessionDTO(session);
                return new GenericResponse<SessionDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "A new session has been successfully created.",
                    Data = session_dto
                };
            }
            catch (Exception ex)
            {
                string message = "Error occured while creating a new session";
                _logger.LogError($"{message}. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<SessionDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = message
                };
            }
        }

        public async Task<GenericResponse<SessionDTO>> GetCurrentSession()
        {
            try
            {
                _logger.LogInformation($"Received request to get the current session");
                var session = await _sessionRepo.GetAll().Where(s => s.Status == Status.Current).SingleOrDefaultAsync();
                if (session == null)
                {
                    return new GenericResponse<SessionDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "No current live session.",
                    };
                }
                var session_dto = CreateSessionDTO(session);
                return new GenericResponse<SessionDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully found the current live session.",
                    Data = session_dto
                };
            }
            catch (Exception ex)
            {
                string message = "Error occured while getting current session";
                _logger.LogError($"{message}. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<SessionDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = message
                };
            }
        }

        public async Task<GenericResponse<List<SessionDTO>>> GetSessions()
        {
            try
            {
                _logger.LogInformation($"Received request to get all sessions");

                var sessions = await _sessionRepo.GetTableByOrder(p => p.StartDate);
                var sessions_dto = new List<SessionDTO>();
                foreach (var session in sessions)
                {
                    var isession = CreateSessionDTO(session);
                    sessions_dto.Add(isession);
                }
                return new GenericResponse<List<SessionDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully got all session(s).",
                    Data = sessions_dto
                };
            }
            catch (Exception ex)
            {
                string message = "Error occured while getting all sessions";
                _logger.LogError($"{message}. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<SessionDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = message
                };
            }
        }

        public SessionDTO CreateSessionDTO(Session session)
        {
            var session_dto = new SessionDTO
            {
                SessionId = session.SessionId,
                Name = session.Name,
                Status = session.Status,
                StartDate = session.StartDate.ToString("dd-MMMM-yy"),
                EndDate = session.EndDate!=null ? session.EndDate.Value.ToString("dd-MMMM-yy") : "Ongoing"
            };
            return session_dto;
        }

        public async Task<GenericResponse<SessionDTO>> GetSessionById(Guid id)
        {
            string session_id = id.ToString();
            try
            {
                _logger.LogInformation($"Received request to get a session with Id:{session_id}");
                var session = await _sessionRepo.GetByIdAsync(id);
                if (session == null)
                {
                    return new GenericResponse<SessionDTO>
                    {
                        Successful = true,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "This sesssion does not exist.",
                    };
                }
                var session_dto = CreateSessionDTO(session);
                return new GenericResponse<SessionDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "Successfully found the session.",
                    Data = session_dto
                };
            }
            catch (Exception ex)
            {
                string message = $"Error occured while getting a session with id: {session_id}";
                _logger.LogError($"{message}. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<SessionDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR,
                    Message = message
                };
            }
        }
    }
}
