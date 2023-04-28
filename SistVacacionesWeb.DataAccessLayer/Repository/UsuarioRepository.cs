using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.DataAccessLayer.Repository
{
    public class UsuarioRepository : Repository, IUsuarioRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabarUsuario;
        private readonly string _grabarRol; 
        private readonly string _recuperar;
        private readonly string _eliminar;

        public UsuarioRepository()
        {
            _listar = "spListarUsuario";
            _grabarUsuario = "spGrabarUsuario";
            _grabarRol = "spGrabarRol";
            _recuperar = "spRecuperarUsuario";
            _eliminar = "spEliminarUsuario";
        }

        public List<UsuarioModel> ListarUsuario(string codEmpresa)
        {
            List<UsuarioModel> listUsuarioModel = new List<UsuarioModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Llave", llave);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UsuarioModel oUsuarioModel = new UsuarioModel();
                                oUsuarioModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oUsuarioModel.IdUsuario = reader.IsDBNull(reader.GetOrdinal("IdUsuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                                oUsuarioModel.CodUsuario = reader.IsDBNull(reader.GetOrdinal("CodUsuario")) ? "" : reader.GetString(reader.GetOrdinal("CodUsuario"));
                                oUsuarioModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oUsuarioModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oUsuarioModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oUsuarioModel.NombreCompletoPersonal = oUsuarioModel.NombrePersonal + " " + oUsuarioModel.ApellidoPersonal;
                                oUsuarioModel.NombreTipoDocumentoPersonal = reader.IsDBNull(reader.GetOrdinal("NombreTipoDocumentoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombreTipoDocumentoPersonal"));
                                oUsuarioModel.NumeroDocumentoPersonal = reader.IsDBNull(reader.GetOrdinal("NumeroDocumentoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("NumeroDocumentoPersonal"));
                                oUsuarioModel.Usuario = reader.IsDBNull(reader.GetOrdinal("Usuario")) ? "" : reader.GetString(reader.GetOrdinal("Usuario"));
                                oUsuarioModel.Pass = reader.IsDBNull(reader.GetOrdinal("Pass")) ? "" : Encoding.UTF8.GetString((byte[])reader.GetValue(reader.GetOrdinal("Pass")));
                                oUsuarioModel.Rol = reader.IsDBNull(reader.GetOrdinal("Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rol"));
                                oUsuarioModel.NombreFoto = reader.IsDBNull(reader.GetOrdinal("NombreFoto")) ? "" : reader.GetString(reader.GetOrdinal("NombreFoto"));
                                oUsuarioModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oUsuarioModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oUsuarioModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Foto")))
                                {
                                    string nomfoto = oUsuarioModel.NombreFoto;
                                    string extension = Path.GetExtension(nomfoto);
                                    string nombresinextension = extension.Substring(1);
                                    byte[] fotobyte = (byte[])reader.GetValue(reader.GetOrdinal("Foto"));
                                    string mime = "data:image/" + nombresinextension + ";base64,";
                                    string fotobase = Convert.ToBase64String(fotobyte);
                                    oUsuarioModel.Fotobase64 = mime + fotobase;
                                }
                                else
                                {
                                    oUsuarioModel.Fotobase64 = "";
                                }
                                listUsuarioModel.Add(oUsuarioModel);
                            }
                            return listUsuarioModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listUsuarioModel;
            }
        }

        public int GrabarUsuario(UsuarioModel oUsuarioModel, RolModel oRolModel)
        {
            int result = 0;
            string codUsuario = "";
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var tran = cn.BeginTransaction())
                    {
                        using (var cmd = new SqlCommand(_grabarUsuario, cn, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CodUsuario", oUsuarioModel.CodUsuario);
                            cmd.Parameters.AddWithValue("@CodPersonal", oUsuarioModel.CodPersonal);
                            cmd.Parameters.AddWithValue("@Usuario", oUsuarioModel.Usuario);
                            cmd.Parameters.AddWithValue("@Pass", oUsuarioModel.Pass);
                            cmd.Parameters.AddWithValue("@Llave", llave);
                            cmd.Parameters.AddWithValue("@Rol", oUsuarioModel.Rol);
                            cmd.Parameters.AddWithValue("@Foto", oUsuarioModel.Foto == null ? System.Data.SqlTypes.SqlBinary.Null : oUsuarioModel.Foto);
                            cmd.Parameters.AddWithValue("@NombreFoto", oUsuarioModel.NombreFoto == null ? "" : oUsuarioModel.NombreFoto);
                            cmd.Parameters.AddWithValue("@Estado", oUsuarioModel.Estado);
                            cmd.Parameters.AddWithValue("@CodEmpresa", oUsuarioModel.CodEmpresa);
                            cmd.Parameters.AddWithValue("@EstaBorrado", oUsuarioModel.EstaBorrado);
                            if (oUsuarioModel.CodUsuario == "")
                            {
                                codUsuario = Convert.ToString(cmd.ExecuteScalar());
                                result += 1;
                            }
                            else
                            {
                                result += cmd.ExecuteNonQuery();
                            }
                        }
                        using (var cmd = new SqlCommand(_grabarRol, cn, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdRol", codUsuario != "" ? oRolModel.IdRol : 1);
                            cmd.Parameters.AddWithValue("@CodUsuario", codUsuario != "" ? codUsuario : oRolModel.CodUsuario);
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
                            result += cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                        return result == 2 ? 1 : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                string msj = ex.Message.ToString();
                return result;
            }
        }

        public UsuarioModel RecuperarUsuario(string codUsuario, string codEmpresa)
        {
            UsuarioModel oUsuarioModel = new UsuarioModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodUsuario", codUsuario);
                        cmd.Parameters.AddWithValue("@Llave", llave);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oUsuarioModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oUsuarioModel.IdUsuario = reader.IsDBNull(reader.GetOrdinal("IdUsuario")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdUsuario"));
                                oUsuarioModel.CodUsuario = reader.IsDBNull(reader.GetOrdinal("CodUsuario")) ? "" : reader.GetString(reader.GetOrdinal("CodUsuario"));
                                oUsuarioModel.CodPersonal = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                oUsuarioModel.NombrePersonal = reader.IsDBNull(reader.GetOrdinal("NombrePersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombrePersonal"));
                                oUsuarioModel.ApellidoPersonal = reader.IsDBNull(reader.GetOrdinal("ApellidoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("ApellidoPersonal"));
                                oUsuarioModel.NombreCompletoPersonal = oUsuarioModel.NombrePersonal + " " + oUsuarioModel.ApellidoPersonal;
                                oUsuarioModel.NombreTipoDocumentoPersonal = reader.IsDBNull(reader.GetOrdinal("NombreTipoDocumentoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("NombreTipoDocumentoPersonal"));
                                oUsuarioModel.NumeroDocumentoPersonal = reader.IsDBNull(reader.GetOrdinal("NumeroDocumentoPersonal")) ? "" : reader.GetString(reader.GetOrdinal("NumeroDocumentoPersonal"));
                                oUsuarioModel.Usuario = reader.IsDBNull(reader.GetOrdinal("Usuario")) ? "" : reader.GetString(reader.GetOrdinal("Usuario"));
                                oUsuarioModel.Pass = reader.IsDBNull(reader.GetOrdinal("Pass")) ? "" : Encoding.UTF8.GetString((byte[])reader.GetValue(reader.GetOrdinal("Pass")));
                                oUsuarioModel.Rol = reader.IsDBNull(reader.GetOrdinal("Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rol"));
                                oUsuarioModel.NombreFoto = reader.IsDBNull(reader.GetOrdinal("NombreFoto")) ? "" : reader.GetString(reader.GetOrdinal("NombreFoto"));
                                oUsuarioModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oUsuarioModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oUsuarioModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Foto")))
                                {
                                    string nomfoto = oUsuarioModel.NombreFoto;
                                    string extension = Path.GetExtension(nomfoto);
                                    string nombresinextension = extension.Substring(1);
                                    byte[] fotobyte = (byte[])reader.GetValue(reader.GetOrdinal("Foto"));
                                    string mime = "data:image/" + nombresinextension + ";base64,";
                                    string fotobase = Convert.ToBase64String(fotobyte);
                                    oUsuarioModel.Fotobase64 = mime + fotobase;
                                }
                                else
                                {
                                    oUsuarioModel.Fotobase64 = "";
                                }
                            }
                            return oUsuarioModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oUsuarioModel;
            }
        }

        public int EliminarUsuarioFisico(string codUsuario, string codEmpresa)
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

        public int EliminarUsuarioLogico(UsuarioModel oUsuarioModel, RolModel oRolModel)
        {
            int result = 0;
            string codUsuario = "";
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var tran = cn.BeginTransaction())
                    {
                        using (var cmd = new SqlCommand(_grabarUsuario, cn, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CodUsuario", oUsuarioModel.CodUsuario);
                            cmd.Parameters.AddWithValue("@CodPersonal", oUsuarioModel.CodPersonal);
                            cmd.Parameters.AddWithValue("@Usuario", oUsuarioModel.Usuario);
                            cmd.Parameters.AddWithValue("@Pass", oUsuarioModel.Pass);
                            cmd.Parameters.AddWithValue("@Rol", oUsuarioModel.Rol);
                            cmd.Parameters.AddWithValue("@Foto", oUsuarioModel.Foto == null ? System.Data.SqlTypes.SqlBinary.Null : oUsuarioModel.Foto);
                            cmd.Parameters.AddWithValue("@NombreFoto", oUsuarioModel.NombreFoto == null ? "" : oUsuarioModel.NombreFoto);
                            cmd.Parameters.AddWithValue("@Estado", oUsuarioModel.Estado);
                            cmd.Parameters.AddWithValue("@CodEmpresa", oUsuarioModel.CodEmpresa);
                            cmd.Parameters.AddWithValue("@EstaBorrado", oUsuarioModel.EstaBorrado);
                            if (oUsuarioModel.CodUsuario == "")
                            {
                                codUsuario = Convert.ToString(cmd.ExecuteScalar());
                                result += 1;
                            }
                            else
                            {
                                result += cmd.ExecuteNonQuery();
                            }
                        }
                        using (var cmd = new SqlCommand(_grabarRol, cn, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@IdRol", codUsuario != "" ? oRolModel.IdRol : 1);
                            cmd.Parameters.AddWithValue("@CodUsuario", codUsuario != "" ? codUsuario : oRolModel.CodUsuario);
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
                            result += cmd.ExecuteNonQuery();
                        }
                        tran.Commit();
                        return result == 2 ? 1 : 0;
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
