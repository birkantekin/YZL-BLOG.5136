using Framework.Application.Features.Authentication.Profile;
using Framework.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;
        private IWebHostEnvironment _env;

        public ProfileController(IMediator mediator, IWebHostEnvironment environment)
        {
            _mediator = mediator;
            _env = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Index(ProfileCommand command, IFormFile? Profile)
        {

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

                //Kullanıcı kendini silmek istediği durum
                if (command.IsActive == EnumType.Passived)
                {
                    return RedirectToAction("Logout", "Account");
                }

                ViewBag.IsSaved = response.IsSave;

                if (!response.IsSave)
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                }
            }

            return View(command);
        }
    }
}
