using Hotelier.Application.Common.Models;
using Hotelier.Application.Subscribes.Queries.GetSubscribes;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hotelier.BackOffice.Controllers;

public class SubscribeController : BaseController
{
    public SubscribeController(RequestHelper requestHelper, IOptions<UrlSetting> urlSetting) : base(requestHelper, urlSetting)
    {
    }
    
    public async Task<IActionResult> Index(int page=1,int pageSize=10)
    {
        parameters = new Dictionary<string, string>();

        parameters["Page"] = page.ToString();
        parameters["PageSize"] = pageSize.ToString();

        var result = await _requestHelper.SendRequestWithResponse<GetSubscribesVm>
        (url: _urlSetting.SubscribePath,
            parameters: parameters);
        
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        await _requestHelper.SendDeleteByIdRequestAsync(_urlSetting.SubscribePath, id);
        return RedirectToAction("Index", "Subscribe");
    }
}