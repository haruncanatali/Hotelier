using Hotelier.Application.Common.Models;
using Hotelier.Application.Staffs.Queries.Dtos;
using Hotelier.Application.Staffs.Queries.GetStaffs;
using Hotelier.BackOffice.Models.Staff;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hotelier.BackOffice.Controllers;

public class StaffController : BaseController
{
    public StaffController(RequestHelper requestHelper, IOptions<UrlSetting> urlSetting) : base(requestHelper, urlSetting)
    {
    }
    
    public async Task<IActionResult> Index(int page=1,int pageSize=10)
    {
        parameters = new Dictionary<string, string>();

        parameters["Page"] = page.ToString();
        parameters["PageSize"] = pageSize.ToString();

        var result = await _requestHelper.SendRequestWithResponse<GetStaffsVm>
        (url: _urlSetting.StaffPath,
            parameters: parameters);
        
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View(new StaffAddRequestModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(StaffAddRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Post, _urlSetting.StaffPath,model);
            return RedirectToAction("Index", "Staff");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Detail(long id)
    {
        return View(await _requestHelper.GetByIdAsync<StaffDto>(_urlSetting.StaffPath,id));
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var result = await _requestHelper.GetByIdAsync<StaffDto>(_urlSetting.StaffPath,id);

        if (!result.Image.IsNullOrEmpty())
        {
            ViewBag.hasProfilePhoto = true;
        }
        
        return View(new StaffUpdateRequestModel
        {
            Id = result.Id,
            Name = result.Name,
            Title = result.Title,
            SocialMediaFacebook = result.SocialMediaFacebook,
            SocialMediaTwitter = result.SocialMediaTwitter,
            SocialMediaInstagram = result.SocialMediaInstagram
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(StaffUpdateRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Put, _urlSetting.StaffPath, model);
            return RedirectToAction("Detail", "Staff", new { id = model.Id });
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        await _requestHelper.SendDeleteByIdRequestAsync(_urlSetting.StaffPath, id);
        return RedirectToAction("Index", "Staff");
    }
}