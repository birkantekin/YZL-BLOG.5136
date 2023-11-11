using Framework.Application.Features.Articles.Commands.Create;
using Framework.Application.Features.Articles.Commands.Delete;
using Framework.Application.Features.Articles.Commands.Update;
using Framework.Application.Features.Articles.Queries.GetById;
using Framework.Application.Features.Articles.Queries.GetByUser;
using Framework.Application.Features.Categories.Queries.GetAll;
using Framework.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Member.Controllers;

[Area("Member")]
[Authorize(Roles = "Member")]
public class HomeController : Controller
{
    private readonly IMediator _mediator;
    private IWebHostEnvironment _env;

    public HomeController(IMediator mediator, IWebHostEnvironment environment)
    {
        _mediator = mediator;
        _env = environment;
    }

    public async Task<IActionResult> Index(GetArticlesByUserQuery query)
    {
        var response = await _mediator.Send(query);

        return View(response.Articles);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        ViewBag.Categories = categories.Categories;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Article article,IFormFile PhotoItem, CancellationToken cancellationToken)
    {
        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        ViewBag.Categories = categories.Categories;

        if (PhotoItem != null && PhotoItem.Length > 0)
        {
            // Dosyanın kaydedileceği hedef klasörü belirleyin
            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads"); // Örnek: wwwroot/uploads

            // Dosyanın benzersiz bir ad alması için bir yol oluşturun
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoItem.FileName;

            // Dosyayı hedef klasöre kaydedin
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await PhotoItem.CopyToAsync(stream);
            }

            article.Photo = "/Uploads/" + uniqueFileName;
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Lütfen fotoğraf yükleyiniz!");

            return View(article);
        }

        var response = await _mediator.Send(new CreateArticleCommand() { Article = article });

        if (response.IsCreated)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Lütfen bilgileri kontrol ederek tekrar deneyiniz!");


        return View(article);
    }

    [HttpGet]
    public async Task<IActionResult> Update(GetArticleByIdQuery query)
    {
        var response = await _mediator.Send(query);

        if (response.Article == null)
        {
            return RedirectToAction("Index");
        }

        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        ViewBag.Categories = categories.Categories;

        return View(response.Article);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Article article, IFormFile PhotoItem, CancellationToken cancellationToken)
    {

        if (PhotoItem != null && PhotoItem.Length > 0)
        {
            // Dosyanın kaydedileceği hedef klasörü belirleyin
            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads"); // Örnek: wwwroot/uploads

            // Dosyanın benzersiz bir ad alması için bir yol oluşturun
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + PhotoItem.FileName;

            // Dosyayı hedef klasöre kaydedin
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await PhotoItem.CopyToAsync(stream);
            }

            article.Photo = "/Uploads/" + uniqueFileName;
        }

        var response = await _mediator.Send(new UpdateArticleCommand() { Article = article });

        if (response.IsSave)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Lütfen bilgileri kontrol ederek tekrar deneyiniz!");

        var categories = await _mediator.Send(new GetAllCategoriesQuery());

        ViewBag.Categories = categories.Categories;

        return View(article);
    }

    public async Task<IActionResult> Remove(DeleteArticleCommand command)
    {
        var response = await _mediator.Send(command);

        return RedirectToAction("Index");
    }
}
