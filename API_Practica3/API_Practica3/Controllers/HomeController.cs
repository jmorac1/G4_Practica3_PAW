using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using API_Practica3.Models;

namespace API_Practica3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("ConsultarCompras")]
        public IActionResult ConsultarCompras()
        {
            using var context = new SqlConnection(_config.GetValue<string>("ConnectionStrings:DefaultConnection"));

            var lista = context.Query<CompraConsulta>(
                "SP_ConsultarCompras",
                commandType: CommandType.StoredProcedure
            ).ToList();

            return Ok(lista);
        }
    }
}