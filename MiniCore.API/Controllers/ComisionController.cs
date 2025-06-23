using Microsoft.AspNetCore.Mvc;
using MiniCoreMultiCliente.MiniCore.Application.DTOs;
using MiniCoreMultiCliente.MiniCore.Application.Interfaces;

namespace MiniCoreMultiCliente.MiniCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComisionController : ControllerBase
    {
        private readonly IComisionService _comisionService;

        public ComisionController(IComisionService comisionService)
        {
            _comisionService = comisionService;
        }

        /// <summary>
        /// Calcula la comisión de un vendedor en base a las ventas realizadas dentro de un rango de fechas.
        /// </summary>
        /// <param name="request">DTO con clienteId, vendedorId, fechaInicio y fechaFin.</param>
        /// <returns>Comisión total y desglose por venta.</returns>
        [HttpPost("calcular")]
        public async Task<IActionResult> Calcular([FromBody] ComisionRequestDto request)
        {
            if (request == null)
                return BadRequest("El cuerpo de la solicitud no puede estar vacío.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _comisionService.CalcularComisionAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log interno si tienes un sistema de logs
                return StatusCode(500, $"Error interno al calcular la comisión: {ex.Message}");
            }
        }

    }
}
