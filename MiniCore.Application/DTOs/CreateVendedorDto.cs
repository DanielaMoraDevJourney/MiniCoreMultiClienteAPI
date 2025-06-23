namespace MiniCoreMultiCliente.MiniCore.Application.DTOs
{
    public class CreateVendedorDto
    {
        public string Nombre { get; set; } = string.Empty;
        public int ClienteId { get; set; }
    }
}
