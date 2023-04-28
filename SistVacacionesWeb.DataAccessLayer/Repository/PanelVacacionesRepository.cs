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
    public class PanelVacacionesRepository : Repository, IPanelVacacionesRepository
    {
        private readonly string _listarPeriodo;
        private readonly string _listarConsumo;

        public PanelVacacionesRepository()
        {
            _listarPeriodo = "spListarVacacionesPeriodo";
            _listarConsumo = "spListarVacacionesConsumo";
        }

        public List<PanelVacacionesPeriodoModel> ListarVacacionesPeriodo(string codPersonal, string codEmpresa)
        {
            List<PanelVacacionesPeriodoModel> listPanelVacacionesPeriodoModel = new List<PanelVacacionesPeriodoModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listarPeriodo, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PanelVacacionesPeriodoModel oPanelVacacionesPeriodoModel = new PanelVacacionesPeriodoModel();
                                oPanelVacacionesPeriodoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oPanelVacacionesPeriodoModel.CodVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesPeriodo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesPeriodo"));
                                oPanelVacacionesPeriodoModel.FechaInicioPeriodo = reader.IsDBNull(reader.GetOrdinal("FechaInicioPeriodo")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaInicioPeriodo"));
                                oPanelVacacionesPeriodoModel.FechaFinPeriodo = reader.IsDBNull(reader.GetOrdinal("FechaFinPeriodo")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaFinPeriodo"));
                                oPanelVacacionesPeriodoModel.DiasAdquiridos = reader.IsDBNull(reader.GetOrdinal("DiasAdquiridos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasAdquiridos"));
                                oPanelVacacionesPeriodoModel.DiasConsumidos = reader.IsDBNull(reader.GetOrdinal("DiasConsumidos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasConsumidos"));
                                oPanelVacacionesPeriodoModel.DiasPorConsumir = reader.IsDBNull(reader.GetOrdinal("DiasPorConsumir")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasPorConsumir"));
                                oPanelVacacionesPeriodoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oPanelVacacionesPeriodoModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oPanelVacacionesPeriodoModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oPanelVacacionesPeriodoModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oPanelVacacionesPeriodoModel.NombreCompletoPersonal = oPanelVacacionesPeriodoModel.NombrePersonal + " " + oPanelVacacionesPeriodoModel.ApellidoPersonal;
                                oPanelVacacionesPeriodoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                listPanelVacacionesPeriodoModel.Add(oPanelVacacionesPeriodoModel);
                            }
                            return listPanelVacacionesPeriodoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listPanelVacacionesPeriodoModel;
            }
        }

        public List<PanelVacacionesConsumoModel> ListarVacacionesConsumo(string codVacacionesPeriodo, string codPersonal, string codEmpresa)
        {
            List<PanelVacacionesConsumoModel> listPanelVacacionesConsumoModel = new List<PanelVacacionesConsumoModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listarConsumo, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", codVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PanelVacacionesConsumoModel oPanelVacacionesConsumoModel = new PanelVacacionesConsumoModel();
                                oPanelVacacionesConsumoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oPanelVacacionesConsumoModel.CodVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesPeriodo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesPeriodo"));
                                oPanelVacacionesConsumoModel.CodVacacionesConsumo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesConsumo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesConsumo"));
                                oPanelVacacionesConsumoModel.FechaInicio = reader.IsDBNull(reader.GetOrdinal("FechaInicio")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaInicio"));
                                oPanelVacacionesConsumoModel.FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaFin"));
                                oPanelVacacionesConsumoModel.DiasUso = reader.IsDBNull(reader.GetOrdinal("DiasUso")) ? 0 : reader.GetInt32(reader.GetOrdinal("DiasUso"));
                                oPanelVacacionesConsumoModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oPanelVacacionesConsumoModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oPanelVacacionesConsumoModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oPanelVacacionesConsumoModel.NombreCompletoPersonal = oPanelVacacionesConsumoModel.NombrePersonal + " " + oPanelVacacionesConsumoModel.ApellidoPersonal;
                                oPanelVacacionesConsumoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                listPanelVacacionesConsumoModel.Add(oPanelVacacionesConsumoModel);
                            }
                            return listPanelVacacionesConsumoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listPanelVacacionesConsumoModel;
            }
        }
    }
}
