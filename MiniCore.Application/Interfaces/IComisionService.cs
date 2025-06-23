using MiniCoreMultiCliente.MiniCore.Application.DTOs;

namespace MiniCoreMultiCliente.MiniCore.Application.Interfaces
{
    public interface IComisionService
    {
        Task<ComisionResponseDto> CalcularComisionAsync(ComisionRequestDto request);
    }
}
