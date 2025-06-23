using Microsoft.AspNetCore.Mvc;
using MiniCoreMultiCliente.MiniCore.Application.DTOs;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;
using MiniCoreMultiCliente.MiniCore.Infrastructure.Data;

namespace MiniCoreMultiCliente.MiniCore.API.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Registra una nueva venta asociada a un vendedor.
        /// </summary>
        /// <param name="dto">DTO con vendedorId, fecha y monto.</param>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateVentaDto dto)
        {
            if (dto.VendedorId <= 0 || dto.Monto <= 0)
                return BadRequest("Debe especificarse un vendedor válido y un monto mayor a cero.");

            var venta = new Venta
            {
                VendedorId = dto.VendedorId,
                Fecha = dto.Fecha,
                Monto = dto.Monto
            };

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            return Ok(venta);
        }
    }
}
