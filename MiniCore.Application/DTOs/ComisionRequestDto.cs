namespace MiniCoreMultiCliente.MiniCore.Application.DTOs
{
    public class ComisionRequestDto
    {
        public int ClienteId { get; set; }
        public int VendedorId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
