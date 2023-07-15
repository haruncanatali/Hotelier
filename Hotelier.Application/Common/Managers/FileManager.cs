using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Hotelier.Application.Common.Managers;

public class FileManager : IFileService
{
    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly ICurrentUserService _currentUserService;
    private readonly ImageSetting _imageSetting;


    public FileManager(IHostingEnvironment hostingEnvironment, ICurrentUserService currentUserService,
        IOptions<ImageSetting> options)
    {
        _hostingEnvironment = hostingEnvironment;
        _currentUserService = currentUserService;
        _imageSetting = options.Value;
    }

    public string UploadPhoto(IFormFile file,string directory)
    {
        string root = $"Images/{directory}/";
        string uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", root);
        var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        string imageHeaderKey = "";


        bool exists = Directory.Exists(uploadFolder);

        if (!exists)
            Directory.CreateDirectory(uploadFolder);

        if (file.Length <= 0) return "false";
        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).ToLower();
        var path = Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot", root, fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }

        if (currentEnvironment != null && currentEnvironment == "Development")
        {
            imageHeaderKey = _imageSetting.LocalKey;
        }
        else
        {
            imageHeaderKey = _imageSetting.HostKey;
        }

        var url = imageHeaderKey + Path.Combine(root, fileName);
        return url;
    }
}