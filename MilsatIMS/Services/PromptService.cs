using MilsatIMS.Enums;
using MilsatIMS.Interfaces;
using MilsatIMS.Models;
using MilsatIMS.ViewModels;
using MilsatIMS.ViewModels.Prompts;

namespace MilsatIMS.Services
{
    public class PromptService : IPromptService
    {
        private readonly ILogger<PromptService> _logger;
        private readonly IAsyncRepository<Prompt> _promptRepo;
        public PromptService(ILogger<PromptService> logger, IAsyncRepository<Prompt> promptRepo)
        {
            _logger = logger;
            _promptRepo = promptRepo;
        }

        public async Task<GenericResponse<PromptDTO>> AddPrompt(PromptVm prompt)
        {
            _logger.LogInformation($"Received request to create a new prompt");
            try
            {
                var new_prompt = new Prompt { Info = prompt.Info };
                await _promptRepo.AddAsync(new_prompt);

                var data = new PromptDTO
                {
                    PromptId = new_prompt.PromptId,
                    Info = new_prompt.Info,
                    PublishDate = new_prompt.PublishDate.ToString("dd-MMMM-yyyy"),
                };
                return new GenericResponse<PromptDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "The new prompt has been created successfully",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while Creating Intern. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<PromptDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }


        public async Task<GenericResponse<List<PromptDTO>>> GetPrompts()
        {
            _logger.LogInformation($"Received request to get all live prompt");
            try
            {
                var live_prompts = await _promptRepo.GetTableByOrder(p => p.PublishDate);
                var prompts_dto = new List<PromptDTO>();
                foreach (var prompt in live_prompts) {
                    var iprompt = new PromptDTO {
                        PromptId=prompt.PromptId,
                        Info = prompt.Info,
                        PublishDate = prompt.PublishDate.ToString("dd-MMMM-yy")
                    };
                    prompts_dto.Add(iprompt);
                }
                return new GenericResponse<List<PromptDTO>>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "The live prompts has been fetched successfully",
                    Data = prompts_dto
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while trying the get live prompts. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<List<PromptDTO>>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public async Task<GenericResponse<PromptDTO>> UpdatePrompt(UpdatePromptVm vm)
        {
            _logger.LogInformation($"Received request to update a prompt");
            try
            {
                var prompt = await _promptRepo.GetByIdAsync(vm.PromptId);
                if (prompt == null)
                {
                    return new GenericResponse<PromptDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "This prompt does not exist"
                    };
                }

                prompt.Info = vm.Info;
                await _promptRepo.UpdateAsync(prompt);
                return new GenericResponse<PromptDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "This prompt has been updated successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while updating prompt. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<PromptDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }

        public async Task<GenericResponse<PromptDTO>> DeletePrompt(Guid id)
        {
            _logger.LogInformation($"Received request to delete a prompt");
            try
            {
                var prompt = await _promptRepo.GetByIdAsync(id);
                if (prompt == null)
                {
                    return new GenericResponse<PromptDTO>
                    {
                        Successful = false,
                        ResponseCode = ResponseCode.NotFound,
                        Message = "This prompt does not exist"
                    };
                }

                await _promptRepo.DeleteAsync(prompt);
                return new GenericResponse<PromptDTO>
                {
                    Successful = true,
                    ResponseCode = ResponseCode.Successful,
                    Message = "This prompt has been removed successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while deleting prompt. Messg: {ex.Message} : StackTrace: {ex.StackTrace}");
                return new GenericResponse<PromptDTO>
                {
                    Successful = false,
                    ResponseCode = ResponseCode.EXCEPTION_ERROR
                };
            }
        }
    }
}
