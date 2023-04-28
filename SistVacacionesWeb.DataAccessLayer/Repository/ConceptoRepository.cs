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
    public class ConceptoRepository : Repository, IConceptoRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public ConceptoRepository()
        {
            _listar = "spListarConcepto";
            _grabar = "spGrabarConcepto";
            _recuperar = "spRecuperarConcepto";
            _eliminar = "spEliminarConcepto";
        }

        public List<ConceptoModel> ListarConcepto(string codEmpresa)
        {
            List<ConceptoModel> listConceptoModel = new List<ConceptoModel>();
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
                                ConceptoModel oConceptoModel = new ConceptoModel();
                                oConceptoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oConceptoModel.IdConcepto = reader.IsDBNull(reader.GetOrdinal("IdConcepto")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdConcepto"));
                                oConceptoModel.CodConcepto = reader.IsDBNull(reader.GetOrdinal("CodConcepto")) ? "" : reader.GetString(reader.GetOrdinal("CodConcepto"));
                                oConceptoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oConceptoModel.Recuperable = reader.IsDBNull(reader.GetOrdinal("Recuperable")) ? 0 : reader.GetInt32(reader.GetOrdinal("Recuperable"));
                                oConceptoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oConceptoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oConceptoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listConceptoModel.Add(oConceptoModel);
                            }
                            return listConceptoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listConceptoModel;
            }
        }

        public int GrabarConcepto(ConceptoModel oConceptoModel)
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
                        cmd.Parameters.AddWithValue("@CodConcepto", oConceptoModel.CodConcepto);
                        cmd.Parameters.AddWithValue("@Nombre", oConceptoModel.Nombre);
                        cmd.Parameters.AddWithValue("@Recuperable", oConceptoModel.Recuperable);
                        cmd.Parameters.AddWithValue("@Estado", oConceptoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oConceptoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oConceptoModel.EstaBorrado);
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

        public ConceptoModel RecuperarConcepto(string codConcepto, string codEmpresa)
        {
            ConceptoModel oConceptoModel = new ConceptoModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodConcepto", codConcepto);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oConceptoModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oConceptoModel.IdConcepto = reader.IsDBNull(reader.GetOrdinal("IdConcepto")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdConcepto"));
                                oConceptoModel.CodConcepto = reader.IsDBNull(reader.GetOrdinal("CodConcepto")) ? "" : reader.GetString(reader.GetOrdinal("CodConcepto"));
                                oConceptoModel.Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                oConceptoModel.Recuperable = reader.IsDBNull(reader.GetOrdinal("Recuperable")) ? 0 : reader.GetInt32(reader.GetOrdinal("Recuperable"));
                                oConceptoModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oConceptoModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oConceptoModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oConceptoModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oConceptoModel;
            }
        }

        public int EliminarConceptoFisico(string codConcepto, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodConcepto", codConcepto);
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

        public int EliminarConceptoLogico(ConceptoModel oConceptoModel)
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
                        cmd.Parameters.AddWithValue("@CodConcepto", oConceptoModel.CodConcepto);
                        cmd.Parameters.AddWithValue("@Nombre", oConceptoModel.Nombre);
                        cmd.Parameters.AddWithValue("@Recuperable", oConceptoModel.Recuperable);
                        cmd.Parameters.AddWithValue("@Estado", oConceptoModel.Estado);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oConceptoModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oConceptoModel.EstaBorrado);
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
