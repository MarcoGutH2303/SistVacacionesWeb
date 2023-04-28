using MailKit.Net.Smtp;
using MimeKit;
using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.UILayer.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class UsuarioController : Controller
    {
        private IUsuarioRepository usuarioRepository = new UsuarioRepository();
        private IRolRepository rolRepository = new RolRepository();
        private IPersonalRepository personalRepository = new PersonalRepository();

        private static string codPersonal;
        private static string usuario;

        // GET: Usuario
        [Seguridad]
        public ActionResult Usuario()
        {
            return View();
        }

        public ActionResult _ListarUsuario()
        {
            return PartialView();
        }

        public ActionResult _ListarPersonal()
        {
            return PartialView();
        }

        public JsonResult ListarUsuario()
        {
            var listUsuario = usuarioRepository.ListarUsuario(CodEmpresa.Value);
            return Json(new { data = listUsuario }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarUsuario()
        {
            var listUsuario = usuarioRepository.ListarUsuario(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(listUsuario, JsonRequestBehavior.AllowGet);
        }

        public bool ValidarUsuario(int tipo, string codPersonal, string usuario)
        {
            var listUsuario = usuarioRepository.ListarUsuario(CodEmpresa.Value);
            if (tipo == 1)
            {
                return listUsuario.
                    Any(x => x.CodPersonal == codPersonal &&
                        x.CodEmpresa == CodEmpresa.Value);
            }
            else
            {
                return listUsuario.
                    Any(x => x.Usuario == usuario &&
                        x.CodEmpresa == CodEmpresa.Value);
            }
        }

        public int GrabarUsuario(UsuarioModel oUsuarioModel, RolModel oRolModel, HttpPostedFileBase foto_usuario)
        {
            oUsuarioModel.CodUsuario = oUsuarioModel.CodUsuario == null ? "" : oUsuarioModel.CodUsuario;
            oUsuarioModel.CodEmpresa = CodEmpresa.Value;
            oRolModel.Usuario = oRolModel.User;
            oRolModel.CodUsuario = oUsuarioModel.CodUsuario == null ? "" : oUsuarioModel.CodUsuario;
            oRolModel.CodEmpresa = CodEmpresa.Value;
            if (foto_usuario != null)
            {
                byte[] bufferLogo;
                string nombreLogo = foto_usuario.FileName;
                BinaryReader lector = new BinaryReader(foto_usuario.InputStream);
                bufferLogo = lector.ReadBytes((int)foto_usuario.ContentLength);
                oUsuarioModel.Foto = bufferLogo;
                oUsuarioModel.NombreFoto = nombreLogo;
            }
            bool siExiste;
            if (oUsuarioModel.CodUsuario == "")
            {
                siExiste = ValidarUsuario(1, oUsuarioModel.CodPersonal, "");
                if (siExiste == false)
                {
                    siExiste = ValidarUsuario(2, "", oUsuarioModel.Usuario);
                    if (siExiste == false)
                    {
                        return usuarioRepository.GrabarUsuario(oUsuarioModel, oRolModel);
                    }
                    else
                    {
                        return 3;
                    }
                }
                else
                {
                    return 2;
                }
            }
            else
            {
                if (oUsuarioModel.CodPersonal == codPersonal)
                {
                    if (oUsuarioModel.Usuario == usuario)
                    {
                        return usuarioRepository.GrabarUsuario(oUsuarioModel, oRolModel);
                    }
                    else
                    {
                        siExiste = ValidarUsuario(2, "", oUsuarioModel.Usuario);
                        if (siExiste == false)
                        {
                            return usuarioRepository.GrabarUsuario(oUsuarioModel, oRolModel);
                        }
                        else
                        {
                            return 3;
                        }
                    }
                }
                else
                {
                    siExiste = ValidarUsuario(1, oUsuarioModel.CodPersonal, "");
                    if (siExiste == false)
                    {
                        if (oUsuarioModel.Usuario != usuario)
                        {
                            siExiste = ValidarUsuario(2, "", oUsuarioModel.Usuario);
                            if (siExiste == false)
                            {
                                return usuarioRepository.GrabarUsuario(oUsuarioModel, oRolModel);
                            }
                            else
                            {
                                return 3;
                            }
                        }
                        else
                        {
                            return usuarioRepository.GrabarUsuario(oUsuarioModel, oRolModel);
                        }
                    }
                    else
                    {
                        return 2;
                    }
                }
            }
        }

        public JsonResult RecuperarUsuario(string codUsuario)
        {
            var oUsuarioModel = usuarioRepository.RecuperarUsuario(codUsuario, CodEmpresa.Value);
            codPersonal = oUsuarioModel.CodPersonal;
            usuario = oUsuarioModel.Usuario;
            return Json(oUsuarioModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RecuperarRol(string codUsuario)
        {
            var oRolModel = rolRepository.RecuperarRol(codUsuario, CodEmpresa.Value);
            oRolModel.User = oRolModel.Usuario;
            return Json(oRolModel, JsonRequestBehavior.AllowGet);
        }

        public int EliminarUsuarioLogico(string codUsuario)
        {
            var oUsuario = usuarioRepository.RecuperarUsuario(codUsuario, CodEmpresa.Value);
            var oRol = rolRepository.RecuperarRol(codUsuario, CodEmpresa.Value);
            oUsuario.EstaBorrado = true;
            oRol.EstaBorrado = true;
            return usuarioRepository.EliminarUsuarioLogico(oUsuario, oRol);
        }

        public int EliminarUsuarioFisico(string codUsuario)
        {
            return usuarioRepository.EliminarUsuarioFisico(codUsuario, CodEmpresa.Value);
        }

        public int EnviarCredencialesCorreo(string codUsuario)
        {
            try
            {
                var oUsuarioModel = usuarioRepository.RecuperarUsuario(codUsuario, CodEmpresa.Value);
                var oPersonalModel = personalRepository.RecuperarPersonal(oUsuarioModel.CodPersonal, CodEmpresa.Value);
                String servidor = "smtp.gmail.com";
                int puerto = 587;
                String userEmisor = "limpterz@gmail.com";
                String pass = "iswdxvgjbpvcdari";
                String userReceptor = oPersonalModel.CorreoElectronico;

                MimeMessage mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("Marco Gutierrez", userEmisor));
                mensaje.To.Add(new MailboxAddress(oPersonalModel.NombreCompleto, userReceptor));
                mensaje.Subject = "Credenciales - Sistema De Vacaciones";

                BodyBuilder cuerpo = new BodyBuilder();
                cuerpo.HtmlBody = $"<div><label>Buenos dias estimado{" " + oPersonalModel.NombreCompleto }. </label><br/><br/> <label> Sus credenciales de ingreso al Sistema De Vacaciones es: </label> <br/><br/> <label style='font-weight: bold'>Usuario: {oUsuarioModel.Usuario}</label> <br/> <label style='font-weight: bold'>Contraseña: {oUsuarioModel.Pass}</label><br/><br/><label>Saludos Cordiales.</label> </div>";

                mensaje.Body = cuerpo.ToMessageBody();

                SmtpClient clienteSmtp = new SmtpClient();
                clienteSmtp.CheckCertificateRevocation = false;
                clienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                clienteSmtp.Authenticate(userEmisor, pass);
                clienteSmtp.Send(mensaje);
                clienteSmtp.Disconnect(true);
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}