using Microsoft.AspNetCore.Mvc;
using MVC_Practica3.Models;
using System.Text;
using System.Text.Json;

namespace MVC_Practica3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _config;
        private readonly string _baseUrl;

        public HomeController(IHttpClientFactory http, IConfiguration config)
        {
            _http = http;
            _config = config;
            _baseUrl = _config.GetValue<string>("Valores:UrlAPI")!;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Consulta()
        {
            var client = _http.CreateClient();
            var baseUrl = _config.GetValue<string>("Valores:UrlAPI");
            var url = baseUrl + "Home/ConsultarCompras";

            var lista = await client.GetFromJsonAsync<List<CompraConsulta>>(url);
            return View(lista ?? new List<CompraConsulta>());
        }

        // GET /Abono/Registro
        [HttpGet]
        public async Task<IActionResult> Registro()
        {
            var http = _http.CreateClient();

            var response = await http.GetAsync($"{_baseUrl}Home/ComprasPendientes");
            var json = await response.Content.ReadAsStringAsync();
            var pendientes = JsonSerializer.Deserialize<List<CompraConsulta>>(json,
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                            ?? new List<CompraConsulta>();

            ViewBag.Compras = pendientes;
            return View();
        }

        // POST /Abono/Registro
        [HttpPost]
        public async Task<IActionResult> Registro(AbonoRequest request)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Registro");

            var http = _http.CreateClient();

            var body = new StringContent(
                           JsonSerializer.Serialize(request),
                           Encoding.UTF8,
                           "application/json");

            await http.PostAsync($"{_baseUrl}Home/Registrar", body);

            return RedirectToAction("Consulta");
        }
    }
}