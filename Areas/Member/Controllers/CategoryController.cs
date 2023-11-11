using Framework.Application.Features.Categories.Commands.Create;
using Framework.Domain.Entities;
using Framework.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category item)
        {

            var response = await _mediator.Send(new CreateCategoryCommand() { Category = item, IsAdmin = false });

            if (!response.IsCreated)
            {
                ModelState.AddModelError(string.Empty, response.Message);
            }
          

            ViewBag.IsCreated = response.IsCreated;

            return View(item);
        }
    }
}
