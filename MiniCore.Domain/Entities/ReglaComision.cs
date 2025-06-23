namespace MiniCoreMultiCliente.MiniCore.Domain.Entities
{
    public class ReglaComision
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public decimal MinMonto { get; set; }
        public decimal MaxMonto { get; set; }
        public decimal Porcentaje { get; set; }
    }
}
