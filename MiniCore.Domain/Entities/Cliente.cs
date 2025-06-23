namespace MiniCoreMultiCliente.MiniCore.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();
        public ICollection<ReglaComision> Reglas { get; set; } = new List<ReglaComision>();
    }
}
