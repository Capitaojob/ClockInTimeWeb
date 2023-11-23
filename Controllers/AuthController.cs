using Microsoft.AspNetCore.Mvc;
using ClockInTimeWeb.Utils;
using ClockInTimeWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace ClockInTimeWeb.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        // POST: Auth
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string email, string password)
        {
            if (Authentication.ValidateUser(email, password))
            {
                CitContext context;
                context = new CitContext();
                var funcionarioId = await context.Funcionarios
                    .Where(f => f.Email == email)
                    .Select(f => f.Id)
                    .FirstOrDefaultAsync();
                
                ViewBag.Id = funcionarioId;

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Credenciais inválidas. Tente novamente.";
                return View();
            }
        }
    }
}
