using Hotelier.Application.Common.Models;
using Hotelier.Application.Rooms.Queries.Dtos;
using Hotelier.Application.Rooms.Queries.GetRooms;
using Hotelier.BackOffice.Models.Room;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hotelier.BackOffice.Controllers;

public class RoomController : BaseController
{
    public RoomController(RequestHelper requestHelper, IOptions<UrlSetting> urlSetting) : base(requestHelper, urlSetting)
    {
    }
    
    public async Task<IActionResult> Index(int page=1,int pageSize=10)
    {
        parameters = new Dictionary<string, string>();

        parameters["Page"] = page.ToString();
        parameters["PageSize"] = pageSize.ToString();

        var result = await _requestHelper.SendRequestWithResponse<GetRoomsVm>
        (url: _urlSetting.RoomPath,
            parameters: parameters);
        
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View(new RoomAddRequestModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(RoomAddRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Post, _urlSetting.RoomPath,model);
            return RedirectToAction("Index", "Room");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Detail(long id)
    {
        return View(await _requestHelper.GetByIdAsync<RoomDto>(_urlSetting.RoomPath,id));
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var result = await _requestHelper.GetByIdAsync<RoomDto>(_urlSetting.RoomPath,id);

        if (!result.CoverImage.IsNullOrEmpty())
        {
            ViewBag.hasCoverImage = true;
        }
        
        return View(new RoomUpdateRequestModel
        {
            Id = result.Id,
            Title = result.Title,
            Description = result.Description,
            RoomNumber = result.RoomNumber,
            Price = result.Price,
            BedCount = result.BedCount,
            BathCount = result.BathCount,
            WiFi = result.WiFi
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(RoomUpdateRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Put, _urlSetting.RoomPath, model);
            return RedirectToAction("Detail", "Room", new { id = model.Id });
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        await _requestHelper.SendDeleteByIdRequestAsync(_urlSetting.RoomPath, id);
        return RedirectToAction("Index", "Room");
    }
}