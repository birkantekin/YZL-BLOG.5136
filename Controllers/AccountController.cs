using Framework.Application.Common.Interfaces;
using Framework.Application.Features.Authentication.Login;
using Framework.Application.Features.Authentication.Register;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace YZL5136.WebUI.Controllers;

public class AccountController : Controller
{
    private readonly IMediator _mediator;
    private IWebHostEnvironment _env;
    private readonly ICurrentUser _currentUser;
    public AccountController(IMediator mediator, IWebHostEnvironment environment, ICurrentUser currentUser)
    {
        _mediator = mediator;
        _env = environment;
        _currentUser = currentUser;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        if (ModelState.IsValid)
        {
            // Kullanıcı bilgilerinin doğruluğu için alt yapıya gönderiyoruz
            var response = await _mediator.Send(command);

            if (response.IsAuthenticated && response.User != null)
            {
                // Kullanıcı girişinde Identity ile kullanıcı bilgilerini dolduruyoruz.
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, response.User.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, response.User.FirstName.ToString() + " " + response.User.LastName.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Email, response.User.Email.ToString()));
                identity.AddClaim(new Claim("ProfilePhoto", response.User.ProfilePhoto.ToString()));
                identity.AddClaim(new Claim("UserName", response.User.UserName.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Role, response.User.Role.Name.ToString()));

                //Kullanıcı giriş işlemi sağlanıyor.
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    new AuthenticationProperties() { IsPersistent = command.RememberMe });

                //İlgili role göre yönlendirme yapılıyor.
                if (response.User.Role.Name == "Admin")
                    return RedirectToAction("Index", "Home", new { area = "Admin" });

                return RedirectToAction("Index", "Home", new { area = "Member" });
            }

            // Bilgiler doğru değilse error mesajları modelstate ile veriliyor.
            ModelState.AddModelError(string.Empty, response.Message);
        }

        return View(command);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterCommand command, IFormFile Profile)
    {
        if (Profile == null)
        {
            ModelState.AddModelError(string.Empty, "Profil fotoğrafı yükleyiniz!");

            return View(command);
        }

        if (Profile != null && Profile.Length > 0)
        {
            // Dosyanın kaydedileceği hedef klasörü belirleyin
            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads"); // Örnek: wwwroot/uploads

            // Dosyanın benzersiz bir ad alması için bir yol oluşturun
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + Profile.FileName;

            // Dosyayı hedef klasöre kaydedin
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Profile.CopyToAsync(stream);
            }

            command.ProfilePhoto = "/Uploads/" + uniqueFileName;
        }

        if (ModelState.IsValid)
        {
            var response = await _mediator.Send(command);

            ViewBag.IsCreated = response.IsCreated;

            if (!response.IsCreated)
            {
                ModelState.AddModelError(string.Empty, response.Message);
            }
        }

        return View(command);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    public async Task<IActionResult> Control()
    {
        if (_currentUser.User==null)
        {
            return Json(false);
        }

        if (_currentUser.User.IsActive==Framework.Domain.Enums.EnumType.Passived)
        {
            return Json(false);
        }

        return View(true);
    }
}