using Manager_Wydatkow.Models;
using Manager_Wydatkow.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Manager_Wydatkow.Controllers
{
    public class StronaGController : Controller
    {
        private readonly AplikacjaDbContext _context;
        public StronaGController(AplikacjaDbContext context) 
        {
            _context= context;
        }

        // Get Action
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        //Post Action
        [HttpPost]
        public ActionResult Login(User u)
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                if (ModelState.IsValid)
                {
                    using (LoginDbContext db = new())
                    {
                        var obj = db.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(u.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            HttpContext.Session.SetString("Username", obj.Username.ToString());
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            else
            {
                
                return RedirectToAction("Login");
            }
            ViewData["LoginFlag"] = "Zła nazwa użytkownika lub hasło!";
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("Username");
            return RedirectToAction("Login");
        }

        [Authentication]
        public async Task<ActionResult> Index()
        {
            //ostatnie 7 dni
            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transakcje> SelectedTransakcjes = await _context.Transakcjes
                .Include(x => x.Kategoria)
                .Where(y => y.Data >=StartDate && y.Data <= EndDate)
                .ToListAsync();

            //Suma Wpływów
            int sumaW = SelectedTransakcjes
                .Where(i => i.Kategoria.Typ == "Wpływ")
                .Sum(j => j.Cena);
            ViewBag.SumaW = sumaW.ToString("c", CultureInfo.GetCultureInfo("pl-PL"));

            //Suma Wydatków
            int sumaWw = SelectedTransakcjes
                .Where(i => i.Kategoria.Typ == "Wydatek")
                .Sum(j => j.Cena);
            ViewBag.SumaWw = sumaWw.ToString("c", CultureInfo.GetCultureInfo("pl-PL"));

            //Stan konta
            int stanK = sumaW - sumaWw;
            ViewBag.StanK = stanK.ToString("c", CultureInfo.GetCultureInfo("pl-PL"));

            //Wykres - kategorie wydatków
            ViewBag.DoughnutChartData = SelectedTransakcjes
                .Where(i => i.Kategoria.Typ == "Wydatek")
                .GroupBy(j => j.Kategoria.KategoriaId)
                .Select(k => new
                {
                    kategoriaTytulIkona = k.First().Kategoria.Ikona+" "+ k.First().Kategoria.Tytul,
                    cena = k.Sum(j => j.Cena),
                    cenaFormat = k.Sum(j => j.Cena).ToString("c", CultureInfo.GetCultureInfo("pl-PL"))
                })
                .OrderByDescending(l=>l.cena)
                .ToList();

            //spline chart
            //wplywy

            List<SplineChartData> WplywyPodsumowanie = SelectedTransakcjes
                .Where(i => i.Kategoria.Typ == "Wpływ")
                .GroupBy(j => j.Data)
                .Select(k => new SplineChartData()
                {
                    dzien = k.First().Data.ToString("dd-MMM"),
                    wplywy = k.Sum(l => l.Cena)
                })
                .ToList();

            //wydatki

            List<SplineChartData> WydatkiPodsumowanie = SelectedTransakcjes
                .Where(i => i.Kategoria.Typ == "Wydatek")
                .GroupBy(j => j.Data)
                .Select(k => new SplineChartData()
                {
                    dzien = k.First().Data.ToString("dd-MMM"),
                    wydatki = k.Sum(l => l.Cena)
                })
                .ToList();
           
            // wydatki i wplywy
            //
            string[] Ostatnie7Dni = Enumerable.Range(0,7)
                .Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from dzien in Ostatnie7Dni
                                      join wplywy in WplywyPodsumowanie on dzien equals wplywy.dzien into dzienWplywJoined
                                      from wplywy in dzienWplywJoined.DefaultIfEmpty()
                                      join wydatki in WydatkiPodsumowanie on dzien equals wydatki.dzien into wydatkiJoined
                                      from wydatki in wydatkiJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          dzien = dzien,
                                          wplywy = wplywy == null ? 0 : wplywy.wplywy,
                                          wydatki = wydatki == null ? 0 : wydatki.wydatki,
                                      };

            //najnowsze transakcje
            ViewBag.NajnowszeTransakcje = await _context.Transakcjes
                .Include(i => i.Kategoria)
                .OrderByDescending(j => j.Data)
                .Take(5)
                .ToListAsync();
            
            
            
            return View();
        }

    }

    public class SplineChartData
    {
        public string dzien;
        public int wplywy;
        public int wydatki;

    }

}
