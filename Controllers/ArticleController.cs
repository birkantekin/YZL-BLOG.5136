using Framework.Application.Features.Articles.Commands.Update;
using Framework.Application.Features.Articles.Queries.GetById;
using Framework.Application.Features.Categories.Queries.GetAll;
using Framework.Application.Features.Comments;
using Framework.Application.Features.Likes;
using Framework.Domain.Entities;
using Framework.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Controllers;

public class ArticleController : Controller
{
    private readonly IMediator _mediator;
    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index(GetArticleByIdQuery query)
    {
        var categoriesResponse = await _mediator.Send(new GetAllCategoriesQuery() { Status = EnumType.Active });
        

        // Tüm kategorileri ekranın sağında göstermek için veritabanından verileri çekiyoruz.
        ViewBag.Categories = categoriesResponse.Categories.OrderBy(x => x.Name).ToList();
        
        var response = await _mediator.Send(query);

        if (response.Article == null)
        {
            return NotFound();
        }
        
        //Okunma sayısını artırmak

        response.Article.Counter += 1;

        var update = await _mediator.Send(new UpdateArticleCommand() { Article = response.Article });

        return View(response.Article);
    }

    public async Task<IActionResult> Comment(CommentCommand command, string Url)
    {
        await _mediator.Send(command);

        return Redirect(Url);
    }

    public async Task<IActionResult> Like(LikeCommand command, string Url)
    {
        await _mediator.Send(command);

        return Redirect(Url);
    }

    
    
    
   
}
