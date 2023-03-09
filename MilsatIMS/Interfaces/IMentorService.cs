using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Mentors;

namespace MilsatIMS.Interfaces
{
    public interface IMentorService
    {
        Task<GenericResponse<List<MentorResponseDTO>>> GetAllMentors(Guid? sessionid, int pageNumber, int pageSize);
        Task<GenericResponse<List<MentorResponseDTO>>> GetMentors(GetMentorVm vm, Guid? sessionid, int pageNumber, int pageSize);
        Task<GenericResponse<List<MentorResponseDTO>>> GetMentorById(Guid? sessionid, Guid id);
        Task<GenericResponse<MentorResponseDTO>> AddMentor(CreateMentorVm vm);
        Task<GenericResponse<MentorResponseUpdateDTO>> UpdateMentor(UpdateMentorVm vm);
    }
}
