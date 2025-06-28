using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;

namespace MiniCoreMultiCliente.MiniCore.Application.Strategies
{
    public class ReglaComisionEstandar : IReglaComisionEstrategia
    {
        public decimal Calcular(decimal monto, List<ReglaComision> reglas)
        {
            var regla = reglas.FirstOrDefault(r => monto >= r.MinMonto && monto <= r.MaxMonto);
            return monto * ((regla?.Porcentaje ?? 0) / 100m);
        }
    }
}
