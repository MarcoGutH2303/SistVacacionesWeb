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
    public class PanelAutorizacionRepository : Repository, IPanelAutorizacionRepository
    {
        private readonly string _listar;
        private readonly string _recuperar;

        public PanelAutorizacionRepository()
        {
            _listar = "spListarAutorizacionPersonal";
            _recuperar = "spRecuperarAutorizacionPersonal";
        }

        public List<PanelAutorizacionModel> ListarAutorizacionPersonal(string codPersonal, string codEmpresa)
        {
            List<PanelAutorizacionModel> listPanelAutorizacionModel = new List<PanelAutorizacionModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PanelAutorizacionModel oPanelAutorizacionModel = new PanelAutorizacionModel();
                                oPanelAutorizacionModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oPanelAutorizacionModel.CodAutorizacion = reader.IsDBNull(reader.GetOrdinal("CodAutorizacion")) ? "" : reader.GetString(reader.GetOrdinal("CodAutorizacion"));
                                oPanelAutorizacionModel.CodSolicitud = reader.IsDBNull(reader.GetOrdinal("CodSolicitud")) ? "" : reader.GetString(reader.GetOrdinal("CodSolicitud"));
                                oPanelAutorizacionModel.FechaSolicitud = reader.IsDBNull(reader.GetOrdinal("FechaSolicitud")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSolicitud"));
                                oPanelAutorizacionModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oPanelAutorizacionModel.NombreCompletoPersonal = reader.IsDBNull(reader.GetOrdinal("NombreCompletoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombreCompletoPersonal"));
                                oPanelAutorizacionModel.CodConcepto = reader.IsDBNull(reader.GetOrdinal("CodConcepto")) ? "" : reader.GetString(reader.GetOrdinal("CodConcepto"));
                                oPanelAutorizacionModel.NombreConcepto= reader.IsDBNull(reader.GetOrdinal("NombreConcepto")) ? "" : reader.GetString(reader.GetOrdinal("NombreConcepto"));
                                oPanelAutorizacionModel.FechaSalida = reader.IsDBNull(reader.GetOrdinal("FechaSalida")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSalida"));
                                oPanelAutorizacionModel.FechaRetorno = reader.IsDBNull(reader.GetOrdinal("FechaRetorno")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaRetorno"));
                                oPanelAutorizacionModel.NumeroDias = reader.IsDBNull(reader.GetOrdinal("NumeroDias")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroDias"));
                                oPanelAutorizacionModel.NumeroHoras = reader.IsDBNull(reader.GetOrdinal("NumeroHoras")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroHoras"));
                                oPanelAutorizacionModel.NumeroMinutos = reader.IsDBNull(reader.GetOrdinal("NumeroMinutos")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroMinutos"));
                                oPanelAutorizacionModel.TiempoCompleto = oPanelAutorizacionModel.NumeroDias.ToString() + " Dia(s), " + oPanelAutorizacionModel.NumeroHoras.ToString() + " Hora(s), " + oPanelAutorizacionModel.NumeroMinutos.ToString() + " Minuto(s)";
                                oPanelAutorizacionModel.NombreCompletoAutorizante = reader.IsDBNull(reader.GetOrdinal("NombreCompletoAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("NombreCompletoAutorizante"));
                                oPanelAutorizacionModel.FechaAutorizacion = reader.IsDBNull(reader.GetOrdinal("FechaAutorizacion")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaAutorizacion"));
                                oPanelAutorizacionModel.EstadoAutorizacion = reader.IsDBNull(reader.GetOrdinal("EstadoAutorizacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("EstadoAutorizacion"));
                                oPanelAutorizacionModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                listPanelAutorizacionModel.Add(oPanelAutorizacionModel);
                            }
                            return listPanelAutorizacionModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listPanelAutorizacionModel;
            }
        }

        public PanelAutorizacionModel RecuperarAutorizacionPersonal(string codAutorizacion, string codPersonal, string codEmpresa)
        {
            PanelAutorizacionModel oPanelAutorizacionModel = new PanelAutorizacionModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodAutorizacion", codAutorizacion);
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oPanelAutorizacionModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oPanelAutorizacionModel.CodAutorizacion = reader.IsDBNull(reader.GetOrdinal("CodAutorizacion")) ? "" : reader.GetString(reader.GetOrdinal("CodAutorizacion"));
                                oPanelAutorizacionModel.CodSolicitud = reader.IsDBNull(reader.GetOrdinal("CodSolicitud")) ? "" : reader.GetString(reader.GetOrdinal("CodSolicitud"));
                                oPanelAutorizacionModel.FechaSolicitud = reader.IsDBNull(reader.GetOrdinal("FechaSolicitud")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSolicitud"));
                                oPanelAutorizacionModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oPanelAutorizacionModel.NombreCompletoPersonal = reader.IsDBNull(reader.GetOrdinal("NombreCompletoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombreCompletoPersonal"));
                                oPanelAutorizacionModel.CodConcepto = reader.IsDBNull(reader.GetOrdinal("CodConcepto")) ? "" : reader.GetString(reader.GetOrdinal("CodConcepto"));
                                oPanelAutorizacionModel.NombreConcepto = reader.IsDBNull(reader.GetOrdinal("NombreConcepto")) ? "" : reader.GetString(reader.GetOrdinal("NombreConcepto"));
                                oPanelAutorizacionModel.FechaSalida = reader.IsDBNull(reader.GetOrdinal("FechaSalida")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaSalida"));
                                oPanelAutorizacionModel.FechaRetorno = reader.IsDBNull(reader.GetOrdinal("FechaRetorno")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaRetorno"));
                                oPanelAutorizacionModel.NumeroDias = reader.IsDBNull(reader.GetOrdinal("NumeroDias")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroDias"));
                                oPanelAutorizacionModel.NumeroHoras = reader.IsDBNull(reader.GetOrdinal("NumeroHoras")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroHoras"));
                                oPanelAutorizacionModel.NumeroMinutos = reader.IsDBNull(reader.GetOrdinal("NumeroMinutos")) ? 0 : reader.GetInt32(reader.GetOrdinal("NumeroMinutos"));
                                oPanelAutorizacionModel.TiempoCompleto = oPanelAutorizacionModel.NumeroDias.ToString() + " Dia(s), " + oPanelAutorizacionModel.NumeroHoras.ToString() + " Hora(s), " + oPanelAutorizacionModel.NumeroMinutos.ToString() + " Minuto(s)";
                                oPanelAutorizacionModel.NombreCompletoAutorizante = reader.IsDBNull(reader.GetOrdinal("NombreCompletoAutorizante")) ? "" : reader.GetString(reader.GetOrdinal("NombreCompletoAutorizante"));
                                oPanelAutorizacionModel.FechaAutorizacion = reader.IsDBNull(reader.GetOrdinal("FechaAutorizacion")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaAutorizacion"));
                                oPanelAutorizacionModel.EstadoAutorizacion = reader.IsDBNull(reader.GetOrdinal("EstadoAutorizacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("EstadoAutorizacion"));
                                oPanelAutorizacionModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                            }
                            return oPanelAutorizacionModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oPanelAutorizacionModel;
            }
        }
    }
}
