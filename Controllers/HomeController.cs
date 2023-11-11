using Framework.Application.Features.Articles.Queries.LastArticles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Kayıtsız kullanıcı karşılama ekranıdır.
        public async Task<IActionResult> Index(LastArticlesQuery query)
        {
            // Son 10 güncel makale listelenir.
            var response = await _mediator.Send(query);

            return View(response.Articles);
        }
    }
}
