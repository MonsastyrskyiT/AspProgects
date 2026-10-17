using PracticAsp11102026.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticAsp11102026.DTO;
using PracticAsp11102026.Models;

namespace PracticAsp11102026.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ProjectService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        // Удалённые записи отсекает HasQueryFilter из AppDbContext
        public async Task<List<ProjectDTO>> GetAllAsync()
        {
            var projects = await _db.Projects
                .AsNoTracking()
                .Include(p => p.Tasks)
                .ToListAsync();

            return _mapper.Map<List<ProjectDTO>>(projects);
        }

        public async Task<ProjectDTO?> GetByIdAsync(Guid id)
        {
            var project = await _db.Projects
                .AsNoTracking()
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            return project == null ? null : _mapper.Map<ProjectDTO>(project);
        }

        public async Task<ProjectDTO> CreateAsync(CreateProjectDTO dto)
        {
            var project = _mapper.Map<Project>(dto);

            await _db.Projects.AddAsync(project);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<ProjectDTO?> UpdateAsync(Guid id, UpdateProjectDTO dto)
        {
            var project = await _db.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return null;

            _mapper.Map(dto, project);
            await _db.SaveChangesAsync();

            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var project = await _db.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
                return false;

            // Soft delete проекта + всех его задач
            project.IsDeleted = true;
            foreach (var task in project.Tasks)
            {
                task.IsDeleted = true;
            }

            await _db.SaveChangesAsync();
            return true;
        }
    }
}
