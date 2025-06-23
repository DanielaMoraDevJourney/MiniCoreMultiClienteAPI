namespace MiniCoreMultiCliente.MiniCore.Application.DTOs
{
    public class VentaComisionDetalleDto
    {
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
        public decimal PorcentajeAplicado { get; set; }
        public decimal Comision { get; set; }
    }
}
