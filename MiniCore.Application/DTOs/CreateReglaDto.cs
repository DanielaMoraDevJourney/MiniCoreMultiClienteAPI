namespace MiniCoreMultiCliente.MiniCore.Application.DTOs
{
    public class CreateReglaDto
    {
        public int ClienteId { get; set; }
        public decimal MinMonto { get; set; }
        public decimal MaxMonto { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
