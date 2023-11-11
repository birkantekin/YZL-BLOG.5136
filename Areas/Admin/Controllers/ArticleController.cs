using Framework.Application.Features.Articles.Commands.Update;
using Framework.Application.Features.Articles.Queries.GetById;
using Framework.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;
        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Passived(GetArticleByIdQuery query)
        {
            var response = await _mediator.Send(query);

            if (response.Article != null)
            {
                response.Article.Status = EnumType.Passived;

                await _mediator.Send(new UpdateArticleCommand() { Article = response.Article });
            }
            return RedirectToAction("Index", "CommentManagement", new { area="Admin"});
        }
    }
}
