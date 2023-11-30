using ClockInTimeWeb.Data;
using ClockInTimeWeb.Models;
using ClockInTimeWeb.Utils;
using iText.StyledXmlParser.Jsoup.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClockInTimeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly CitContext _context;

        public HomeController(ILogger<HomeController> logger, CitContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
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

        [HttpGet]
        public async Task<IActionResult> GetReportDataForUser(int idFuncionario, int month, int year)
        {
            var funcionario = await _context.Funcionarios.FindAsync(idFuncionario);

            if (funcionario == null)
            {
                return NotFound();
            }

            var cargo = await _context.Cargos
                .FirstOrDefaultAsync(c => c.IdCargo == funcionario.Cargo); 

            if (cargo == null)
            {
                return NotFound();
            }

            var workedHours = PayrollUtils.GetWorkedHoursForUser(idFuncionario, month, year);

            string endereco = funcionario.Endereco != null ? funcionario.Endereco.ToString() : "Não Informado";
            string telefone = funcionario.Telefone != null ? funcionario.Telefone.ToString() : "Não Informado";

            return Json(new 
                { 
                    nome = funcionario.Nome, 
                    email = funcionario.Email,
                    endereco,
                    telefone,
                    horasTotaisBase = cargo.CargaHoraria, 
                    horasTotaisTrabalhadas = workedHours, 
                    salarioBruto = cargo.Salario 
                }
            );
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        public IActionResult ClockIn()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetClockInRegistersForUser(int idFuncionario) 
        {
            if (idFuncionario <= 0)
            {
                return Json(new { error = "Funcionário inválido" });
            }

            var clockInRegisters = await _context.Pontos
                                          .Where(p => p.IdFuncionario == idFuncionario)
                                          .OrderByDescending(p => p.Data)
                                          .Take(5)
                                          .ToListAsync();

            return Json(new { clockInRegisters });
        }

        // POST: Home/ClockInUser
        [HttpPost]
        public ActionResult ClockInUser([FromBody] int idFuncionario) 
        {
            if (idFuncionario <= 0)
            {
                return Json(new { error = "Funcionário inválido" });
            }

            // Verificar se o funcionário existe no banco
            var employee = new Funcionario();

            if (employee == null)
            {
                return Json(new { error = "Funcionário não encontrado" });
            }

            DateTime novaData = DateTime.Now;
            TimeSpan currentHour = novaData.TimeOfDay;

            // Verificar se já existe um registro para o funcionário na data de hoje
            var todayEntry = _context.Pontos
                .Where(p => p.IdFuncionario == idFuncionario && p.Data == DateUtils.ParseDateTimeToDateOnly(novaData)) 
                .FirstOrDefault();

            if (todayEntry != null)
            {
                String clockinType = "";
                // Já existe um registro para hoje, verificar campos nulos se necessário
                if (todayEntry.SaidaAl == null) 
                { 
                    todayEntry.SaidaAl = novaData;
                    clockinType = "Saída Para Almoço";
                }
                else if (todayEntry.EntradaAl == null)
                {
                    todayEntry.EntradaAl = novaData;
                    clockinType = "Volta Do Almoço";
                }
                else if (todayEntry.Saida == null)
                {
                    todayEntry.Saida = novaData;
                    clockinType = "Saída";
                }
                else
                {
                    return Json(new { success = "Todas as horas já foram concluídas hoje. Registro inválido. " });
                }

                _context.SaveChanges();

                return Json(new { success = "Registro aplicado com sucesso ", entry = clockinType });
            }
            else
            {
                // Não existe registro para hoje, criar um novo registro
                var newEntry = new Ponto
                {
                    IdFuncionario = idFuncionario,
                    Data = DateUtils.ParseDateTimeToDateOnly(novaData),
                    Entrada = novaData,
                    SaidaAl = null,
                    EntradaAl = null,
                    Saida = null
                };

                _context.Pontos.Add(newEntry);
                _context.SaveChanges();

                return Json(new { success = "Novo registro criado", entry = "Entrada" });
            }
        }

        // GET: Home/GetEmployeeAdministratorRole/5
        [HttpGet]
        public async Task<IActionResult> GetEmployeeAdministratorRole(int userId)
        {
            CitContext _context = new CitContext();

            var funcionario = await _context.Funcionarios.FindAsync(userId);

            if (funcionario == null)
            {
                return Json(new { error = "Funcionário não encontrado. Entre em contato com um adminitrador." });
            }

            var cargo = await _context.Cargos
                       .Where(c => c.IdCargo == funcionario.Cargo)
                       .FirstOrDefaultAsync();

            if (cargo == null)
            {
                return Json(new { error = "Cargo não encontrado. Entre em contato com um adminitrador." });
            }

            if (cargo.Administrador == false)
            {
                return Json(new { isAdministrator = false });
            }

            return Json(new { isAdministrator = true }); 
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}