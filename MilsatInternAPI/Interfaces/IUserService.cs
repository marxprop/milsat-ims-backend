using MilsatInternAPI.ViewModels;
using MilsatInternAPI.ViewModels.Users;

namespace MilsatInternAPI.Interfaces
{
    public interface IUserService
    {
        Task<GenericResponse<List<UserResponseDTO>>> GetAllUsers(int pageNumber, int pageSize);
        Task<GenericResponse<List<UserResponseDTO>>> FilterUsers(GetUserVm vm, int pageNumber, int pageSize);
        Task<GenericResponse<List<UserResponseDTO>>> GetUserById(Guid id);
        Task<GenericResponse<UserResponseDTO>> UpdateProfile(UpdateUserVm vm);
        Task<GenericResponse<UserResponseDTO>> RemoveUser(Guid id);
    }
}