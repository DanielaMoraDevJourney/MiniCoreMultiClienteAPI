using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Application.DTOs;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;
using MiniCoreMultiCliente.MiniCore.Infrastructure.Data;

namespace MiniCoreMultiCliente.MiniCore.API.Controllers
{
    [ApiController]
    [Route("api/reglas")]
    public class ReglasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReglasController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Crea una nueva regla de comisión para un cliente específico.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateReglaDto dto)
        {
            if (dto.Porcentaje < 0 || dto.MinMonto < 0 || dto.MaxMonto <= dto.MinMonto)
                return BadRequest("Datos inválidos para la regla de comisión.");

            var regla = new ReglaComision
            {
                ClienteId = dto.ClienteId,
                MinMonto = dto.MinMonto,
                MaxMonto = dto.MaxMonto,
                Porcentaje = dto.Porcentaje
            };

            _context.ReglasComision.Add(regla);
            await _context.SaveChangesAsync();
            return Ok(regla);
        }

        /// <summary>
        /// Obtiene todas las reglas de comisión asociadas a un cliente.
        /// </summary>
        [HttpGet("{id}/reglas")]
        public async Task<IActionResult> ObtenerReglas(int id)
        {
            var reglas = await _context.ReglasComision
                .Where(r => r.ClienteId == id)
                .Select(r => new
                {
                    r.Id,
                    r.MinMonto,
                    r.MaxMonto,
                    r.Porcentaje
                })
                .ToListAsync();

            return Ok(reglas);
        }
    }
}
