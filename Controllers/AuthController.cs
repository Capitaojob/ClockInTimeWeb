using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClockInTimeWeb.Utils;

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
        public ActionResult Index(string email, string password)
        {
            if (Authentication.ValidateUser(email, password))
            {
                // Autenticação bem-sucedida, redirecione ou faça algo mais
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Autenticação falhou, retorne uma mensagem de erro ou redirecione para uma página de erro
                ViewBag.ErrorMessage = "Credenciais inválidas. Tente novamente.";
                return View();
            }
        }
    }
}
