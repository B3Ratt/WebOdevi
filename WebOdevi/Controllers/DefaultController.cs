using Microsoft.AspNetCore.Mvc;

namespace WebOdevi.Controllers
{
	public class DefaultController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
