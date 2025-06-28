using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;

namespace MiniCoreMultiCliente.MiniCore.Infrastructure.Data
{
    public class ComisionRepository : IComisionRepository
    {
        private readonly ApplicationDbContext _context;

        public ComisionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Vendedor?> ObtenerVendedorConClienteAsync(int vendedorId, int clienteId)
        {
            return await _context.Vendedores
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == vendedorId && v.ClienteId == clienteId);
        }

        public async Task<List<Venta>> ObtenerVentasPorVendedorYFechasAsync(int vendedorId, DateTime inicio, DateTime fin)
        {
            return await _context.Ventas
                .Where(v => v.VendedorId == vendedorId && v.Fecha >= inicio && v.Fecha <= fin)
                .ToListAsync();
        }

        public async Task<List<ReglaComision>> ObtenerReglasPorClienteAsync(int clienteId)
        {
            return await _context.ReglasComision
                .Where(r => r.ClienteId == clienteId)
                .ToListAsync();
        }
    }
}
