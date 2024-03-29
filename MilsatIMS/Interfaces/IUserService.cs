﻿using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Users;

namespace MilsatIMS.Interfaces
{
    public interface IUserService
    {
        Task<GenericResponse<List<UserResponseDTO>>> GetAllUsers(Guid? sessionid, int pageNumber, int pageSize);
        Task<GenericResponse<List<UserResponseDTO>>> FilterUsers(GetUserVm vm, Guid? sessionid, int pageNumber, int pageSize);
        Task<GenericResponse<List<UserResponseDTO>>> GetUserById(Guid? sessionid, Guid id);
        Task<GenericResponse<UserResponseDTO>> UpdateInternProfile(UpdateInternVm vm);
        Task<GenericResponse<UserResponseDTO>> UpdateMentorProfile(UpdateMentorVm vm);
        Task<GenericResponse<UserResponseDTO>> RemoveUser(Guid id);
    }
}