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
    public class AreaRepository : Repository, IAreaRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public AreaRepository()
        {
            _listar = "spListarArea";
            _grabar = "spGrabarArea";
            _recuperar = "spRecuperarArea";
            _eliminar = "spEliminarArea";
        }

        public List<AreaModel> ListarArea(string codEmpresa)
        {
            List<AreaModel> listAreaModel = new List<AreaModel>();
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
                                AreaModel oAreaModel = new AreaModel();
                                oAreaModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oAreaModel.IdArea = reader.IsDBNull(reader.GetOrdinal("IdArea")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdArea"));
                                oAreaModel.CodArea = reader.IsDBNull(reader.GetOrdinal("CodArea")) ? "" : reader.GetString(reader.GetOrdinal("CodArea"));
                                oAreaModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oAreaModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oAreaModel.NombreCentroCosto = reader.IsDBNull(reader.GetOrdinal("NombreCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("NombreCentroCosto"));
                                oAreaModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oAreaModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oAreaModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listAreaModel.Add(oAreaModel);
                            }
                            return listAreaModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listAreaModel;
            }
        }

        public int GrabarArea(AreaModel oAreaModel)
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
                        cmd.Parameters.AddWithValue("@CodArea", oAreaModel.CodArea);
                        cmd.Parameters.AddWithValue("@Nombre", oAreaModel.Nombre);
                        cmd.Parameters.AddWithValue("@CodCentroCosto", oAreaModel.CodCentroCosto);
                        cmd.Parameters.AddWithValue("@Estado", oAreaModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oAreaModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oAreaModel.EstaBorrado);
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

        public AreaModel RecuperarArea(string codArea, string codEmpresa)
        {
            AreaModel oAreaModel = new AreaModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodArea", codArea);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oAreaModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oAreaModel.IdArea = reader.IsDBNull(reader.GetOrdinal("IdArea")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdArea"));
                                oAreaModel.CodArea = reader.IsDBNull(reader.GetOrdinal("CodArea")) ? "" : reader.GetString(reader.GetOrdinal("CodArea"));
                                oAreaModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oAreaModel.CodCentroCosto = reader.IsDBNull(reader.GetOrdinal("CodCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("CodCentroCosto"));
                                oAreaModel.NombreCentroCosto = reader.IsDBNull(reader.GetOrdinal("NombreCentroCosto")) ? "" : reader.GetString(reader.GetOrdinal("NombreCentroCosto"));
                                oAreaModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oAreaModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oAreaModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oAreaModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oAreaModel;
            }
        }

        public int EliminarAreaFisico(string codArea, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodArea", codArea);
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

        public int EliminarAreaLogico(AreaModel oAreaModel)
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
                        cmd.Parameters.AddWithValue("@CodArea", oAreaModel.CodArea);
                        cmd.Parameters.AddWithValue("@Nombre", oAreaModel.Nombre);
                        cmd.Parameters.AddWithValue("@CodCentroCosto", oAreaModel.CodCentroCosto);
                        cmd.Parameters.AddWithValue("@Estado", oAreaModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oAreaModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oAreaModel.EstaBorrado);
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
