namespace MiniCoreMultiCliente.MiniCore.Application.DTOs
{
    public class CreateVentaDto
    {
        public int VendedorId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }
    }
}
