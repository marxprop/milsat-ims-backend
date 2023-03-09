using Microsoft.AspNetCore.Mvc;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Interns;

namespace MilsatIMS.Interfaces
{
    public interface IInternService
    {
        Task<GenericResponse<List<InternResponseDTO>>> GetAllInterns(Guid? sessionId, int pageNumber, int pageSize);
        Task<GenericResponse<List<InternResponseDTO>>> FilterInterns(GetInternVm vm, Guid? sessionid, int pageNumber, int pageSize);
        Task<GenericResponse<List<InternResponseDTO>>> GetInternById(Guid? sessionid, Guid id);
        Task<GenericResponse<InternResponseDTO>> AddIntern(CreateInternDTO vm);
        Task<GenericResponse<InternResponseDTO>> UpdateIntern(UpdateInternVm vm);        
    }
}
