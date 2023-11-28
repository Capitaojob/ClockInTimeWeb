using Microsoft.AspNetCore.Mvc;
using ClockInTimeWeb.Utils;
using ClockInTimeWeb.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ClockInTimeWeb.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }

        // POST: /Auth/ValidateUser
        [HttpPost]
        public async Task<IActionResult> ValidateUser([FromBody] UserCredentials credentials)
        {
            try
            {
                if (Authentication.ValidateUser(credentials.Email, credentials.Password))
                {
                    CitContext context;
                    context = new CitContext();
                    var funcionarioId = await context.Funcionarios
                        .Where(f => f.Email == credentials.Email)
                        .Select(f => f.Id)
                        .FirstOrDefaultAsync();
                
                    return Json(new { success = "Autenticado com sucesso", userId = funcionarioId });
                }
                else
                {
                    throw new Exception("Login ou senha incorretos, tente novamente.");
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message ?? "Erro na autenticação" });
            }
        }

        public class UserCredentials
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
