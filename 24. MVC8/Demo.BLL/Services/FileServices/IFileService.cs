using Demo.BLL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services;

public interface IFileService
{
    Task<Result<string>> UploadFile(IFormFile? file);
}

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;
    private readonly IEnumerable<string> _allowedExtenions = [".png", ".jpg", ".jpeg", ".webp"];

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<Result<string>> UploadFile(IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            return Error.Validation(descreption: "Please Slect an image.");
        }

        string fileExtenion = Path.GetExtension(file.FileName).ToLower();

        if (!_allowedExtenions.Contains(fileExtenion))
        {
            return Error.Validation(descreption: "Invalid file fromat.");
        }
        const string imageFolderName = "images";
        string folderPath = Path.Combine(_environment.WebRootPath, imageFolderName);

        try
        {
            Directory.CreateDirectory(folderPath);

            string fileName = Guid.NewGuid().ToString("N") + fileExtenion;

            string filePath = Path.Combine(folderPath, fileName);

            using FileStream stream = new FileStream(filePath, FileMode.Create);

            await file.CopyToAsync(stream);

            return new Result<string>("/" + imageFolderName + "/" + fileName);
        }
        catch (Exception ex)
        {
            return Error.Failure(ex.Message);
        }
    }
}