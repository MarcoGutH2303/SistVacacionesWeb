using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.DataAccessLayer.Repository
{
    public class PanelControlRepository : Repository, IPanelControlRepository
    {
        private readonly string _recuperarPanelAdministrador;
        private readonly string _recuperarPanelEmpleado;
        
        public PanelControlRepository()
        {
            _recuperarPanelAdministrador = "spPanelControlAdministrador";
            _recuperarPanelEmpleado = "spPanelControlEmpleado";
        }

        public PanelControlAdministradorModel RecuperarPanelControlAdministrador(string codEmpresa)
        {
            PanelControlAdministradorModel oPanelControlAdministradorModel = new PanelControlAdministradorModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperarPanelAdministrador, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPanelControlAdministradorModel.CantSolicitudPendiente = reader.IsDBNull(reader.GetOrdinal("CantSolicitudPendiente")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantSolicitudPendiente"));
                                oPanelControlAdministradorModel.CantSolicitudResuelto = reader.IsDBNull(reader.GetOrdinal("CantSolicitudResuelto")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantSolicitudResuelto"));
                                oPanelControlAdministradorModel.CantAutorizacionRealizado = reader.IsDBNull(reader.GetOrdinal("CantAutorizacionRealizado")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantAutorizacionRealizado"));
                                oPanelControlAdministradorModel.CantVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CantVacacionesPeriodo")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantVacacionesPeriodo"));
                            }
                            return oPanelControlAdministradorModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oPanelControlAdministradorModel;
            }
        }

        public PanelControlEmpleadoModel RecuperarPanelControlEmpleado(string codPersonal, string codEmpresa)
        {
            PanelControlEmpleadoModel oPanelControlEmpleadoModel = new PanelControlEmpleadoModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperarPanelEmpleado, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPanelControlEmpleadoModel.CantSolicitudPendiente = reader.IsDBNull(reader.GetOrdinal("CantSolicitudPendiente")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantSolicitudPendiente"));
                                oPanelControlEmpleadoModel.CantAutorizacionRealizado = reader.IsDBNull(reader.GetOrdinal("CantAutorizacionRealizado")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantAutorizacionRealizado"));
                                oPanelControlEmpleadoModel.CantVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CantVacacionesPeriodo")) ? 0 : reader.GetInt32(reader.GetOrdinal("CantVacacionesPeriodo"));
                            }
                            return oPanelControlEmpleadoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oPanelControlEmpleadoModel;
            }
        }
    }
}
