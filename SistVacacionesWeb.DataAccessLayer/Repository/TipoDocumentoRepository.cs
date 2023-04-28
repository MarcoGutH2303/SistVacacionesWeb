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
    public class TipoDocumentoRepository : Repository, ITipoDocumentoRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public TipoDocumentoRepository()
        {
            _listar = "spListarTipoDocumento";
            _grabar = "spGrabarTipoDocumento";
            _recuperar = "spRecuperarTipoDocumento";
            _eliminar = "spEliminarTipoDocumento";
        }

        public List<TipoDocumentoModel> ListarTipoDocumento(string codEmpresa)
        {
            List<TipoDocumentoModel> listTipoDocumentoModel = new List<TipoDocumentoModel>();
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
                                TipoDocumentoModel oTipoDocumentoModel = new TipoDocumentoModel();
                                oTipoDocumentoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oTipoDocumentoModel.IdTipoDocumento = reader.IsDBNull(reader.GetOrdinal("IdTipoDocumento")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdTipoDocumento"));
                                oTipoDocumentoModel.CodTipoDocumento = reader.IsDBNull(reader.GetOrdinal("CodTipoDocumento")) ? "" : reader.GetString(reader.GetOrdinal("CodTipoDocumento"));
                                oTipoDocumentoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oTipoDocumentoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oTipoDocumentoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oTipoDocumentoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listTipoDocumentoModel.Add(oTipoDocumentoModel);
                            }
                            return listTipoDocumentoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listTipoDocumentoModel;
            }
        }

        public int GrabarTipoDocumento(TipoDocumentoModel oTipoDocumentoModel)
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
                        cmd.Parameters.AddWithValue("@CodTipoDocumento", oTipoDocumentoModel.CodTipoDocumento);
                        cmd.Parameters.AddWithValue("@Nombre", oTipoDocumentoModel.Nombre);
                        cmd.Parameters.AddWithValue("@Estado", oTipoDocumentoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oTipoDocumentoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oTipoDocumentoModel.EstaBorrado);
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

        public TipoDocumentoModel RecuperarTipoDocumento(string codTipoDocumento, string codEmpresa)
        {
            TipoDocumentoModel oTipoDocumentoModel = new TipoDocumentoModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodTipoDocumento", codTipoDocumento);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oTipoDocumentoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oTipoDocumentoModel.IdTipoDocumento = reader.IsDBNull(reader.GetOrdinal("IdTipoDocumento")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdTipoDocumento"));
                                oTipoDocumentoModel.CodTipoDocumento = reader.IsDBNull(reader.GetOrdinal("CodTipoDocumento")) ? "" : reader.GetString(reader.GetOrdinal("CodTipoDocumento"));
                                oTipoDocumentoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oTipoDocumentoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oTipoDocumentoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oTipoDocumentoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oTipoDocumentoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oTipoDocumentoModel;
            }
        }

        public int EliminarTipoDocumentoFisico(string codTipoDocumento, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodTipoDocumento", codTipoDocumento);
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

        public int EliminarTipoDocumentoLogico(TipoDocumentoModel oTipoDocumentoModel)
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
                        cmd.Parameters.AddWithValue("@CodTipoDocumento", oTipoDocumentoModel.CodTipoDocumento);
                        cmd.Parameters.AddWithValue("@Nombre", oTipoDocumentoModel.Nombre);
                        cmd.Parameters.AddWithValue("@Estado", oTipoDocumentoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oTipoDocumentoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oTipoDocumentoModel.EstaBorrado);
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
