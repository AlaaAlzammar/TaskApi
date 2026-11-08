namespace WebApplication4.Services
{
    public interface IUploadService
    {
        string? Upload(IFormFile formFile, string subFolder = "Avatars");
    }
}
