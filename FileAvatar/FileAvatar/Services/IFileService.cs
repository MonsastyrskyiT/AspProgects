using FileAvatar.DTO;

namespace FileAvatar.Services
{
    public interface IFileService
    {
        Task<FileResponseDTO> SaveFileAsync(IFormFile file, string subFolder);
        void DeleteFile(string reletivePath);
    }
}
