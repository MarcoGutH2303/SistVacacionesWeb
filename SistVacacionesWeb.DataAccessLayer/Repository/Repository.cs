using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.DataAccessLayer.Repository
{
    public class Repository
    {
        public string cadena { get; set; }
        public string llave { get; set; }

        public Repository()
        {
            cadena = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            llave = "SistVacacionesWeb";
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(cadena);
        } 
    }
}
