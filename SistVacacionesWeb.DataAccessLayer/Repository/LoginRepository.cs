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
    public class LoginRepository : Repository, ILoginRepository
    {
        private readonly string _validar;
        private readonly string _recuperar;

        public LoginRepository()
        {
            _validar = "spValidarLogin";
            _recuperar = "spRecuperarLogin";
        }

        public List<LoginModel> ListaMenuCabecerasAdministrador()
        {
            List<LoginModel> Cabeceras = new List<LoginModel>()
            {
                new LoginModel() { Correlativo = 1, ActionName = "Administrador", ControllerName = "PanelControl", Icon = "fas fa-tachometer-alt", Nombre = "Panel De Control" },
                new LoginModel() { Correlativo = 2, ActionName = "Empresa", ControllerName = "Empresa", Icon = "fas fa-building", Nombre = "Empresa" },
                new LoginModel() { Correlativo = 3, ActionName = "Mantenimiento", ControllerName = "Mantenimiento", Icon = "fas fa-cogs", Nombre = "Mantenimiento" },
                new LoginModel() { Correlativo = 4, ActionName = "Personal", ControllerName = "Personal", Icon = "fas fa-user-friends", Nombre = "Personal" },
                new LoginModel() { Correlativo = 5, ActionName = "Concepto", ControllerName = "Concepto", Icon = "fas fa-th-list", Nombre = "Concepto" },
                new LoginModel() { Correlativo = 6, ActionName = "Autorizacion", ControllerName = "Autorizacion", Icon = "fas fa-tasks", Nombre = "Autorización" },
                new LoginModel() { Correlativo = 7, ActionName = "Vacaciones", ControllerName = "Vacaciones", Icon = "fas fa-calendar-alt", Nombre = "Vacaciones" },
                new LoginModel() { Correlativo = 8, ActionName = "Reporte", ControllerName = "Reporte", Icon = "fas fa-file-pdf", Nombre = "Reporte" },
                new LoginModel() { Correlativo = 9, ActionName = "Usuario", ControllerName = "Usuario", Icon = "fas fa-users", Nombre = "Usuario" },
            };
            return Cabeceras;
        }
        public List<LoginModel> ListaMenuCabecerasEmpleado()
        {
            List<LoginModel> Cabeceras = new List<LoginModel>()
            {
                new LoginModel() { Correlativo = 1, ActionName = "Empleado", ControllerName = "PanelControl", Icon = "fas fa-tachometer-alt", Nombre = "Panel De Control" },
                new LoginModel() { Correlativo = 2, ActionName = "Solicitud", ControllerName = "Solicitud", Icon = "far fa-file-alt", Nombre = "Solicitud"},
                new LoginModel() { Correlativo = 3, ActionName = "PanelAutorizacion", ControllerName = "PanelAutorizacion", Icon = "fas fa-tasks", Nombre = "Autorizaciones" },
                new LoginModel() { Correlativo = 4, ActionName = "PanelVacaciones", ControllerName = "PanelVacaciones", Icon = "fas fa-calendar-alt", Nombre = "Vacaciones" },
            };
            return Cabeceras;
        }
        public List<LoginModel> ListaMenuHijosAdministrador()
        {
            List<LoginModel> Hijos = new List<LoginModel>()
            {
                new LoginModel() { Correlativo = 2, ActionName = "TipoDocumento", ControllerName = "TipoDocumento", Icon = "", Nombre = "Tipo Documento" },
                new LoginModel() { Correlativo = 2, ActionName = "CentroCosto", ControllerName = "CentroCosto", Icon = "", Nombre = "Centro De Costo" },
                new LoginModel() { Correlativo = 2, ActionName = "Area", ControllerName = "Area", Icon = "", Nombre = "Área" },
                new LoginModel() { Correlativo = 2, ActionName = "Cargo", ControllerName = "Cargo", Icon = "", Nombre = "Cargo" },
                new LoginModel() { Correlativo = 8, ActionName = "ReporteSolicitud", ControllerName = "Reporte", Icon = "", Nombre = "Solicitud" },
                new LoginModel() { Correlativo = 8, ActionName = "ReporteAutorizacion", ControllerName = "Reporte", Icon = "", Nombre = "Autorización" },
                new LoginModel() { Correlativo = 8, ActionName = "ReporteVacaciones", ControllerName = "Reporte", Icon = "", Nombre = "Vacaciones" },
            };
            return Hijos;
        }

        public string ValidarLogin(string usuario, string pass, string codEmpresa)
        {
            string result = "";
            try
            {
                using (var cn = GetSqlConnection())
                {
                    cn.Open();
                    using (var cmd = new SqlCommand(_validar, cn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Usuario", usuario);
                        cmd.Parameters.AddWithValue("@Pass", pass);
                        cmd.Parameters.AddWithValue("@Llave", llave);
                        cmd.Parameters.AddWithValue("@CodEmpresa", codEmpresa);
                        result = Convert.ToString(cmd.ExecuteScalar());
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }

        public int RecuperarLogin(string codUsuario, string codEmpresa)
        {
            int result = 0;
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
                                CodUsuario.Value = reader.IsDBNull(reader.GetOrdinal("CodUsuario")) ? "" : reader.GetString(reader.GetOrdinal("CodUsuario"));
                                CodPersonal.Value = reader.IsDBNull(reader.GetOrdinal("CodPersonal")) ? "" : reader.GetString(reader.GetOrdinal("CodPersonal"));
                                Nombre.Value = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? "" : reader.GetString(reader.GetOrdinal("Nombre"));
                                Apellido.Value = reader.IsDBNull(reader.GetOrdinal("Apellido")) ? "" : reader.GetString(reader.GetOrdinal("Apellido"));
                                Usuario.Value = reader.IsDBNull(reader.GetOrdinal("Usuario")) ? "" : reader.GetString(reader.GetOrdinal("Usuario"));
                                Pass.Value = reader.IsDBNull(reader.GetOrdinal("Pass")) ? "" : Encoding.UTF8.GetString((byte[])reader.GetValue(reader.GetOrdinal("Pass")));
                                Rol.Value = reader.IsDBNull(reader.GetOrdinal("Rol")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rol"));
                                string nombreFoto = reader.IsDBNull(reader.GetOrdinal("NombreFoto")) ? "" : reader.GetString(reader.GetOrdinal("NombreFoto"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Foto")))
                                {
                                    string nomfoto = nombreFoto;
                                    string extension = Path.GetExtension(nomfoto);
                                    string nombresinextension = extension.Substring(1);
                                    byte[] fotobyte = (byte[])reader.GetValue(reader.GetOrdinal("Foto"));
                                    string mime = "data:image/" + nombresinextension + ";base64,";
                                    string fotobase = Convert.ToBase64String(fotobyte);
                                    FotoFotobase64.Value = mime + fotobase;
                                }
                                else
                                {
                                    FotoFotobase64.Value = "";
                                }
                                CodEmpresa.Value = reader.IsDBNull(reader.GetOrdinal("CodEmpresa")) ? "" : reader.GetString(reader.GetOrdinal("CodEmpresa"));
                                RazonSocial.Value = reader.IsDBNull(reader.GetOrdinal("RazonSocial")) ? "" : reader.GetString(reader.GetOrdinal("RazonSocial"));
                                CorreoElectronico.Value = reader.IsDBNull(reader.GetOrdinal("CorreoElectronico")) ? "" : reader.GetString(reader.GetOrdinal("CorreoElectronico"));
                            }
                            if (CodUsuario.Value != "")
                                result = 1;
                            else
                                result = 0;
                            return result;
                        }
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }
        }
    }
}
