using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniCoreMultiCliente.MiniCore.Application.DTOs;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;
using MiniCoreMultiCliente.MiniCore.Infrastructure.Data;

namespace MiniCoreMultiCliente.MiniCore.API.Controllers
{
    [ApiController]
    [Route("api/vendedores")]
    public class VendedoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VendedoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos los vendedores asociados a un cliente específico.
        /// </summary>
        /// <param name="id">ID del cliente</param>
        [HttpGet("cliente/{id}")]
        public async Task<IActionResult> ObtenerVendedoresPorCliente(int id)
        {
            var vendedores = await _context.Vendedores
                .Where(v => v.ClienteId == id)
                .Select(v => new { v.Id, v.Nombre })
                .ToListAsync();

            return Ok(vendedores);
        }

        /// <summary>
        /// Retorna el detalle de un vendedor por ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var vendedor = await _context.Vendedores
                .Include(v => v.Cliente)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vendedor == null)
                return NotFound();

            return Ok(new
            {
                vendedor.Id,
                vendedor.Nombre,
                Cliente = vendedor.Cliente.Nombre
            });
        }

        /// <summary>
        /// Crea un nuevo vendedor asociado a un cliente.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateVendedorDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre) || dto.ClienteId <= 0)
                return BadRequest("El nombre y clienteId son obligatorios.");

            var vendedor = new Vendedor
            {
                Nombre = dto.Nombre,
                ClienteId = dto.ClienteId
            };

            _context.Vendedores.Add(vendedor);
            await _context.SaveChangesAsync();
            return Ok(vendedor);
        }

        /// <summary>
        /// Obtiene todos los vendedores del sistema.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var vendedores = await _context.Vendedores
                .Include(v => v.Cliente)
                .Select(v => new
                {
                    v.Id,
                    v.Nombre,
                    ClienteNombre = v.Cliente.Nombre
                })
                .ToListAsync();

            return Ok(vendedores);
        }

    }
}
