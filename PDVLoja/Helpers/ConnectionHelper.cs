using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDVLoja.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            // Para multi-caixa: Altere para IP do servidor, ex: "Server=192.168.1.100\\SQLEXPRESS;Database=PDV_LojaDB;User Id=sa;Password=senha;"
            return "Server=(localdb)\\mssqllocaldb;Database=PDV_LojaDB;Trusted_Connection=True;";
        }
    }
}
