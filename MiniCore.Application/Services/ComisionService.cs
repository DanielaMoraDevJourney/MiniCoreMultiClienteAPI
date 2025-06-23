using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Application.DTOs;
using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Infrastructure.Data;

namespace MiniCoreMultiCliente.MiniCore.Application.Services
{

    public class ComisionService : IComisionService
    {
        private readonly ApplicationDbContext _context;

        public ComisionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ComisionResponseDto> CalcularComisionAsync(ComisionRequestDto request)
        {
            var vendedor = await _context.Vendedores
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == request.VendedorId && v.ClienteId == request.ClienteId);

            if (vendedor == null)
                throw new Exception("Vendedor no encontrado para el cliente.");

            var ventas = await _context.Ventas
                .Where(v => v.VendedorId == request.VendedorId &&
                            v.Fecha >= request.FechaInicio &&
                            v.Fecha <= request.FechaFin)
                .ToListAsync();

            if (!ventas.Any())
                throw new Exception("No se encontraron ventas para este vendedor en el rango de fechas proporcionado.");

            var reglas = await _context.ReglasComision
                .Where(r => r.ClienteId == request.ClienteId)
                .ToListAsync();

            if (!reglas.Any())
                throw new Exception("No hay reglas de comisión definidas para este cliente.");

            var detalles = new List<VentaComisionDetalleDto>();
            decimal total = 0;

            foreach (var venta in ventas)
            {
                var regla = reglas.FirstOrDefault(r =>
                    venta.Monto >= r.MinMonto && venta.Monto <= r.MaxMonto);

                decimal porcentaje = regla?.Porcentaje ?? 0;
                decimal comision = venta.Monto * (porcentaje / 100m);
                total += comision;

                detalles.Add(new VentaComisionDetalleDto
                {
                    Fecha = venta.Fecha,
                    Monto = venta.Monto,
                    PorcentajeAplicado = porcentaje,
                    Comision = comision
                });
            }

            return new ComisionResponseDto
            {
                ClienteId = request.ClienteId,
                VendedorId = request.VendedorId,
                NombreVendedor = vendedor.Nombre,
                TotalComision = total,
                Detalles = detalles
            };
        }

    }
}