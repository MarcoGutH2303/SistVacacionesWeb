using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.Domain.Models;
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
    public class EmpresaRepository : Repository, IEmpresaRepository, IDisposable
    {
        private readonly string _listar;
        private readonly string _grabar;
        private readonly string _grabarUsuario;
        private readonly string _grabarRol;
        private readonly string _recuperar;
        private readonly string _eliminar;

        public EmpresaRepository()
        {
            _listar = "spListarEmpresa";
            _grabar = "spGrabarEmpresa";
            _grabarUsuario = "spGrabarUsuario";
            _grabarRol = "spGrabarRol";
            _recuperar = "spRecuperarEmpresa";
            _eliminar = "spEliminarEmpresa";
        }

        public List<EmpresaModel> ListarEmpresa()
        {
            List<EmpresaModel> listEmpresaModel = new List<EmpresaModel>();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_listar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmpresaModel oEmpresaModel = new EmpresaModel();
                                oEmpresaModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oEmpresaModel.IdEmpresa = reader.IsDBNull(reader.GetOrdinal("IdEmpresa")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdEmpresa"));
                                oEmpresaModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oEmpresaModel.RazonSocial = reader.IsDBNull(reader.GetOrdinal("RazonSocial")) ? "" : reader.GetString(reader.GetOrdinal("RazonSocial"));
                                oEmpresaModel.Ruc = reader.IsDBNull(reader.GetOrdinal("Ruc")) ? "" : reader.GetString(reader.GetOrdinal("Ruc"));
                                oEmpresaModel.DomicilioFiscal = reader.IsDBNull(reader.GetOrdinal("DomicilioFiscal")) ? "" : reader.GetString(reader.GetOrdinal("DomicilioFiscal"));
                                oEmpresaModel.Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString(reader.GetOrdinal("Telefono"));
                                oEmpresaModel.CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? "" : reader.GetString(reader.GetOrdinal("CorreoElectronico"));
                                oEmpresaModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oEmpresaModel.NombreLogo = reader.IsDBNull(reader.GetOrdinal("NombreLogo")) ? "" : reader.GetString(reader.GetOrdinal("NombreLogo"));
                                oEmpresaModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Logo")))
                                {
                                    string nomfoto = oEmpresaModel.NombreLogo;
                                    string extension = Path.GetExtension(nomfoto);
                                    string nombresinextension = extension.Substring(1);
                                    byte[] fotobyte = (byte[])reader.GetValue(reader.GetOrdinal("Logo"));
                                    string mime = "data:image/" + nombresinextension + ";base64,";
                                    string fotobase = Convert.ToBase64String(fotobyte);
                                    oEmpresaModel.Fotobase64 = mime + fotobase;
                                }
                                else
                                {
                                    oEmpresaModel.Fotobase64 = "";
                                }
                                listEmpresaModel.Add(oEmpresaModel);
                            }
                            return listEmpresaModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return listEmpresaModel;
            }   
        }

        public int GrabarEmpresa(EmpresaModel oEmpresaModel, UsuarioModel oUsuarioModel, RolModel oRolModel)
        {
            int result = 0;
            string CodEmpresa = "";
            string CodUsuario = "";
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var tran = cn.BeginTransaction())
                    {
                        using (var cmd = new SqlCommand(_grabar, cn, tran))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@CodEmpresa", oEmpresaModel.CodEmpresa);
                            cmd.Parameters.AddWithValue("@RazonSocial", oEmpresaModel.RazonSocial);
                            cmd.Parameters.AddWithValue("@Ruc", oEmpresaModel.Ruc);
                            cmd.Parameters.AddWithValue("@DomicilioFiscal", oEmpresaModel.DomicilioFiscal == null ? "" : oEmpresaModel.DomicilioFiscal);
                            cmd.Parameters.AddWithValue("@Telefono", oEmpresaModel.Telefono == null ? "" : oEmpresaModel.Telefono);
                            cmd.Parameters.AddWithValue("@CorreoElectronico", oEmpresaModel.CorreoElectronico == null ? "" : oEmpresaModel.CorreoElectronico);
                            cmd.Parameters.AddWithValue("@Estado", oEmpresaModel.Estado);
                            cmd.Parameters.AddWithValue("@Logo", oEmpresaModel.Logo == null ? System.Data.SqlTypes.SqlBinary.Null : oEmpresaModel.Logo);
                            cmd.Parameters.AddWithValue("@NombreLogo", oEmpresaModel.NombreLogo == null ? "" : oEmpresaModel.NombreLogo);
                            cmd.Parameters.AddWithValue("@EstaBorrado", oEmpresaModel.EstaBorrado);
                            if (oEmpresaModel.CodEmpresa == "")
                            {
                                CodEmpresa = Convert.ToString(cmd.ExecuteScalar());
                                result += 1;
                            }
                            else
                            {
                                result += cmd.ExecuteNonQuery();
                            }
                        }
                        if (oEmpresaModel.CodEmpresa == "")
                        {
                            using (var cmd = new SqlCommand(_grabarUsuario, cn, tran))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@CodUsuario", oUsuarioModel.CodUsuario);
                                cmd.Parameters.AddWithValue("@CodPersonal", oUsuarioModel.CodPersonal);
                                cmd.Parameters.AddWithValue("@Usuario", oUsuarioModel.Usuario);
                                cmd.Parameters.AddWithValue("@Pass", oUsuarioModel.Pass);
                                cmd.Parameters.AddWithValue("@Rol", oUsuarioModel.Rol);
                                cmd.Parameters.AddWithValue("@Estado", oUsuarioModel.Estado);
                                cmd.Parameters.AddWithValue("@Foto", oUsuarioModel.Foto == null ? System.Data.SqlTypes.SqlBinary.Null : oUsuarioModel.Foto);
                                cmd.Parameters.AddWithValue("@NombreFoto", oUsuarioModel.NombreFoto == null ? "" : oUsuarioModel.NombreFoto);
                                cmd.Parameters.AddWithValue("@CodEmpresa", CodEmpresa);
                                cmd.Parameters.AddWithValue("@EstaBorrado", oUsuarioModel.EstaBorrado);
                                if (oUsuarioModel.CodUsuario == "")
                                {
                                    CodUsuario = Convert.ToString(cmd.ExecuteScalar());
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
                                cmd.Parameters.AddWithValue("@IdRol", oRolModel.IdRol);
                                cmd.Parameters.AddWithValue("@CodUsuario", CodUsuario != "" ? CodUsuario : oRolModel.CodUsuario);
                                cmd.Parameters.AddWithValue("@Empresa", oRolModel.Empresa);
                                cmd.Parameters.AddWithValue("@Mantenimiento", oRolModel.Mantenimiento);
                                cmd.Parameters.AddWithValue("@Concepto", oRolModel.Concepto);
                                cmd.Parameters.AddWithValue("@Personal", oRolModel.Personal);
                                cmd.Parameters.AddWithValue("@Autorizacion", oRolModel.Autorizacion);
                                cmd.Parameters.AddWithValue("@Vacaciones", oRolModel.Vacaciones);
                                cmd.Parameters.AddWithValue("@Reporte", oRolModel.Reporte);
                                cmd.Parameters.AddWithValue("@Usuario", oRolModel.Usuario);
                                cmd.Parameters.AddWithValue("@CodEmpresa", CodEmpresa);
                                cmd.Parameters.AddWithValue("@EstaBorrado", oRolModel.EstaBorrado);
                                result += cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
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

        public EmpresaModel RecuperarEmpresa(string codEmpresa)
        {
            EmpresaModel oEmpresaModel = new EmpresaModel();
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_recuperar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                oEmpresaModel.Correlativo = reader.IsDBNull(reader.GetOrdinal("Correlativo")) ? 0 : Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Correlativo")));
                                oEmpresaModel.IdEmpresa = reader.IsDBNull(reader.GetOrdinal("IdEmpresa")) ? 0 : reader.GetInt32(reader.GetOrdinal("IdEmpresa"));
                                oEmpresaModel.CodEmpresa = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                oEmpresaModel.RazonSocial = reader.IsDBNull(reader.GetOrdinal("RazonSocial")) ? "" : reader.GetString(reader.GetOrdinal("RazonSocial"));
                                oEmpresaModel.Ruc = reader.IsDBNull(reader.GetOrdinal("Ruc")) ? "" : reader.GetString(reader.GetOrdinal("Ruc"));
                                oEmpresaModel.DomicilioFiscal = reader.IsDBNull(reader.GetOrdinal("DomicilioFiscal")) ? "" : reader.GetString(reader.GetOrdinal("DomicilioFiscal"));
                                oEmpresaModel.Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? "" : reader.GetString(reader.GetOrdinal("Telefono"));
                                oEmpresaModel.CorreoElectronico = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? "" : reader.GetString(reader.GetOrdinal("CorreoElectronico"));
                                oEmpresaModel.Estado = reader.IsDBNull(reader.GetOrdinal("Estado")) ? 0 : reader.GetInt32(reader.GetOrdinal("Estado"));
                                oEmpresaModel.NombreLogo = reader.IsDBNull(reader.GetOrdinal("NombreLogo")) ? "" : reader.GetString(reader.GetOrdinal("NombreLogo"));
                                oEmpresaModel.EstaBorrado = reader.IsDBNull(reader.GetOrdinal("EstaBorrado")) ? false : reader.GetBoolean(reader.GetOrdinal("EstaBorrado"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Logo")))
                                {
                                    string nomfoto = oEmpresaModel.NombreLogo;
                                    string extension = Path.GetExtension(nomfoto);
                                    string nombresinextension = extension.Substring(1);
                                    byte[] fotobyte = (byte[])reader.GetValue(reader.GetOrdinal("Logo"));
                                    string mime = "data:image/" + nombresinextension + ";base64,";
                                    string fotobase = Convert.ToBase64String(fotobyte);
                                    oEmpresaModel.Fotobase64 = mime + fotobase;
                                }
                                else
                                {
                                    oEmpresaModel.Fotobase64 = "";
                                }
                            }
                            return oEmpresaModel;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return oEmpresaModel;
            }
        }

        public int EliminarEmpresaFisico(string codEmpresa)
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

        public int EliminarEmpresaLogico(EmpresaModel oEmpresaModel)
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
                        cmd.Parameters.AddWithValue("@CodEmpresa", oEmpresaModel.CodEmpresa);
                        cmd.Parameters.AddWithValue("@RazonSocial", oEmpresaModel.RazonSocial);
                        cmd.Parameters.AddWithValue("@Ruc", oEmpresaModel.Ruc);
                        cmd.Parameters.AddWithValue("@DomicilioFiscal", oEmpresaModel.DomicilioFiscal == null ? "" : oEmpresaModel.DomicilioFiscal);
                        cmd.Parameters.AddWithValue("@Telefono", oEmpresaModel.Telefono == null ? "" : oEmpresaModel.Telefono);
                        cmd.Parameters.AddWithValue("@CorreoElectronico", oEmpresaModel.CorreoElectronico == null ? "" : oEmpresaModel.CorreoElectronico);
                        cmd.Parameters.AddWithValue("@Estado", oEmpresaModel.Estado);
                        cmd.Parameters.AddWithValue("@Logo", oEmpresaModel.Logo == null ? System.Data.SqlTypes.SqlBinary.Null : oEmpresaModel.Logo);
                        cmd.Parameters.AddWithValue("@NombreLogo", oEmpresaModel.NombreLogo == null ? "" : oEmpresaModel.NombreLogo);
                        cmd.Parameters.AddWithValue("@EstaBorrado", oEmpresaModel.EstaBorrado);
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
