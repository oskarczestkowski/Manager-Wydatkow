using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Manager_Wydatkow.Models;
using Manager_Wydatkow.Utilities;

namespace Manager_Wydatkow.Controllers
{
    public class KategoriasController : Controller
    {
        private readonly AplikacjaDbContext _context;

        public KategoriasController(AplikacjaDbContext context)
        {
            _context = context;
        }


        [Authentication]
        // GET: Kategorias
        public async Task<IActionResult> Index()
        {
              return View(await _context.Kategorias.ToListAsync());
        }

        [Authentication]
        // GET: Kategorias/AddorEdit
        public IActionResult AddOrEdit(int id=0) 
        {
            if(id== 0)
                return View(new Kategoria());
            else
                return View(_context.Kategorias.Find(id));
        }
        [Authentication]
        // POST: Kategorias/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("KategoriaId,Tytul,Ikona,Typ")] Kategoria kategoria)
        {
            if (ModelState.IsValid)
            {
                if(kategoria.KategoriaId==0)
                    _context.Add(kategoria);
                else
                    _context.Update(kategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kategoria);
        }
        [Authentication]
        // POST: Kategorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kategorias == null)
            {
                return Problem("Entity set 'AplikacjaDbContext.Kategorias'  is null.");
            }
            var kategoria = await _context.Kategorias.FindAsync(id);
            if (kategoria != null)
            {
                _context.Kategorias.Remove(kategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriaExists(int id)
        {
          return _context.Kategorias.Any(e => e.KategoriaId == id);
        }
    }
}
