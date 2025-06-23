namespace MiniCoreMultiCliente.MiniCore.Domain.Entities
{
    public class Venta
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Monto { get; set; }

        public int VendedorId { get; set; }
        public Vendedor Vendedor { get; set; } = null!;
    }
}
