using CrystalDecisions.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistVacacionesWeb.UILayer.Reports
{
    public class ConexionReports
    {
        public static CrystalDecisions.Shared.ConnectionInfo GetConnectionInfo()
        {
            var cadenaSql = ConfigurationManager.ConnectionStrings["cn"].ConnectionString;
            SqlConnectionStringBuilder cn = new SqlConnectionStringBuilder(cadenaSql);

            CrystalDecisions.Shared.ConnectionInfo cnInfo = new CrystalDecisions.Shared.ConnectionInfo();
            cnInfo.ServerName = cn.DataSource;
            cnInfo.DatabaseName = cn.InitialCatalog;
            cnInfo.UserID = cn.UserID;
            cnInfo.Password = cn.Password;
            cnInfo.IntegratedSecurity = false;
            cnInfo.Type = ConnectionInfoType.SQL;
            cnInfo.AllowCustomConnection = true;
            return cnInfo;
        }
    }
}