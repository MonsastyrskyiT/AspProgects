using PracticAsp11102026.DTO;
using TaskStatus = PracticAsp11102026.Models.TaskStatus;

namespace PracticAsp11102026.Services
{
    public interface ITaskItemService
    {
        Task<List<TaskItemDTO>> GetAllAsync(Guid? projectId, TaskStatus? status);
        Task<TaskItemDTO?> GetByIdAsync(Guid id);
        Task<TaskItemDTO?> CreateAsync(CreateTaskItemDTO dto);   // null, если проект не найден
        Task<TaskItemDTO?> UpdateAsync(Guid id, UpdateTaskItemDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
