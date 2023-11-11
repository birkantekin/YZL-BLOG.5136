using Framework.Application.Features.Authorities.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Controllers;

public class AuthorityController : Controller
{
    private readonly IMediator _mediator;
    public AuthorityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Profil detay sayfasıdır makale yazarının.
    public async Task<IActionResult> Index(GetAuthorityByIdQuery query)
    {
        var response = await _mediator.Send(query);

        if (response.Authority == null)
        {
            return NotFound();
        }

        return View(response.Authority);
    }
}
