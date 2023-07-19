using Hotelier.Application.Common.Models;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hotelier.BackOffice.Controllers;

public class BaseController : Controller
{
    protected readonly RequestHelper _requestHelper;
    protected readonly UrlSetting _urlSetting;
    protected Dictionary<string, string> parameters = new Dictionary<string, string>();

    public BaseController(RequestHelper requestHelper, IOptions<UrlSetting> _options)
    {
        _requestHelper = requestHelper;
        _urlSetting = _options.Value;
    }
}