using MiniCoreMultiCliente.MiniCore.Domain.Entities;

namespace MiniCoreMultiCliente.MiniCore.Application.Interfaces
{
    public interface IComisionRepository
    {
        Task<Vendedor?> ObtenerVendedorConClienteAsync(int vendedorId, int clienteId);
        Task<List<Venta>> ObtenerVentasPorVendedorYFechasAsync(int vendedorId, DateTime inicio, DateTime fin);
        Task<List<ReglaComision>> ObtenerReglasPorClienteAsync(int clienteId);
    }
}
