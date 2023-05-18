
namespace Dominio
{
    public static class ConfigVestillo
    {


        public static void Iniciar(string connectionString,int empresa )
        {
            Vestillo.Connection.ProviderFactory.StringConnection = connectionString;
            Vestillo.Lib.Funcoes.SetIdEmpresaLogada = empresa;
            
        }
       
    }
}
