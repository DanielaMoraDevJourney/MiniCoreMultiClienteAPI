namespace MiniCoreMultiCliente.MiniCore.Domain.Entities
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
