namespace MiniCoreMultiCliente.MiniCore.Application.Interfaces
{
    public interface IReglaComisionFactory
    {
        IReglaComisionEstrategia ObtenerEstrategia(string tipoCliente);
    }

}
