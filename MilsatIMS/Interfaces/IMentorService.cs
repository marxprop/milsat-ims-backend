using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Mentors;

namespace MilsatIMS.Interfaces
{
    public interface IMentorService
    {
        Task<GenericResponse<List<MentorResponseDTO>>> GetAllMentors(int pageNumber, int pageSize);
        Task<GenericResponse<List<MentorResponseDTO>>> GetMentors(GetMentorVm vm, int pageNumber, int pageSize);
        Task<GenericResponse<List<MentorResponseDTO>>> GetMentorById(Guid id);
        Task<GenericResponse<List<MentorResponseDTO>>> AddMentor(List<CreateMentorVm> vm);
        Task<GenericResponse<List<MentorResponseDTO>>> UpdateMentor(UpdateMentorVm vm);
    }
}
