using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Application.DTOs;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;
using MiniCoreMultiCliente.MiniCore.Infrastructure.Data;

namespace MiniCoreMultiCliente.MiniCore.API.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene la lista de todos los clientes registrados.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var clientes = await _context.Clientes
                .Select(c => new { c.Id, c.Nombre })
                .ToListAsync();

            return Ok(clientes);
        }

        /// <summary>
        /// Crea un nuevo cliente con el nombre proporcionado.
        /// </summary>
        /// <param name="dto">DTO que contiene el nombre del cliente.</param>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateClienteDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("El nombre del cliente es obligatorio.");

            var cliente = new Cliente { Nombre = dto.Nombre };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }
    }
}
