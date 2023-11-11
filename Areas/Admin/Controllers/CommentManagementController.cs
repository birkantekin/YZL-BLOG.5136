using Framework.Application.Features.Comments.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentManagementController : Controller
    {
        private readonly IMediator _mediator;
        public CommentManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _mediator.Send(new GetAllCommmentsQuery());

            return View(response.Comments);
        }
    }
}
