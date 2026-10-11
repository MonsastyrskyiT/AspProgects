using PracticAsp11102026.DTO;

namespace PracticAsp11102026.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDTO>> GetAllAsync();
        Task<ProjectDTO?> GetByIdAsync(Guid id);
        Task<ProjectDTO> CreateAsync(CreateProjectDTO dto);
        Task<ProjectDTO?> UpdateAsync(Guid id, UpdateProjectDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
