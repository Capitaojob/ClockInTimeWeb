using ClockInTimeWeb.Data;
using ClockInTimeWeb.Models;
using ClockInTimeWeb.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClockInTimeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ClockIn()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        // GET: Home/ClockInUser/12
        [HttpGet]
        public ActionResult ClockInUser(int employeeId)
        {
            if (employeeId <= 0)
            {
                return Json(new { error = "Funcionário inválido" });
            }

            CitContext context = new CitContext();

            // Verificar se o funcionário existe no banco
            var employee = context.Funcionarios.Find(employeeId);

            if (employee == null)
            {
                return Json(new { error = "Funcionário não encontrado" });
            }

            DateOnly novaData = DateUtils.ParseDateTimeToDateOnly(DateTime.Now);

            // Verificar se já existe um registro para o funcionário na data de hoje
            var todayEntry = context.Pontos
                .Where(p => p.IdFuncionario == employeeId && p.Data == novaData) 
                .FirstOrDefault();

            if (todayEntry != null)
            {
                // Já existe um registro para hoje, verificar campos nulos se necessário
                // Exemplo: if (todayEntry.SaidaAl == null) { todayEntry.SaidaAl = novaData; }

                // Retornar sucesso ou realizar operação de update (PUT)
                return Json(new { success = "Registro encontrado para hoje " }); //, entry = todayEntry
            }
            else
            {
                // Não existe registro para hoje, criar um novo registro
                var newEntry = new Ponto
                {
                    IdFuncionario = employeeId,
                    Data = novaData,
                    Entrada = DateTime.Now,
                    SaidaAl = null,
                    EntradaAl = null,
                    Saida = null
                };

                context.Pontos.Add(newEntry);
                context.SaveChanges();

                // Retornar sucesso ou realizar operação de criação (POST)
                return Json(new { success = "Novo registro criado" }); //entry = newEntry
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}