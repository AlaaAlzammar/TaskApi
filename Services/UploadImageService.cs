namespace WebApplication4.Services
{
    public class UploadImageService : IUploadService
    {
        private readonly IWebHostEnvironment _enviroment;

        private string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".tif", ".gif" };

        private int fileSize = (1 * 1024 * 1024); // 1MB

        private String basePath = "images";

        public static string accept = "image/*";

        public UploadImageService(IWebHostEnvironment enviroment)
        {
            this._enviroment = enviroment;
        }

        public string? Upload(IFormFile Upload, string subFolder = "Products")
        {
            String imgUrl = string.Empty;
            // validation
            if (Upload.Length > fileSize)
            {
                throw new Exception($"Allowed file size {fileSize / 1024 / 1024} MB");

            }
            if (!allowedExtensions.Contains(Path.GetExtension(Upload.FileName).ToLower()))
            {
                throw new Exception($"Allowed file type {string.Join(",", allowedExtensions)} ");
            }


            // save image
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Upload.FileName);
            var filePath = Path.Combine(_enviroment.WebRootPath, basePath, subFolder, fileName);
            using (var filestream = System.IO.File.Create(filePath))
            {
                Upload.CopyTo(filestream);
            }


            return $"/{basePath}/{subFolder}/{fileName}";

        }
    }
}
