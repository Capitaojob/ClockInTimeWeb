using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClockInTimeWeb.Data;
using ClockInTimeWeb.Utils;

namespace ClockInTimeWeb.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly CitContext _context;

        public FuncionariosController(CitContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        public async Task<IActionResult> Index()
        {
              return _context.Funcionarios != null ? 
                          View(await _context.Funcionarios.ToListAsync()) :
                          Problem("Entity set 'CitContext.Funcionarios'  is null.");
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Nascimento, Cargo, Status, Senha, Nome, Email, Cpf")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                funcionario.Status = 1;
                funcionario.Senha = Cryptography.EncryptPassword("tz2wsx@dr5");
                funcionario.Nascimento = DateUtils.ParseStringToDateOnly(Request.Form["Nascimento"]);

                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nascimento,Cargo,Status,Senha,Nome,Email,Cpf")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Formata o nascimento para o tipo do banco de dados
                    funcionario.Nascimento = DateUtils.ParseStringToDateOnly(Request.Form["Nascimento"]);

                    // Verificar se a senha recebida está vazia
                    if (string.IsNullOrWhiteSpace(funcionario.Senha))
                    {
                        // Carregar a senha atual do funcionário do banco de dados
                        var currentPassword = await _context.Funcionarios
                            .Where(f => f.Id == funcionario.Id)
                            .Select(f => f.Senha)
                            .FirstOrDefaultAsync();

                        // Atribuir a senha atual de volta ao modelo
                        funcionario.Senha = currentPassword;
                    } 
                    else
                    {
                        funcionario.Senha = Cryptography.EncryptPassword(Request.Form["Senha"]);
                    }

                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Funcionarios == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Funcionarios == null)
            {
                return Problem("Entity set 'CitContext.Funcionarios'  is null.");
            }
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario != null)
            {
                _context.Funcionarios.Remove(funcionario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
          return (_context.Funcionarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
