using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Domain.Entities;

namespace MiniCoreMultiCliente.MiniCore.Application.Strategies
{
    public class ReglaComisionPremium : IReglaComisionEstrategia
    {
        public decimal Calcular(decimal monto, List<ReglaComision> reglas)
        {
            var regla = reglas.OrderByDescending(r => r.Porcentaje).FirstOrDefault();
            var porcentaje = (regla?.Porcentaje ?? 0) + 2; // Bonus 2% extra por ser premium
            return monto * (porcentaje / 100m);
        }
    }

}
