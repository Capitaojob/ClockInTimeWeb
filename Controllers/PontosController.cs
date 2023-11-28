using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClockInTimeWeb.Data;

namespace ClockInTimeWeb.Controllers
{
    public class PontosController : Controller
    {
        private readonly CitContext _context;

        public PontosController(CitContext context)
        {
            _context = context;
        }

        // GET: Pontoes
        public async Task<IActionResult> Index()
        {
              return _context.Pontos != null ? 
                          View(await _context.Pontos.ToListAsync()) :
                          Problem("Entity set 'CitContext.Pontos'  is null.");
        }

        // GET: Pontoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pontos == null)
            {
                return NotFound();
            }

            var ponto = await _context.Pontos
                .FirstOrDefaultAsync(m => m.IdPonto == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // GET: Pontoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pontoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPonto,IdFuncionario,Data,Entrada,SaidaAl,EntradaAl,Saida")] Ponto ponto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ponto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ponto);
        }

        // GET: Pontoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pontos == null)
            {
                return NotFound();
            }

            var ponto = await _context.Pontos.FindAsync(id);
            if (ponto == null)
            {
                return NotFound();
            }
            return View(ponto);
        }

        // POST: Pontoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPonto,IdFuncionario,Data,Entrada,SaidaAl,EntradaAl,Saida")] Ponto ponto)
        {
            if (id != ponto.IdPonto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ponto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontoExists(ponto.IdPonto))
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
            return View(ponto);
        }

        // GET: Pontoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pontos == null)
            {
                return NotFound();
            }

            var ponto = await _context.Pontos
                .FirstOrDefaultAsync(m => m.IdPonto == id);
            if (ponto == null)
            {
                return NotFound();
            }

            return View(ponto);
        }

        // POST: Pontoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pontos == null)
            {
                return Problem("Entity set 'CitContext.Pontos'  is null.");
            }
            var ponto = await _context.Pontos.FindAsync(id);
            if (ponto != null)
            {
                _context.Pontos.Remove(ponto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontoExists(int id)
        {
          return (_context.Pontos?.Any(e => e.IdPonto == id)).GetValueOrDefault();
        }
    }
}
