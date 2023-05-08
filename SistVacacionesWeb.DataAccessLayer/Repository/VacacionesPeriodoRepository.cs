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
    public class VacacionesPeriodoRepository : Repository, IVacacionesPeriodoRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;
        private readonly string _aplicarAumentoAuto;

        public VacacionesPeriodoRepository()
        {
            _listar = "spListarVacacionesPeriodo";
            _grabar = "spGrabarVacacionesPeriodo";
            _recuperar = "spRecuperarVacacionesPeriodo";
            _eliminar = "spEliminarVacacionesPeriodo";
            _aplicarAumentoAuto = "spAumentoDiasAdquiridosPeriodoAutomatico";
        }

        public List<VacacionesPeriodoModel> ListarVacacionesPeriodo(string codPersonal, string codEmpresa)
        {
            List<VacacionesPeriodoModel> listVacacionesPeriodoModel = new List<VacacionesPeriodoModel>();
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
                                VacacionesPeriodoModel oVacacionesPeriodoModel = new VacacionesPeriodoModel();
                                oVacacionesPeriodoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oVacacionesPeriodoModel.IdVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("IdVacacionesPeriodo")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdVacacionesPeriodo"));
                                oVacacionesPeriodoModel.CodVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesPeriodo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesPeriodo"));
                                oVacacionesPeriodoModel.FechaInicioPeriodo = reader.IsDBNull(reader.GetOrdinal("FechaInicioPeriodo")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaInicioPeriodo"));
                                oVacacionesPeriodoModel.FechaFinPeriodo = reader.IsDBNull(reader.GetOrdinal("FechaFinPeriodo")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaFinPeriodo"));
                                oVacacionesPeriodoModel.AplicarAumentoDiasAdquiridosAutomatico = reader.IsDBNull(reader.GetOrdinal("AplicarAumentoDiasAdquiridosAutomatico")) ? false : reader.GetBoolean(reader.GetOrdinal("AplicarAumentoDiasAdquiridosAutomatico"));
                                oVacacionesPeriodoModel.AplicarConsumoDiasAdquiridos = reader.IsDBNull(reader.GetOrdinal("AplicarConsumoDiasAdquiridos")) ? false : reader.GetBoolean(reader.GetOrdinal("AplicarConsumoDiasAdquiridos"));
                                oVacacionesPeriodoModel.DiasAdquiridos = reader.IsDBNull(reader.GetOrdinal("DiasAdquiridos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasAdquiridos"));
                                oVacacionesPeriodoModel.DiasConsumidos = reader.IsDBNull(reader.GetOrdinal("DiasConsumidos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasConsumidos"));
                                oVacacionesPeriodoModel.DiasPorConsumir = reader.IsDBNull(reader.GetOrdinal("DiasPorConsumir")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasPorConsumir"));
                                oVacacionesPeriodoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oVacacionesPeriodoModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oVacacionesPeriodoModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oVacacionesPeriodoModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oVacacionesPeriodoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oVacacionesPeriodoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listVacacionesPeriodoModel.Add(oVacacionesPeriodoModel);
                            }
                            return listVacacionesPeriodoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listVacacionesPeriodoModel;
            }
        }

        public int GrabarVacacionesPeriodo(VacacionesPeriodoModel oVacacionesPeriodoModel)
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
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", oVacacionesPeriodoModel.CodVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@FechaInicioPeriodo", oVacacionesPeriodoModel.FechaInicioPeriodo);
                        cmd.Parameters.AddWithValue("@FechaFinPeriodo", oVacacionesPeriodoModel.FechaFinPeriodo);
                        cmd.Parameters.AddWithValue("@AplicarAumentoDiasAdquiridosAutomatico", oVacacionesPeriodoModel.AplicarAumentoDiasAdquiridosAutomatico);
                        cmd.Parameters.AddWithValue("@AplicarConsumoDiasAdquiridos", oVacacionesPeriodoModel.AplicarConsumoDiasAdquiridos);
                        cmd.Parameters.AddWithValue("@DiasAdquiridos", oVacacionesPeriodoModel.DiasAdquiridos);
                        cmd.Parameters.AddWithValue("@DiasConsumidos", oVacacionesPeriodoModel.DiasConsumidos);
                        cmd.Parameters.AddWithValue("@DiasPorConsumir", oVacacionesPeriodoModel.DiasPorConsumir);
                        cmd.Parameters.AddWithValue("@Estado", oVacacionesPeriodoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodPersonal", oVacacionesPeriodoModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oVacacionesPeriodoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oVacacionesPeriodoModel.EstaBorrado);
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

        public VacacionesPeriodoModel RecuperarVacacionesPeriodo(string codVacacionesPeriodo, string codPersonal, string codEmpresa)
        {
            VacacionesPeriodoModel oVacacionesPeriodoModel = new VacacionesPeriodoModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", codVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oVacacionesPeriodoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oVacacionesPeriodoModel.IdVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("IdVacacionesPeriodo")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdVacacionesPeriodo"));
                                oVacacionesPeriodoModel.CodVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesPeriodo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesPeriodo"));
                                oVacacionesPeriodoModel.AplicarAumentoDiasAdquiridosAutomatico = reader.IsDBNull(reader.GetOrdinal("AplicarAumentoDiasAdquiridosAutomatico")) ? false : reader.GetBoolean(reader.GetOrdinal("AplicarAumentoDiasAdquiridosAutomatico"));
                                oVacacionesPeriodoModel.AplicarConsumoDiasAdquiridos = reader.IsDBNull(reader.GetOrdinal("AplicarConsumoDiasAdquiridos")) ? false : reader.GetBoolean(reader.GetOrdinal("AplicarConsumoDiasAdquiridos"));
                                oVacacionesPeriodoModel.FechaInicioPeriodo = reader.IsDBNull(reader.GetOrdinal("FechaInicioPeriodo")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaInicioPeriodo"));
                                oVacacionesPeriodoModel.FechaFinPeriodo = reader.IsDBNull(reader.GetOrdinal("FechaFinPeriodo")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaFinPeriodo"));
                                oVacacionesPeriodoModel.DiasAdquiridos = reader.IsDBNull(reader.GetOrdinal("DiasAdquiridos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasAdquiridos"));
                                oVacacionesPeriodoModel.DiasConsumidos = reader.IsDBNull(reader.GetOrdinal("DiasConsumidos")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasConsumidos"));
                                oVacacionesPeriodoModel.DiasPorConsumir = reader.IsDBNull(reader.GetOrdinal("DiasPorConsumir")) ? 0 : reader.GetDecimal(reader.GetOrdinal("DiasPorConsumir"));
                                oVacacionesPeriodoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oVacacionesPeriodoModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oVacacionesPeriodoModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oVacacionesPeriodoModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oVacacionesPeriodoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oVacacionesPeriodoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oVacacionesPeriodoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oVacacionesPeriodoModel;
            }
        }

        public int EliminarVacacionesPeriodoFisico(string codVacacionesPeriodo, string codPersonal, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", codVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
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

        public int EliminarVacacionesPeriodoLogico(VacacionesPeriodoModel oVacacionesPeriodoModel)
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
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", oVacacionesPeriodoModel.CodVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@FechaInicioPeriodo", oVacacionesPeriodoModel.FechaInicioPeriodo);
                        cmd.Parameters.AddWithValue("@FechaFinPeriodo", oVacacionesPeriodoModel.FechaFinPeriodo);
                        cmd.Parameters.AddWithValue("@AplicarAumentoDiasAdquiridosAutomatico", oVacacionesPeriodoModel.AplicarAumentoDiasAdquiridosAutomatico);
                        cmd.Parameters.AddWithValue("@AplicarConsumoDiasAdquiridos", oVacacionesPeriodoModel.AplicarConsumoDiasAdquiridos);
                        cmd.Parameters.AddWithValue("@DiasAdquiridos", oVacacionesPeriodoModel.DiasAdquiridos);
                        cmd.Parameters.AddWithValue("@DiasConsumidos", oVacacionesPeriodoModel.DiasConsumidos);
                        cmd.Parameters.AddWithValue("@DiasPorConsumir", oVacacionesPeriodoModel.DiasPorConsumir);
                        cmd.Parameters.AddWithValue("@Estado", oVacacionesPeriodoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodPersonal", oVacacionesPeriodoModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oVacacionesPeriodoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oVacacionesPeriodoModel.EstaBorrado);
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

        public int AplicarAumentoAutomatico(string codEmpresa)
        {
            int result = 0;
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_aplicarAumentoAuto, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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

        public void Dispose()
        {

        }
    }
}
