using Framework.Application.Features.Users.Commands.Approve;
using Framework.Application.Features.Users.Commands.Delete;
using Framework.Application.Features.Users.Commands.UpdatePassived;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Approved(UpdateApproveUserCommand command)
        {

            await _mediator.Send(command);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }

        public async Task<IActionResult> Reject(DeleteUserCommand command)
        {
            await _mediator.Send(command);

            return RedirectToAction("Index", "Home", new { area = "Admin" });
        }


        public async Task<IActionResult> Passived(UpdateUserPassivedCommand command)
        {
            await _mediator.Send(command);

            return RedirectToAction("Index", "CommentManagement", new { area = "Admin" });
        }
    }
}
