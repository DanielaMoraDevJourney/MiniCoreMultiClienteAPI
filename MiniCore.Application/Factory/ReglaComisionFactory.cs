using MiniCoreMultiCliente.MiniCore.Application.Interfaces;
using MiniCoreMultiCliente.MiniCore.Application.Strategies;

namespace MiniCoreMultiCliente.MiniCore.Application.Factory
{
    public class ReglaComisionFactory : IReglaComisionFactory
    {
        private readonly IServiceProvider _provider;

        public ReglaComisionFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IReglaComisionEstrategia ObtenerEstrategia(string tipoCliente)
        {
            return tipoCliente switch
            {
                "premium" => _provider.GetRequiredService<ReglaComisionPremium>(),
                _ => _provider.GetRequiredService<ReglaComisionEstandar>()
            };
        }
    }

}
