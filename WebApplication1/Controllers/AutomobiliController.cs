using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication1.Controllers
{
    public class AutomobiliController : Controller
    {
        private static List<Automobil> automobili = new List<Automobil>
        {
            new Automobil
            {
                Id = 1,
                Marka = "Audi",
                Model = "A4",
                Godiste = 2018,
                ZapreminaMotora = 2.0,
                Snaga = 150,
                Gorivo = "Dizel",
                Karoserija = "Limuzina",
                Opis = "Odličan auto, redovno servisiran.",
                Cena = 15500,
                Kontakt = "065/123-4567"
            },
            new Automobil
            {
                Id = 2,
                Marka = "Volkswagen",
                Model = "Golf 7",
                Godiste = 2016,
                ZapreminaMotora = 1.6,
                Snaga = 110,
                Gorivo = "Dizel",
                Karoserija = "Hečbek",
                Opis = "U odličnom stanju, mali potrošač.",
                Cena = 11900,
                Kontakt = "064/987-6543"
            }
        };

        public IActionResult Index()
        {
            return View(automobili);
        }

        public IActionResult Details(int id)
        {
            var auto = automobili.FirstOrDefault(a => a.Id == id);
            if (auto == null)
            {
                return NotFound();
            }
            return View(auto);
        }

        public IActionResult Delete(int id)
        {
            if (!AdminController.IsAdmin(HttpContext))
            {
                return Unauthorized(); 
            }

            var auto = automobili.FirstOrDefault(a => a.Id == id);
            if (auto != null)
            {
                automobili.Remove(auto);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Automobil auto)
        {
            if (ModelState.IsValid)
            {
                auto.Id = automobili.Any() ? automobili.Max(a => a.Id) + 1 : 1;
                automobili.Add(auto);
                return RedirectToAction(nameof(Index));
            }
            return View(auto);
        }
    }
}