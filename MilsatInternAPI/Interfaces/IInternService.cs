using Microsoft.AspNetCore.Mvc;
using MilsatInternAPI.ViewModels;
using MilsatInternAPI.ViewModels.Interns;

namespace MilsatInternAPI.Interfaces
{
    public interface IInternService
    {
        Task<GenericResponse<List<InternResponseDTO>>> GetAllInterns(int pageNumber, int pageSize);
        Task<GenericResponse<List<InternResponseDTO>>> FilterInterns(GetInternVm vm, int pageNumber, int pageSize);
        Task<GenericResponse<List<InternResponseDTO>>> GetInternById(Guid id);
        Task<GenericResponse<List<InternResponseDTO>>> AddIntern(CreateInternDTO vm);
        Task<GenericResponse<List<InternResponseDTO>>> UpdateIntern(UpdateInternVm vm);        
    }
}
