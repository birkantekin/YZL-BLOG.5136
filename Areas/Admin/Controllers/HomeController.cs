using Framework.Application.Features.Categories.Queries.GetAll;
using Framework.Application.Features.Users.Queries.GetByPassived;
using Framework.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(GetUserByPassivedQuery query)
        {
            var response = await _mediator.Send(query);

            var categories = await _mediator.Send(new GetAllCategoriesQuery() { Status = EnumType.Passived });

            ViewBag.Categories = categories.Categories;    

            return View(response.Users);
        }
    }
}
