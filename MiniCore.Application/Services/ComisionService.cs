using MiniCoreMultiCliente.MiniCore.Application.DTOs;
using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;

namespace MiniCoreMultiCliente.MiniCore.Application.Services
{
    public class ComisionService : IComisionService
    {
        private readonly IComisionRepository _repo;
        private readonly IReglaComisionFactory _factory;

        public ComisionService(IComisionRepository repo, IReglaComisionFactory factory)
        {
            _repo = repo;
            _factory = factory;
        }

        public async Task<ComisionResponseDto> CalcularComisionAsync(ComisionRequestDto request)
        {
            var vendedor = await _repo.ObtenerVendedorConClienteAsync(request.VendedorId, request.ClienteId);
            if (vendedor == null)
                throw new Exception("Vendedor no encontrado para el cliente.");

            var ventas = await _repo.ObtenerVentasPorVendedorYFechasAsync(request.VendedorId, request.FechaInicio, request.FechaFin);
            if (!ventas.Any())
                throw new Exception("No se encontraron ventas.");

            var reglas = await _repo.ObtenerReglasPorClienteAsync(request.ClienteId);
            if (!reglas.Any())
                throw new Exception("No hay reglas de comisión.");

            string tipoCliente = "estandar";
            var estrategia = _factory.ObtenerEstrategia(tipoCliente);

            var detalles = new List<VentaComisionDetalleDto>();
            decimal total = 0;

            foreach (var venta in ventas)
            {
                decimal comision = estrategia.Calcular(venta.Monto, reglas);
                decimal porcentaje = reglas.FirstOrDefault(r => venta.Monto >= r.MinMonto && venta.Monto <= r.MaxMonto)?.Porcentaje ?? 0;

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
