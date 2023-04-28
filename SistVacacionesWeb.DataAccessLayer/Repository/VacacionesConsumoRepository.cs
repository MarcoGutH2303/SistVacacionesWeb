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
    public class VacacionesConsumoRepository : Repository, IVacacionesConsumoRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public VacacionesConsumoRepository()
        {
            _listar = "spListarVacacionesConsumo";
            _grabar = "spGrabarVacacionesConsumo";
            _recuperar = "spRecuperarVacacionesConsumo";
            _eliminar = "spEliminarVacacionesConsumo";
        }

        public List<VacacionesConsumoModel> ListarVacacionesConsumo(string codVacacionesPeriodo, string codPersonal, string codEmpresa)
        {
            List<VacacionesConsumoModel> listVacacionesConsumoModel = new List<VacacionesConsumoModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", codVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                VacacionesConsumoModel oVacacionesConsumoModel = new VacacionesConsumoModel();
                                oVacacionesConsumoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oVacacionesConsumoModel.IdVacacionesConsumo = reader.IsDBNull(reader.GetOrdinal("IdVacacionesConsumo")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdVacacionesConsumo"));
                                oVacacionesConsumoModel.CodVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesPeriodo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesPeriodo"));
                                oVacacionesConsumoModel.CodVacacionesConsumo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesConsumo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesConsumo"));
                                oVacacionesConsumoModel.FechaInicio = reader.IsDBNull(reader.GetOrdinal("FechaInicio")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaInicio"));
                                oVacacionesConsumoModel.FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaFin"));
                                oVacacionesConsumoModel.DiasUso = reader.IsDBNull(reader.GetOrdinal("DiasUso")) ? 0 : reader.GetInt32(reader.GetOrdinal("DiasUso"));
                                oVacacionesConsumoModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oVacacionesConsumoModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oVacacionesConsumoModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oVacacionesConsumoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oVacacionesConsumoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listVacacionesConsumoModel.Add(oVacacionesConsumoModel);
                            }
                            return listVacacionesConsumoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listVacacionesConsumoModel;
            }
        }

        public int GrabarVacacionesConsumo(VacacionesConsumoModel oVacacionesConsumoModel)
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
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", oVacacionesConsumoModel.CodVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@CodVacacionesConsumo", oVacacionesConsumoModel.CodVacacionesConsumo);
                        cmd.Parameters.AddWithValue("@FechaInicio", oVacacionesConsumoModel.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", oVacacionesConsumoModel.FechaFin);
                        cmd.Parameters.AddWithValue("@DiasUso", oVacacionesConsumoModel.DiasUso);
                        cmd.Parameters.AddWithValue("@CodPersonal", oVacacionesConsumoModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oVacacionesConsumoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oVacacionesConsumoModel.EstaBorrado);
                        result = cmd.ExecuteNonQuery();
                        return result > 0 ? 1 : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                string msj = ex.Message.ToString();
                return result;
            }
        }

        public VacacionesConsumoModel RecuperarVacacionesConsumo(string codVacacionesConsumo, string codVacacionesPeriodo, string codPersonal, string codEmpresa)
        {
            VacacionesConsumoModel oVacacionesConsumoModel = new VacacionesConsumoModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodVacacionesConsumo", codVacacionesConsumo);
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", codVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@CodPersonal", codPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oVacacionesConsumoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oVacacionesConsumoModel.IdVacacionesConsumo = reader.IsDBNull(reader.GetOrdinal("IdVacacionesConsumo")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdVacacionesConsumo"));
                                oVacacionesConsumoModel.CodVacacionesPeriodo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesPeriodo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesPeriodo"));
                                oVacacionesConsumoModel.CodVacacionesConsumo = reader.IsDBNull(reader.GetOrdinal("CodVacacionesConsumo")) ? "" : reader.GetString(reader.GetOrdinal("CodVacacionesConsumo"));
                                oVacacionesConsumoModel.FechaInicio = reader.IsDBNull(reader.GetOrdinal("FechaInicio")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaInicio"));
                                oVacacionesConsumoModel.FechaFin = reader.IsDBNull(reader.GetOrdinal("FechaFin")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("FechaFin"));
                                oVacacionesConsumoModel.DiasUso = reader.IsDBNull(reader.GetOrdinal("DiasUso")) ? 0 : reader.GetInt32(reader.GetOrdinal("DiasUso"));
                                oVacacionesConsumoModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oVacacionesConsumoModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oVacacionesConsumoModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oVacacionesConsumoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oVacacionesConsumoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oVacacionesConsumoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oVacacionesConsumoModel;
            }
        }

        public int EliminarVacacionesConsumoFisico(string codVacacionesConsumo, string codVacacionesPeriodo, string codPersonal, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodVacacionesConsumo", codVacacionesConsumo);
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

        public int EliminarVacacionesConsumoLogico(VacacionesConsumoModel oVacacionesConsumoModel)
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
                        cmd.Parameters.AddWithValue("@CodVacacionesPeriodo", oVacacionesConsumoModel.CodVacacionesPeriodo);
                        cmd.Parameters.AddWithValue("@CodVacacionesConsumo", oVacacionesConsumoModel.CodVacacionesConsumo);
                        cmd.Parameters.AddWithValue("@FechaInicio", oVacacionesConsumoModel.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", oVacacionesConsumoModel.FechaFin);
                        cmd.Parameters.AddWithValue("@DiasUso", oVacacionesConsumoModel.DiasUso);
                        cmd.Parameters.AddWithValue("@CodPersonal", oVacacionesConsumoModel.CodPersonal);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oVacacionesConsumoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oVacacionesConsumoModel.EstaBorrado);
                        result = cmd.ExecuteNonQuery();
                        return result > 0 ? 1 : 0;
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
