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
    public class CentroCostoRepository : Repository, ICentroCostoRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public CentroCostoRepository()
        {
            _listar = "spListarCentroCosto";
            _grabar = "spGrabarCentroCosto";
            _recuperar = "spRecuperarCentroCosto";
            _eliminar = "spEliminarCentroCosto";
        }

        public List<CentroCostoModel> ListarCentroCosto(string codEmpresa)
        {
            List<CentroCostoModel> listCentroCostoModel = new List<CentroCostoModel>();
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
                                CentroCostoModel oCentroCostoModel = new CentroCostoModel();
                                oCentroCostoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oCentroCostoModel.IdCentroCosto = reader.IsDBNull(reader.GetOrdinal("IdCentroCosto")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdCentroCosto"));
                                oCentroCostoModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oCentroCostoModel.CodigoAnterior = reader.IsDBNull(reader.GetOrdinal("CodigoAnterior")) ? "" : reader.GetString(reader.GetOrdinal("CodigoAnterior"));
                                oCentroCostoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oCentroCostoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oCentroCostoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oCentroCostoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listCentroCostoModel.Add(oCentroCostoModel);
                            }
                            return listCentroCostoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listCentroCostoModel;
            }
        }

        public int GrabarCentroCosto(CentroCostoModel oCentroCostoModel)
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
                        cmd.Parameters.AddWithValue("@CodCentroCosto", oCentroCostoModel.CodCentroCosto);
                        cmd.Parameters.AddWithValue("@CodigoAnterior", oCentroCostoModel.CodigoAnterior);
                        cmd.Parameters.AddWithValue("@Nombre", oCentroCostoModel.Nombre);
                        cmd.Parameters.AddWithValue("@Estado", oCentroCostoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oCentroCostoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oCentroCostoModel.EstaBorrado);
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

        public CentroCostoModel RecuperarCentroCosto(string codCentroCosto, string codEmpresa)
        {
            CentroCostoModel oCentroCostoModel = new CentroCostoModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodCentroCosto", codCentroCosto);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oCentroCostoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oCentroCostoModel.IdCentroCosto = reader.IsDBNull(reader.GetOrdinal("IdCentroCosto")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdCentroCosto"));
                                oCentroCostoModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oCentroCostoModel.CodigoAnterior = reader.IsDBNull(reader.GetOrdinal("CodigoAnterior")) ? "" : reader.GetString(reader.GetOrdinal("CodigoAnterior"));
                                oCentroCostoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oCentroCostoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oCentroCostoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oCentroCostoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oCentroCostoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oCentroCostoModel;
            }
        }

        public int EliminarCentroCostoFisico(string codCentroCosto, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodCentroCosto", codCentroCosto);
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

        public int EliminarCentroCostoLogico(CentroCostoModel oCentroCostoModel)
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
                        cmd.Parameters.AddWithValue("@CodCentroCosto", oCentroCostoModel.CodCentroCosto);
                        cmd.Parameters.AddWithValue("@CodigoAnterior", oCentroCostoModel.CodigoAnterior);
                        cmd.Parameters.AddWithValue("@Nombre", oCentroCostoModel.Nombre);
                        cmd.Parameters.AddWithValue("@Estado", oCentroCostoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oCentroCostoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oCentroCostoModel.EstaBorrado);
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
