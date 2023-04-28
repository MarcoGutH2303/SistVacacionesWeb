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
    public class AutorizacionRepository : Repository, IAutorizacionRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public AutorizacionRepository()
        {
            _listar = "spListarAutorizacion";
            _grabar = "spGrabarAutorizacion";
            _recuperar = "spRecuperarAutorizacion";
            _eliminar = "spEliminarAutorizacion";
        }

        public List<AutorizacionModel> ListarAutorizacion(string codEmpresa)
        {
            List<AutorizacionModel> listAutorizacionModel = new List<AutorizacionModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AutorizacionModel oAutorizacionModel = new AutorizacionModel();
                                oAutorizacionModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oAutorizacionModel.IdAutorizacion = reader.IsDBNull(reader.GetOrdinal("IdAutorizacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdAutorizacion"));
                                oAutorizacionModel.CodAutorizacion = reader.IsDBNull(reader.GetOrdinal("CodAutorizacion")) ? "" : reader.GetString(reader.GetOrdinal("CodAutorizacion"));
                                oAutorizacionModel.CodSolicitud = reader.IsDBNull(reader.GetOrdinal("CodSolicitud")) ? "" : reader.GetString(reader.GetOrdinal("CodSolicitud"));
                                oAutorizacionModel.NombrePersonalSolicitante = reader.IsDBNull(reader.GetOrdinal("NombrePersonalSolicitante")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonalSolicitante"));
                                oAutorizacionModel.ApellidoPersonalSolicitante = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonalSolicitante")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonalSolicitante"));
                                oAutorizacionModel.NombreCompletoSolicitante = oAutorizacionModel.NombrePersonalSolicitante + " " + oAutorizacionModel.ApellidoPersonalSolicitante;
                                oAutorizacionModel.CodPersonalAutorizante = reader.IsDBNull(reader.GetOrdinal("CodPersonalAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonalAutorizante"));
                                oAutorizacionModel.NombrePersonalAutorizante = reader.IsDBNull(reader.GetOrdinal("NombrePersonalAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonalAutorizante"));
                                oAutorizacionModel.ApellidoPersonalAutorizante = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonalAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonalAutorizante"));
                                oAutorizacionModel.NombreCompletoAutorizante = oAutorizacionModel.NombrePersonalAutorizante + " " + oAutorizacionModel.ApellidoPersonalAutorizante;
                                oAutorizacionModel.FechaAutorizacion = reader.IsDBNull(reader.GetOrdinal("FechaAutorizacion")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaAutorizacion"));
                                oAutorizacionModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oAutorizacionModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oAutorizacionModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listAutorizacionModel.Add(oAutorizacionModel);
                            }
                            return listAutorizacionModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listAutorizacionModel;
            }
        }

        public int GrabarAutorizacion(AutorizacionModel oAutorizacionModel)
        {
            int result = 0;
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_grabar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodAutorizacion", oAutorizacionModel.CodAutorizacion);
                        cmd.Parameters.AddWithValue("@CodSolicitud", oAutorizacionModel.CodSolicitud);
                        cmd.Parameters.AddWithValue("@CodPersonalAutorizante", oAutorizacionModel.CodPersonalAutorizante);
                        cmd.Parameters.AddWithValue("@FechaAutorizacion", oAutorizacionModel.FechaAutorizacion);
                        cmd.Parameters.AddWithValue("@Estado", oAutorizacionModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oAutorizacionModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oAutorizacionModel.EstaBorrado);
                        result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public AutorizacionModel RecuperarAutorizacion(string codAutorizacion, string codEmpresa)
        {
            AutorizacionModel oAutorizacionModel = new AutorizacionModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodAutorizacion", codAutorizacion);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oAutorizacionModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oAutorizacionModel.IdAutorizacion = reader.IsDBNull(reader.GetOrdinal("IdAutorizacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdAutorizacion"));
                                oAutorizacionModel.CodAutorizacion = reader.IsDBNull(reader.GetOrdinal("CodAutorizacion")) ? "" : reader.GetString(reader.GetOrdinal("CodAutorizacion"));
                                oAutorizacionModel.CodSolicitud = reader.IsDBNull(reader.GetOrdinal("CodSolicitud")) ? "" : reader.GetString(reader.GetOrdinal("CodSolicitud"));
                                oAutorizacionModel.FechaSolicitud = reader.IsDBNull(reader.GetOrdinal("FechaSolicitud")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSolicitud"));
                                oAutorizacionModel.NombrePersonalSolicitante = reader.IsDBNull(reader.GetOrdinal("NombrePersonalSolicitante")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonalSolicitante"));
                                oAutorizacionModel.ApellidoPersonalSolicitante = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonalSolicitante")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonalSolicitante"));
                                oAutorizacionModel.NombreCompletoSolicitante = oAutorizacionModel.NombrePersonalSolicitante + " " + oAutorizacionModel.ApellidoPersonalSolicitante;
                                oAutorizacionModel.CodPersonalAutorizante = reader.IsDBNull(reader.GetOrdinal("CodPersonalAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonalAutorizante"));
                                oAutorizacionModel.NombrePersonalAutorizante = reader.IsDBNull(reader.GetOrdinal("NombrePersonalAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonalAutorizante"));
                                oAutorizacionModel.ApellidoPersonalAutorizante = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonalAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonalAutorizante"));
                                oAutorizacionModel.NombreCompletoAutorizante = oAutorizacionModel.NombrePersonalAutorizante + " " + oAutorizacionModel.ApellidoPersonalAutorizante;
                                oAutorizacionModel.FechaAutorizacion = reader.IsDBNull(reader.GetOrdinal("FechaAutorizacion")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaAutorizacion"));
                                oAutorizacionModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oAutorizacionModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oAutorizacionModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oAutorizacionModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oAutorizacionModel;
            }
        }

        public int EliminarAutorizacionFisico(string codAutorizacion, string codEmpresa)
        {
            int result = 0;
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_eliminar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodAutorizacion", codAutorizacion);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public int EliminarAutorizacionLogico(AutorizacionModel oAutorizacionModel)
        {
            int result = 0;
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_grabar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodAutorizacion", oAutorizacionModel.CodAutorizacion);
                        cmd.Parameters.AddWithValue("@CodSolicitud", oAutorizacionModel.CodSolicitud);
                        cmd.Parameters.AddWithValue("@CodPersonalAutorizante", oAutorizacionModel.CodPersonalAutorizante);
                        cmd.Parameters.AddWithValue("@FechaAutorizacion", oAutorizacionModel.FechaAutorizacion);
                        cmd.Parameters.AddWithValue("@Estado", oAutorizacionModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oAutorizacionModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oAutorizacionModel.EstaBorrado);
                        result = cmd.ExecuteNonQuery();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public void Dispose()
        {

        }
    }
}
