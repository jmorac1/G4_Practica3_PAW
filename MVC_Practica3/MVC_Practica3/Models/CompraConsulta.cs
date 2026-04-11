namespace MVC_Practica3.Models
{
    public class CompraConsulta
    {
        public long Id_Compra { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public decimal Saldo { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
}