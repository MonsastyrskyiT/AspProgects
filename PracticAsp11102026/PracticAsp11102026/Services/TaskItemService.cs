using PracticAsp11102026.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PracticAsp11102026.DTO;
using PracticAsp11102026.Models;
using TaskStatus = PracticAsp11102026.Models.TaskStatus;

namespace PracticAsp11102026.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public TaskItemService(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<List<TaskItemDTO>> GetAllAsync(Guid? projectId, TaskStatus? status)
        {
            var query = _db.TaskItems.AsNoTracking().AsQueryable();

            if (projectId.HasValue)
                query = query.Where(t => t.ProjectId == projectId.Value);

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            var tasks = await query
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return _mapper.Map<List<TaskItemDTO>>(tasks);
        }

        public async Task<TaskItemDTO?> GetByIdAsync(Guid id)
        {
            var task = await _db.TaskItems
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            return task == null ? null : _mapper.Map<TaskItemDTO>(task);
        }

        public async Task<TaskItemDTO?> CreateAsync(CreateTaskItemDTO dto)
        {
            // Проект должен существовать и быть не удалённым
            var projectExists = await _db.Projects.AnyAsync(p => p.Id == dto.ProjectId);
            if (!projectExists)
                return null;

            var task = _mapper.Map<TaskItem>(dto);

            await _db.TaskItems.AddAsync(task);
            await _db.SaveChangesAsync();

            return _mapper.Map<TaskItemDTO>(task);
        }

        public async Task<TaskItemDTO?> UpdateAsync(Guid id, UpdateTaskItemDTO dto)
        {
            var task = await _db.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                return null;

            _mapper.Map(dto, task);
            await _db.SaveChangesAsync();

            return _mapper.Map<TaskItemDTO>(task);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _db.TaskItems.FirstOrDefaultAsync(t => t.Id == id);
            if (task == null)
                return false;

            task.IsDeleted = true;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
