using familiada.Hubs;
using familiada.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;
using System.IO;
using familiada.Services;

namespace familiada.Controllers
{
    public class RozgrywkaController : Controller
    {
        public static Zespol Czerwoni_Druzyna = new Zespol {Nazwa = "Czerwoni"};
        public static Zespol Niebiescy_Druzyna = new Zespol {Nazwa = "Niebiescy"};
		private readonly IHubContext<WynikiHub> _hub;
        private readonly PytaniaService service;
        public static int ktora_runda=0;
        private string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pytania.json");
		public IActionResult Admin()
        {
            var pytania = service.OdczytajPytania();

            var viewModel = new AdminViewModel
            {
                Zespoly = new List<Zespol> { Czerwoni_Druzyna, Niebiescy_Druzyna },
                Pytania = pytania,
                runda = ktora_runda
            };
            return View(viewModel);
        }
        public RozgrywkaController(PytaniaService pytaniaService, IHubContext<WynikiHub> hub) 
        {
            service = pytaniaService;
            _hub = hub;
        }
        public IActionResult Gracz()
        {
            return View();
        }
        public IActionResult Czerwoni()
        {
            return View();
        }
        public IActionResult Niebiescy() 
        {
            return View();
        }
        [HttpGet]
        public IActionResult przyciskrunda()
        {
            if (ktora_runda<=11)
            {
                ktora_runda++;
            }
            else
            {
                ktora_runda = 0;
            }
            PobierzPytaniePartial();
            _hub.Clients.All.SendAsync("OdswiezWyniki");
            return Json(new { sukces = true });
        }
        [HttpGet]
        public IActionResult OdejmijSzanse(string nazwa)
        {
            if (string.IsNullOrEmpty(nazwa))
            {
                return Json(new { sukces = false, komunikat = "Brak nazwy zespołu" });
            }
            if (nazwa.ToString() == "Czerwoni")
            {
                Czerwoni_Druzyna.szanse--;
            }
            else
            {
                Niebiescy_Druzyna.szanse--;
            }
            _hub.Clients.All.SendAsync("OdswiezWyniki");
            return Json(new { sukces = true });
        }
        [HttpGet]
        public IActionResult DodajPunkty(string nazwa, int punkty)
        {
            if (nazwa == "Czerwoni")
            {
                Czerwoni_Druzyna.punkty += punkty;
            }
            else if (nazwa=="Niebiescy")
            {
                Niebiescy_Druzyna.punkty += punkty;
            }
            _hub.Clients.All.SendAsync("OdswiezWyniki");
            return Json(new { sukces = true });
        }
        [HttpGet]
        public IActionResult PobierzPytaniePartial()
        {
            Czerwoni_Druzyna.szanse = 3;
            Niebiescy_Druzyna.szanse = 3;
            var pytania = service.OdczytajPytania();
            var model = new AdminViewModel
            {
                Zespoly = new List<Zespol> { Czerwoni_Druzyna, Niebiescy_Druzyna },
                Pytania = pytania,
                runda = ktora_runda
            };
            return PartialView("Pytanie", model);
        }
        [HttpGet]
        public IActionResult PobierzWynikiPartial()
        {
			var pytania = service.OdczytajPytania();
			var model = new AdminViewModel
			{
				Zespoly = new List<Zespol> { Czerwoni_Druzyna, Niebiescy_Druzyna },
				Pytania = pytania,
                runda=ktora_runda
			};
			return PartialView("Runda", model);
        }
        [HttpGet]
        public IActionResult ZarejestrujKlikniecie(string zespol)
        {
            if (zespol == "Czerwoni")
            {
                Czerwoni_Druzyna.Przycisniety = DateTime.Now;
            }
            else if(zespol=="Niebiescy")
            {
                Niebiescy_Druzyna.Przycisniety = DateTime.Now;
            }
            return Json(new { sukces = true });
        }

    }
}
