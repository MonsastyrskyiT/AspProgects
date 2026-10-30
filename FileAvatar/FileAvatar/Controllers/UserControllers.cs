using AutoMapper;
using FileAvatar.Data;
using FileAvatar.Data.Models;
using FileAvatar.DTO;
using FileAvatar.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileAvatar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class UserController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly AppDBContext _context;
        private const string SubFolder = "avatars";

        public UserController(AppDBContext appDbContext, IFileService fileService, IMapper mapper)
        {
            _context = appDbContext;
            _mapper = mapper;
            _fileService = fileService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDTO dto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = dto.Role,
                Age = dto.Age,
                CreatedAt = DateTime.Now,
            };

            if (dto.Avatar != null && dto.Avatar.Length > 0)
            {
                var fileResult = await _fileService.SaveFileAsync(dto.Avatar, SubFolder);
                user.AvatarURL = fileResult.RelativePath;
            }


            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
         }



        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return Ok(user);
        }

    }
}
