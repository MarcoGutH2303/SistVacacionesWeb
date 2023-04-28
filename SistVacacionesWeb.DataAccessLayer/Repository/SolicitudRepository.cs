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
    public class SolicitudRepository : Repository, ISolicitudRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public SolicitudRepository()
        {
            _listar = "spListarSolicitud";
            _grabar = "spGrabarSolicitud";
            _recuperar = "spRecuperarSolicitud";
            _eliminar = "spEliminarSolicitud";
        }

        public List<SolicitudModel> ListarSolicitud(string codEmpresa)
        {
            List<SolicitudModel> listSolicitudModel = new List<SolicitudModel>();
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
                                SolicitudModel oSolicitudModel = new SolicitudModel();
                                oSolicitudModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oSolicitudModel.IdSolicitud = reader.IsDBNull(reader.GetOrdinal("IdSolicitud")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdSolicitud"));
                                oSolicitudModel.CodSolicitud = reader.IsDBNull(reader.GetOrdinal("CodSolicitud")) ? "" : reader.GetString(reader.GetOrdinal("CodSolicitud"));
                                oSolicitudModel.FechaSolicitud = reader.IsDBNull(reader.GetOrdinal("FechaSolicitud")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSolicitud"));
                                oSolicitudModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oSolicitudModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oSolicitudModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oSolicitudModel.NombreCompletoPersonal = oSolicitudModel.NombrePersonal + " " + oSolicitudModel.ApellidoPersonal;
                                oSolicitudModel.CodConcepto = reader.IsDBNull(reader.GetOrdinal("CodConcepto")) ? "" : reader.GetString(reader.GetOrdinal("CodConcepto"));
                                oSolicitudModel.NombreConcepto = reader.IsDBNull(reader.GetOrdinal("NombreConcepto")) ? "" : reader.GetString(reader.GetOrdinal("NombreConcepto"));
                                oSolicitudModel.FechaSalida = reader.IsDBNull(reader.GetOrdinal("FechaSalida")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSalida"));
                                oSolicitudModel.FechaRetorno = reader.IsDBNull(reader.GetOrdinal("FechaRetorno")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaRetorno"));
                                oSolicitudModel.NumeroDias = reader.IsDBNull(reader.GetOrdinal("NumeroDias")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroDias"));
                                oSolicitudModel.NumeroHoras = reader.IsDBNull(reader.GetOrdinal("NumeroHoras")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroHoras"));
                                oSolicitudModel.NumeroMinutos = reader.IsDBNull(reader.GetOrdinal("NumeroMinutos")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroMinutos"));
                                oSolicitudModel.TiempoCompleto = oSolicitudModel.NumeroDias.ToString() + " Dia(s), " + oSolicitudModel.NumeroHoras.ToString() + " Hora(s), " + oSolicitudModel.NumeroMinutos.ToString() + " Minuto(s)";
                                oSolicitudModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oSolicitudModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oSolicitudModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listSolicitudModel.Add(oSolicitudModel);
                            }
                            return listSolicitudModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listSolicitudModel;
            }
        }

        public int GrabarSolicitud(SolicitudModel oSolicitudModel)
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
                        cmd.Parameters.AddWithValue("@CodSolicitud", oSolicitudModel.CodSolicitud);
                        cmd.Parameters.AddWithValue("@FechaSolicitud", oSolicitudModel.FechaSolicitud);
                        cmd.Parameters.AddWithValue("@CodPersonal", oSolicitudModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodConcepto", oSolicitudModel.CodConcepto);
                        cmd.Parameters.AddWithValue("@FechaSalida", oSolicitudModel.FechaSalida);
                        cmd.Parameters.AddWithValue("@FechaRetorno", oSolicitudModel.FechaRetorno);
                        cmd.Parameters.AddWithValue("@NumeroDias", oSolicitudModel.NumeroDias);
                        cmd.Parameters.AddWithValue("@NumeroHoras", oSolicitudModel.NumeroHoras);
                        cmd.Parameters.AddWithValue("@NumeroMinutos", oSolicitudModel.NumeroMinutos);
                        cmd.Parameters.AddWithValue("@Estado", oSolicitudModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oSolicitudModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oSolicitudModel.EstaBorrado);
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

        public SolicitudModel RecuperarSolicitud(string codSolicitud, string codEmpresa)
        {
            SolicitudModel oSolicitudModel = new SolicitudModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodSolicitud", codSolicitud);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oSolicitudModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oSolicitudModel.IdSolicitud = reader.IsDBNull(reader.GetOrdinal("IdSolicitud")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdSolicitud"));
                                oSolicitudModel.CodSolicitud = reader.IsDBNull(reader.GetOrdinal("CodSolicitud")) ? "" : reader.GetString(reader.GetOrdinal("CodSolicitud"));
                                oSolicitudModel.FechaSolicitud = reader.IsDBNull(reader.GetOrdinal("FechaSolicitud")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSolicitud"));
                                oSolicitudModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oSolicitudModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oSolicitudModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oSolicitudModel.NombreCompletoPersonal = oSolicitudModel.NombrePersonal + " " + oSolicitudModel.ApellidoPersonal;
                                oSolicitudModel.CodConcepto = reader.IsDBNull(reader.GetOrdinal("CodConcepto")) ? "" : reader.GetString(reader.GetOrdinal("CodConcepto"));
                                oSolicitudModel.NombreConcepto = reader.IsDBNull(reader.GetOrdinal("NombreConcepto")) ? "" : reader.GetString(reader.GetOrdinal("NombreConcepto"));
                                oSolicitudModel.FechaSalida = reader.IsDBNull(reader.GetOrdinal("FechaSalida")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSalida"));
                                oSolicitudModel.FechaRetorno = reader.IsDBNull(reader.GetOrdinal("FechaRetorno")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaRetorno"));
                                oSolicitudModel.NumeroDias = reader.IsDBNull(reader.GetOrdinal("NumeroDias")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroDias"));
                                oSolicitudModel.NumeroHoras = reader.IsDBNull(reader.GetOrdinal("NumeroHoras")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroHoras"));
                                oSolicitudModel.NumeroMinutos = reader.IsDBNull(reader.GetOrdinal("NumeroMinutos")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroMinutos"));
                                oSolicitudModel.TiempoCompleto = oSolicitudModel.NumeroDias.ToString() + " Dia(s), " + oSolicitudModel.NumeroHoras.ToString() + " Hora(s), " + oSolicitudModel.NumeroMinutos.ToString() + " Minuto(s)";
                                oSolicitudModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oSolicitudModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oSolicitudModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oSolicitudModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oSolicitudModel;
            }
        }

        public int EliminarSolicitudFisico(string codSolicitud, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodSolicitud", codSolicitud);
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

        public int EliminarSolicitudLogico(SolicitudModel oSolicitudModel)
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
                        cmd.Parameters.AddWithValue("@CodSolicitud", oSolicitudModel.CodSolicitud);
                        cmd.Parameters.AddWithValue("@FechaSolicitud", oSolicitudModel.FechaSolicitud);
                        cmd.Parameters.AddWithValue("@CodPersonal", oSolicitudModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodConcepto", oSolicitudModel.CodConcepto);
                        cmd.Parameters.AddWithValue("@FechaSalida", oSolicitudModel.FechaSalida);
                        cmd.Parameters.AddWithValue("@FechaRetorno", oSolicitudModel.FechaRetorno);
                        cmd.Parameters.AddWithValue("@NumeroDias", oSolicitudModel.NumeroDias);
                        cmd.Parameters.AddWithValue("@NumeroHoras", oSolicitudModel.NumeroHoras);
                        cmd.Parameters.AddWithValue("@NumeroMinutos", oSolicitudModel.NumeroMinutos);
                        cmd.Parameters.AddWithValue("@Estado", oSolicitudModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oSolicitudModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oSolicitudModel.EstaBorrado);
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
