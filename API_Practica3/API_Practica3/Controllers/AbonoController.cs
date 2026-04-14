using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using API_Practica3.Models;

namespace API_Practica3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbonoController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AbonoController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/Abono/ComprasPendientes
        [HttpGet("ComprasPendientes")]
        public IActionResult ComprasPendientes()
        {
            using var cn = new SqlConnection(
                _config.GetValue<string>("ConnectionStrings:DefaultConnection"));

            var lista = cn.Query<CompraConsulta>(
                "SP_ConsultarComprasPendientes",
                commandType: CommandType.StoredProcedure
            ).ToList();

            return Ok(lista);
        }

        // GET api/Abono/SaldoCompra/{id}
        [HttpGet("SaldoCompra/{id}")]
        public IActionResult SaldoCompra(long id)
        {
            using var cn = new SqlConnection(
                _config.GetValue<string>("ConnectionStrings:DefaultConnection"));

            var saldo = cn.QueryFirstOrDefault<decimal>(
                "SELECT Saldo FROM Principal WHERE Id_Compra = @Id",
                new { Id = id }
            );

            return Ok(new { saldo });
        }

        // POST api/Abono/Registrar
        [HttpPost("Registrar")]
        public IActionResult Registrar([FromBody] AbonoRequest request)
        {
            using var cn = new SqlConnection(
                _config.GetValue<string>("ConnectionStrings:DefaultConnection"));

            var parametros = new DynamicParameters();
            parametros.Add("@Id_Compra", request.Id_Compra);
            parametros.Add("@Monto", request.Monto);

            cn.Execute(
                "SP_RegistrarAbono",
                parametros,
                commandType: CommandType.StoredProcedure
            );

            return Ok(new { mensaje = "Abono registrado correctamente." });
        }
    }
}