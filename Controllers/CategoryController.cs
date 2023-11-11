using Framework.Application.Features.Categories.Queries.GetAll;
using Framework.Application.Features.Follow;
using Framework.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Kategorilerin ve makalelerin listelenediği seçilen kategoriye göre 5 adet makale gelir. 
        public async Task<IActionResult> Index(long? categoryId)
        {
            var response = await _mediator.Send(new GetAllCategoriesQuery() { Status = EnumType.Active });

            // Tüm kategorileri ekranın sağında göstermek için veritabanından verileri çekiyoruz.
            ViewBag.Categories = response.Categories.OrderBy(x => x.Name).ToList();

            ViewBag.CategoryId = categoryId;


            return View();
        }

        // Takip et veya takip iptali işlemi
        public async Task<IActionResult> Follow(FollowCommand command,string? url)
        {
            await _mediator.Send(command);

            if (!string.IsNullOrEmpty(url))
            {
                return Redirect(url);
            }

            return RedirectToAction("Index");
        }
    }
}
