using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Prompts;

namespace MilsatIMS.Interfaces
{
    public interface IPromptService
    {
        Task<GenericResponse<List<PromptDTO>>> GetPrompts();
        Task<GenericResponse<PromptDTO>> AddPrompt(PromptVm prompt);
        Task<GenericResponse<PromptDTO>> UpdatePrompt(UpdatePromptVm vm);
        Task<GenericResponse<PromptDTO>> DeletePrompt(Guid id);
    }
}
