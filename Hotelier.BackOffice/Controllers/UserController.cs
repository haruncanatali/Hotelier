using System.IO.Compression;
using Hotelier.Application.Common.Models;
using Hotelier.Application.Users.Queries.Dtos;
using Hotelier.Application.Users.Queries.GetUsers;
using Hotelier.BackOffice.Models.User;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hotelier.BackOffice.Controllers;

public class UserController : BaseController
{
    public UserController(RequestHelper requestHelper, IOptions<UrlSetting> _options) : base(requestHelper, _options)
    {
    }
    
    public async Task<IActionResult> Index(int page=1,int pageSize=10)
    {
        parameters = new Dictionary<string, string>();

        parameters["Page"] = page.ToString();
        parameters["PageSize"] = pageSize.ToString();

        var result = await _requestHelper.SendRequestWithResponse<GetUsersVm>
        (url: _urlSetting.UsersPath,
            parameters: parameters);
        
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View(new UserAddRequestModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(UserAddRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Post, _urlSetting.UserPath,model);
            return RedirectToAction("Index", "User");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Detail(long id)
    {
        parameters = new Dictionary<string, string>();
        parameters["Id"] = id.ToString();
        return View(await _requestHelper.SendRequestWithResponse<UserDto>(_urlSetting.UserPath, parameters));
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        parameters = new Dictionary<string, string>();
        parameters["Id"] = id.ToString();

        var result = await _requestHelper.SendRequestWithResponse<UserDto>(_urlSetting.UserPath, parameters);

        if (!result.ProfilePhoto.IsNullOrEmpty())
        {
            ViewBag.hasProfilePhoto = true;
        }
        
        return View(new UserUpdateRequestModel
        {
            Birthdate = result.Birthdate,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Id = result.Id
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(UserUpdateRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Put, _urlSetting.UserPath, model);
            return RedirectToAction("Detail", "User", new { id = model.Id });
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        await _requestHelper.SendDeleteByIdRequestAsync(_urlSetting.UserPath, id);
        return RedirectToAction("Index", "User");
    }
}