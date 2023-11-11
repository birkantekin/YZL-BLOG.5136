using Framework.Application.Features.Categories.Commands.Approve;
using Framework.Application.Features.Categories.Commands.Create;
using Framework.Application.Features.Categories.Commands.Delete;
using Framework.Application.Features.Categories.Commands.Update;
using Framework.Application.Features.Categories.Queries.GetAll;
using Framework.Domain.Entities;
using Framework.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _mediator.Send(new GetAllCategoriesQuery() { Status = EnumType.Active });

            ViewBag.Categories = categories.Categories;

            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category item)
        {
            item.Status = EnumType.Active;

            var response = await _mediator.Send(new CreateCategoryCommand() { Category = item, IsAdmin = true });

            if (!response.IsCreated)
            {
                ModelState.AddModelError(string.Empty, response.Message);
            }
            else
            {
                return RedirectToAction("Index");
            }

            ViewBag.IsCreated = response.IsCreated;

            return View(item);
        }

        public async Task<IActionResult> Update(long Id)
        {
            var response = await _mediator.Send(new GetAllCategoriesQuery() { Status = EnumType.Active });

            var category = response.Categories.FirstOrDefault(x => x.Id == Id);

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Category item)
        {
            item.Status = EnumType.Active;

             await _mediator.Send(new UpdateCategoryCommand() { Category = item });

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(DeleteCategoryCommand command)
        {
            await _mediator.Send(command);

            return RedirectToAction("Index", "Category", new { area = "Admin" });
        }


        public async Task<IActionResult> Approved(UpdateApproveCategoryCommand command)
        {

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public async Task<IActionResult> Reject(DeleteCategoryCommand command)
        {
            await _mediator.Send(command);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }
    }
}
