namespace MiniCoreMultiCliente.MiniCore.Application.DTOs
{
    public class ComisionResponseDto
    {
        public string NombreVendedor { get; set; } = string.Empty;
        public int VendedorId { get; set; }
        public int ClienteId { get; set; }
        public decimal TotalComision { get; set; }
        public List<VentaComisionDetalleDto> Detalles { get; set; } = new();
    }
}
