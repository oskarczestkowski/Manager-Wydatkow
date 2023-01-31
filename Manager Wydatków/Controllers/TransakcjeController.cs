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
    public class TransakcjeController : Controller
    {
        private readonly AplikacjaDbContext _context;

        public TransakcjeController(AplikacjaDbContext context)
        {
            _context = context;
        }


        [Authentication]
        // GET: Transakcje
        public async Task<IActionResult> Index()
        {
            var aplikacjaDbContext = _context.Transakcjes.Include(t => t.Kategoria);
            return View(await aplikacjaDbContext.ToListAsync());
        }

        [Authentication]
        // GET: Transakcje/AddOrEdit
        public IActionResult AddOrEdit(int id=0)
        {
            PopulateCategories();
            if(id == 0) 
                return View(new Transakcje());
            else
                return View(_context.Transakcjes.Find(id));
        }

        // POST: Transakcje/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authentication]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransakcjeId,KategoriaId,Cena,Opis,Data")] Transakcje transakcje)
        {
            if (ModelState.IsValid)
            {
                if(transakcje.TransakcjeId == 0)
                    _context.Add(transakcje);
                else
                    _context.Update(transakcje);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(transakcje);
        }

        [Authentication]
        // POST: Transakcje/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transakcjes == null)
            {
                return Problem("Entity set 'AplikacjaDbContext.Transakcjes'  is null.");
            }
            var transakcje = await _context.Transakcjes.FindAsync(id);
            if (transakcje != null)
            {
                _context.Transakcjes.Remove(transakcje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var CategoryCollection = _context.Kategorias.ToList();
            Kategoria DefaultCategory = new Kategoria() { KategoriaId = 0, Tytul = "Wybierz kategorię" };
            CategoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = CategoryCollection;
        }
    
    
    }
}
