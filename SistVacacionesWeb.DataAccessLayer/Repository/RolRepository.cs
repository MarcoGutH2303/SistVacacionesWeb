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
    public class RolRepository : Repository, IRolRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public RolRepository()
        {
            _listar = "spListarRol";
            _grabar = "spGrabarRol";
            _recuperar = "spRecuperarRol";
            _eliminar = "spEliminarRol";
        }

        public List<RolModel> ListarRol(string codUsuario, string codEmpresa)
        {
            List<RolModel> listRolModel = new List<RolModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodUsuario", codUsuario);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RolModel oRolModel = new RolModel();
                                oRolModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oRolModel.IdRol = reader.IsDBNull(reader.GetOrdinal("IdRol")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdRol"));
                                oRolModel.CodUsuario = reader.IsDBNull(reader.GetOrdinal("CodUsuario")) ? "" : reader.GetString(reader.GetOrdinal("CodUsuario"));
                                oRolModel.Empresa = reader.IsDBNull(reader.GetOrdinal("Empresa")) ? 0 : reader.GetInt32(reader.GetOrdinal("Empresa"));
                                oRolModel.Mantenimiento = reader.IsDBNull(reader.GetOrdinal("Mantenimiento")) ? 0 : reader.GetInt32(reader.GetOrdinal("Mantenimiento"));
                                oRolModel.Concepto = reader.IsDBNull(reader.GetOrdinal("Concepto")) ? 0 : reader.GetInt32(reader.GetOrdinal("Concepto"));
                                oRolModel.Personal = reader.IsDBNull(reader.GetOrdinal("Personal")) ? 0 : reader.GetInt32(reader.GetOrdinal("Personal"));
                                oRolModel.Autorizacion = reader.IsDBNull(reader.GetOrdinal("Autorizacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("Autorizacion"));
                                oRolModel.Vacaciones = reader.IsDBNull(reader.GetOrdinal("Vacaciones")) ? 0 : reader.GetInt32(reader.GetOrdinal("Vacaciones"));
                                oRolModel.Reporte = reader.IsDBNull(reader.GetOrdinal("Reporte")) ? 0 : reader.GetInt32(reader.GetOrdinal("Reporte"));
                                oRolModel.Usuario = reader.IsDBNull(reader.GetOrdinal("Usuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("Usuario"));
                                oRolModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oRolModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                listRolModel.Add(oRolModel);
                            }
                            return listRolModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listRolModel;
            }
        }

        public int GrabarRol(RolModel oRolModel)
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
                        cmd.Parameters.AddWithValue("@IdRol", oRolModel.IdRol);
                        cmd.Parameters.AddWithValue("@CodUsuario", oRolModel.CodUsuario);
                        cmd.Parameters.AddWithValue("@Empresa", oRolModel.Empresa);
                        cmd.Parameters.AddWithValue("@Mantenimiento", oRolModel.Mantenimiento);
                        cmd.Parameters.AddWithValue("@Concepto", oRolModel.Concepto);
                        cmd.Parameters.AddWithValue("@Personal", oRolModel.Personal);
                        cmd.Parameters.AddWithValue("@Autorizacion", oRolModel.Autorizacion);
                        cmd.Parameters.AddWithValue("@Vacaciones", oRolModel.Vacaciones);
                        cmd.Parameters.AddWithValue("@Reporte", oRolModel.Reporte);
                        cmd.Parameters.AddWithValue("@Usuario", oRolModel.Usuario);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oRolModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oRolModel.EstaBorrado);
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

        public RolModel RecuperarRol(string codUsuario, string codEmpresa)
        {
            RolModel oRolModel = new RolModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodUsuario", codUsuario);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oRolModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oRolModel.IdRol = reader.IsDBNull(reader.GetOrdinal("IdRol")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdRol"));
                                oRolModel.CodUsuario = reader.IsDBNull(reader.GetOrdinal("CodUsuario")) ? "" : reader.GetString(reader.GetOrdinal("CodUsuario"));
                                oRolModel.Empresa = reader.IsDBNull(reader.GetOrdinal("Empresa")) ? 0 : reader.GetInt32(reader.GetOrdinal("Empresa"));
                                oRolModel.Mantenimiento = reader.IsDBNull(reader.GetOrdinal("Mantenimiento")) ? 0 : reader.GetInt32(reader.GetOrdinal("Mantenimiento"));
                                oRolModel.Concepto = reader.IsDBNull(reader.GetOrdinal("Concepto")) ? 0 : reader.GetInt32(reader.GetOrdinal("Concepto"));
                                oRolModel.Personal = reader.IsDBNull(reader.GetOrdinal("Personal")) ? 0 : reader.GetInt32(reader.GetOrdinal("Personal"));
                                oRolModel.Autorizacion = reader.IsDBNull(reader.GetOrdinal("Autorizacion")) ? 0 : reader.GetInt32(reader.GetOrdinal("Autorizacion"));
                                oRolModel.Vacaciones = reader.IsDBNull(reader.GetOrdinal("Vacaciones")) ? 0 : reader.GetInt32(reader.GetOrdinal("Vacaciones"));
                                oRolModel.Reporte = reader.IsDBNull(reader.GetOrdinal("Reporte")) ? 0 : reader.GetInt32(reader.GetOrdinal("Reporte"));
                                oRolModel.Usuario = reader.IsDBNull(reader.GetOrdinal("Usuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("Usuario"));
                                oRolModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oRolModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                            }
                            return oRolModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oRolModel;
            }
        }

        public int EliminarRolFisico(string codUsuario, string codEmpresa)
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
                        cmd.Parameters.AddWithValue("@CodUsuario", codUsuario);
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

        public int EliminarRolLogico(RolModel oRolModel)
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
                        cmd.Parameters.AddWithValue("@IdRol", oRolModel.IdRol);
                        cmd.Parameters.AddWithValue("@CodUsuario", oRolModel.CodUsuario);
                        cmd.Parameters.AddWithValue("@Empresa", oRolModel.Empresa);
                        cmd.Parameters.AddWithValue("@Mantenimiento", oRolModel.Mantenimiento);
                        cmd.Parameters.AddWithValue("@Concepto", oRolModel.Concepto);
                        cmd.Parameters.AddWithValue("@Personal", oRolModel.Personal);
                        cmd.Parameters.AddWithValue("@Autorizacion", oRolModel.Autorizacion);
                        cmd.Parameters.AddWithValue("@Vacaciones", oRolModel.Vacaciones);
                        cmd.Parameters.AddWithValue("@Reporte", oRolModel.Reporte);
                        cmd.Parameters.AddWithValue("@Usuario", oRolModel.Usuario);
                        cmd.Parameters.AddWithValue("@CodEmpresa", oRolModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oRolModel.EstaBorrado);
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
