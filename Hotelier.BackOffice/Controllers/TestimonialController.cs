using Hotelier.Application.Common.Models;
using Hotelier.Application.Testimonials.Queries.Dtos;
using Hotelier.Application.Testimonials.Queries.GetTestimonials;
using Hotelier.BackOffice.Models.Testimonial;
using Hotelier.BackOffice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Hotelier.BackOffice.Controllers;

public class TestimonialController : BaseController
{
    public TestimonialController(RequestHelper requestHelper, IOptions<UrlSetting> urlSetting) : base(requestHelper, urlSetting)
    {
    }
    
    public async Task<IActionResult> Index(int page=1,int pageSize=10)
    {
        parameters = new Dictionary<string, string>();

        parameters["Page"] = page.ToString();
        parameters["PageSize"] = pageSize.ToString();

        var result = await _requestHelper.SendRequestWithResponse<GetTestimonialsVm>
        (url: _urlSetting.TestimonialPath,
            parameters: parameters);
        
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        return View(new TestimonialAddRequestModel());
    }

    [HttpPost]
    public async Task<IActionResult> Add(TestimonialAddRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Post, _urlSetting.TestimonialPath,model);
            return RedirectToAction("Index", "Testimonial");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Detail(long id)
    {
        return View(await _requestHelper.GetByIdAsync<TestimonialDto>(_urlSetting.TestimonialPath,id));
    }

    [HttpGet]
    public async Task<IActionResult> Update(long id)
    {
        var result = await _requestHelper.GetByIdAsync<TestimonialDto>(_urlSetting.TestimonialPath,id);

        if (!result.Image.IsNullOrEmpty())
        {
            ViewBag.hasImage = true;
        }
        
        return View(new TestimonialUpdateRequestModel
        {
            Id = result.Id,
            Description = result.Description,
            Name = result.Name,
            Type = result.Type
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(TestimonialUpdateRequestModel model)
    {
        if (ModelState.IsValid)
        {
            await _requestHelper.SendRequestAsync(RequestType.Put, _urlSetting.TestimonialPath, model);
            return RedirectToAction("Detail", "Testimonial", new { id = model.Id });
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(long id)
    {
        await _requestHelper.SendDeleteByIdRequestAsync(_urlSetting.TestimonialPath, id);
        return RedirectToAction("Index", "Testimonial");
    }
}