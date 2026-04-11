using Microsoft.AspNetCore.Mvc;
using MVC_Practica3.Models;

namespace MVC_Practica3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _config;

        public HomeController(IHttpClientFactory http, IConfiguration config)
        {
            _http = http;
            _config = config;
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
    }
}