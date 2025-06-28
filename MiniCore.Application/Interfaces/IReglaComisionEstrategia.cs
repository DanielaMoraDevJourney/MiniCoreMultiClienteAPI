using MiniCoreMultiCliente.MiniCore.Domain.Entities;

namespace MiniCoreMultiCliente.MiniCore.Application.Interfaces
{
    public interface IReglaComisionEstrategia
    {
        decimal Calcular(decimal monto, List<ReglaComision> reglas);
    }

}
