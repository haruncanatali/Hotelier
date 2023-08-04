using Hotelier.Application.Common.Models;
using Hotelier.Application.Services.Queries.Dtos;
using Hotelier.Application.Services.Queries.GetServices;
using Hotelier.BackOffice.Models.Service;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hotelier.BackOffice.Controllers;

public class ServiceController : BaseController
{
    public ServiceController(RequestHelper requestHelper, IOptions<UrlSetting> urlSetting) : base(requestHelper, urlSetting)
    {
    }
    
    public async Task<IActionResult> Index(int page=1,int pageSize=10)
    {
        parameters = new Dictionary<string, string>();

        parameters["Page"] = page.ToString();
        parameters["PageSize"] = pageSize.ToString();

        var result = await _requestHelper.SendRequestWithResponse<GetServicesVm>
        (url: _urlSetting.ServicePath,
            parameters: parameters);
        
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View(new ServiceAddRequestModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(ServiceAddRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Post, _urlSetting.ServicePath,model);
            return RedirectToAction("Index", "Service");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Detail(long id)
    {
        return View(await _requestHelper.GetByIdAsync<ServiceDto>(_urlSetting.ServicePath,id));
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var result = await _requestHelper.GetByIdAsync<ServiceDto>(_urlSetting.ServicePath,id);

        if (!result.Icon.IsNullOrEmpty())
        {
            ViewBag.hasIcon = true;
        }
        
        return View(new ServiceUpdateRequestModel
        {
            Id = result.Id,
            Description = result.Description,
            Title = result.Title,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(ServiceUpdateRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Put, _urlSetting.ServicePath, model);
            return RedirectToAction("Detail", "Service", new { id = model.Id });
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        await _requestHelper.SendDeleteByIdRequestAsync(_urlSetting.ServicePath, id);
        return RedirectToAction("Index", "Service");
    }
}