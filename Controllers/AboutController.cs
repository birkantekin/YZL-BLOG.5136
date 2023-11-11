using Microsoft.AspNetCore.Mvc;

namespace YZL5136.WebUI.Controllers;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
