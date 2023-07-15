using Microsoft.AspNetCore.Http;

namespace Hotelier.Application.Common.Interfaces;

public interface IFileService
{
    public string UploadPhoto(IFormFile file,string directory);
}