using Framework.Application.Common.Interfaces;
using Framework.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace YZL5136.WebUI.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _contextAccessor;
    public CurrentUser(IHttpContextAccessor accessor)
    {
        _contextAccessor = accessor;
    }

    // Giriş yapan kullanıcının Id değerini Identity'den alarak sınıfımıza tanımladık.
    public long? UserId => Convert.ToInt64(_contextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);

    public User? User
    {
        get
        {

            var context = _contextAccessor.HttpContext.RequestServices.GetRequiredService<IApplicationDbContext>();

            return context.Users.Where(x => x.Id == UserId).FirstOrDefault();

        }
    }
}
