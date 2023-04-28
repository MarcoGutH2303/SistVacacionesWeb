using MailKit.Net.Smtp;
using MimeKit;
using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.UILayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class LoginController : Controller
    {
        private ILoginRepository loginRepository = new LoginRepository();
        private IUsuarioRepository usuarioRepository = new UsuarioRepository();
        private IRolRepository rolRepository = new RolRepository();
        private IPersonalRepository personalRepository = new PersonalRepository();

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login");
        }

        public JsonResult ValidarDatosUsuario(string usuario, string pass, string codEmpresa)
        {
            if (codEmpresa == "") { codEmpresa = CodEmpresa.Value; }
            if (usuario == "") { usuario = Usuario.Value; }
            if (pass == "") { pass = Pass.Value; }
            string codUsuario = loginRepository.ValidarLogin(usuario, pass, codEmpresa);
            loginRepository.RecuperarLogin(codUsuario.Trim(), codEmpresa);
            if (FotoFotobase64.Value == "")
            {
                string path = Server.MapPath("/Recursos/undraw_rocket.svg");
                byte[] fotobyte = System.IO.File.ReadAllBytes(path);
                string mime = "data:image/svg+xml;base64,";
                string fotobase = Convert.ToBase64String(fotobyte);
                FotoFotobase64.Value = mime + fotobase;
            }
            return Json(usuarioRepository.RecuperarUsuario(codUsuario.Trim(), codEmpresa), JsonRequestBehavior.AllowGet);
        }

        public int CargarSesion(string usuario, string pass, string codEmpresa, string rolUser, string codUsuario)
        {
            if (codEmpresa == "") { codEmpresa = CodEmpresa.Value; }
            if (usuario == "") { usuario = Usuario.Value; }
            if (pass == "") { pass = Pass.Value; }
            var oUsuarioModel = usuarioRepository.RecuperarUsuario(codUsuario, codEmpresa);
            if (Rol.Value == 2)
            {
                if (rolUser == "Administrador")
                {
                    if (oUsuarioModel.CodUsuario.Trim() != "")
                    {
                        Session["usuario"] = oUsuarioModel;
                        var listaCab = loginRepository.ListaMenuCabecerasAdministrador();
                        var listaHij = loginRepository.ListaMenuHijosAdministrador();
                        var listaRol = rolRepository.RecuperarRol(oUsuarioModel.CodUsuario, codEmpresa);
                        List<LoginModel> listaCabecera = new List<LoginModel>();
                        List<LoginModel> listaMenu = new List<LoginModel>();
                        foreach (var item in listaCab.ToList())
                        {
                            if (item.Nombre == "Panel De Control") { listaCabecera.Add(item); listaMenu.Add(item); }
                            if (item.Nombre == "Empresa") { if (listaRol.Empresa == 1) { listaCabecera.Add(item); listaMenu.Add(item); } }
                            if (item.Nombre == "Mantenimiento") { if (listaRol.Mantenimiento == 1) { listaCabecera.Add(item); } }
                            if (item.Nombre == "Personal") { if (listaRol.Personal == 1) { listaCabecera.Add(item); listaMenu.Add(item); } }
                            if (item.Nombre == "Concepto") { if (listaRol.Concepto == 1) { listaCabecera.Add(item); listaMenu.Add(item); } }
                            if (item.Nombre == "Autorización") { if (listaRol.Autorizacion == 1) { listaCabecera.Add(item); listaMenu.Add(item); } }
                            if (item.Nombre == "Vacaciones") { if (listaRol.Vacaciones == 1) { listaCabecera.Add(item); listaMenu.Add(item); } }
                            if (item.Nombre == "Reporte") { if (listaRol.Reporte == 1) { listaCabecera.Add(item); } }
                            if (item.Nombre == "Usuario") { if (listaRol.Usuario == 1) { listaCabecera.Add(item); listaMenu.Add(item); } }
                        }
                        foreach (var item in listaHij.ToList())
                        {
                            if (listaRol.Mantenimiento == 1)
                            {
                                if (item.Nombre == "Tipo Documento") { listaMenu.Add(item); }
                                if (item.Nombre == "Centro De Costo") { listaMenu.Add(item); }
                                if (item.Nombre == "Área") { listaMenu.Add(item); }
                                if (item.Nombre == "Cargo") { listaMenu.Add(item); }
                            }
                            if (listaRol.Reporte == 1)
                            {
                                if (item.Nombre == "Solicitud") { listaMenu.Add(item); }
                                if (item.Nombre == "Autorización") { listaMenu.Add(item); }
                                if (item.Nombre == "Vacaciones") { listaMenu.Add(item); }
                            }
                        }
                        Session["menu"] = listaMenu;
                        Session["cabecera"] = listaCabecera;
                        return 1;
                    }
                    else
                    {
                        Session["usuario"] = null;
                        Session["menu"] = null;
                        Session["cabecera"] = null;
                        return 0;
                    }
                }
                else if (rolUser == "Empleado")
                {
                    if (oUsuarioModel.CodUsuario.Trim() != "")
                    {
                        Session["usuario"] = oUsuarioModel;
                        var listaCab = loginRepository.ListaMenuCabecerasEmpleado();
                        List<LoginModel> listaCabecera = new List<LoginModel>();
                        List<LoginModel> listaMenu = new List<LoginModel>();
                        foreach (var item in listaCab.ToList())
                        {
                            if (item.Nombre == "Panel De Control") { listaCabecera.Add(item); listaMenu.Add(item); }
                            if (item.Nombre == "Solicitud") { listaCabecera.Add(item); listaMenu.Add(item); }
                            if (item.Nombre == "Autorizaciones") { listaCabecera.Add(item); listaMenu.Add(item); }
                            if (item.Nombre == "Vacaciones") { listaCabecera.Add(item); listaMenu.Add(item); }
                        }
                        Session["menu"] = listaMenu;
                        Session["cabecera"] = listaCabecera;
                        return 1;
                    }
                    else
                    {
                        Session["usuario"] = null;
                        Session["menu"] = null;
                        Session["cabecera"] = null;
                        return 0;
                    }
                }
                else
                {
                    Session["usuario"] = null;
                    Session["menu"] = null;
                    Session["cabecera"] = null;
                    return 0;
                }
            }
            else if (Rol.Value == 1)
            {
                if (rolUser == "Empleado")
                {
                    if (oUsuarioModel.CodUsuario.Trim() != "")
                    {
                        Session["usuario"] = oUsuarioModel;
                        var listaCab = loginRepository.ListaMenuCabecerasEmpleado();
                        List<LoginModel> listaCabecera = new List<LoginModel>();
                        List<LoginModel> listaMenu = new List<LoginModel>();
                        foreach (var item in listaCab.ToList())
                        {
                            if (item.Nombre == "Panel De Control") { listaCabecera.Add(item); listaMenu.Add(item); }
                            if (item.Nombre == "Solicitud") { listaCabecera.Add(item); listaMenu.Add(item); }
                            if (item.Nombre == "Autorizaciones") { listaCabecera.Add(item); listaMenu.Add(item); }
                            if (item.Nombre == "Vacaciones") { listaCabecera.Add(item); listaMenu.Add(item); }
                        }
                        Session["menu"] = listaMenu;
                        Session["cabecera"] = listaCabecera;
                        return 1;
                    }
                    else
                    {
                        Session["usuario"] = null;
                        Session["menu"] = null;
                        Session["cabecera"] = null;
                        return 0;
                    }
                }
            }
            return 0;
        }

        public ActionResult LoadMenu()
        {
            return PartialView();
        }

        public int RecuperarPass(string usuario, string codEmpresa)
        {
            bool siExiste = usuarioRepository
                .ListarUsuario(codEmpresa)
                .Any(x => x.Usuario.Equals(usuario));
            if (siExiste)
            {
                var oUsuarioModel = usuarioRepository
                    .ListarUsuario(codEmpresa)
                    .FirstOrDefault(x => x.Usuario.Equals(usuario));
                var oPersonalModel = personalRepository.RecuperarPersonal(oUsuarioModel.CodPersonal, codEmpresa);
                String servidor = "smtp.gmail.com";
                int puerto = 587;
                String userEmisor = "limpterz@gmail.com";
                String pass = "iswdxvgjbpvcdari";
                if (oPersonalModel != null)
                {
                    if (oPersonalModel.CorreoElectronico != "")
                    {
                        String userReceptor = oPersonalModel.CorreoElectronico;

                        MimeMessage mensaje = new MimeMessage();
                        mensaje.From.Add(new MailboxAddress("Marco Gutierrez", userEmisor));
                        mensaje.To.Add(new MailboxAddress(oPersonalModel.NombreCompleto, userReceptor));
                        mensaje.Subject = "Recuperación De Credenciales - Sistema De Vacaciones";

                        BodyBuilder cuerpo = new BodyBuilder();
                        cuerpo.HtmlBody = $"<div><label>Buenos dias estimado/a{" " + oPersonalModel.NombreCompleto}. </label><br/><br/> <label> Sus credenciales de ingreso al Sistema De Vacaciones es: </label> <br/><br/> <label style='font-weight: bold'>Usuario: {oUsuarioModel.Usuario}</label> <br/> <label style='font-weight: bold'>Contraseña: {oUsuarioModel.Pass}</label><br/><br/><label>Saludos Cordiales.</label> </div>";

                        mensaje.Body = cuerpo.ToMessageBody();

                        SmtpClient clienteSmtp = new SmtpClient();
                        clienteSmtp.CheckCertificateRevocation = false;
                        clienteSmtp.Connect(servidor, puerto, MailKit.Security.SecureSocketOptions.StartTls);
                        clienteSmtp.Authenticate(userEmisor, pass);
                        clienteSmtp.Send(mensaje);
                        clienteSmtp.Disconnect(true);
                        return 1;
                    }
                    else
                    {
                        //El usuario no tiene correo
                        return 3;
                    }
                }
                else
                {
                    //Error
                    return 0;
                }
            }
            else if (siExiste == false)
            {
                //No existe el usuario
                return 2;
            }
            else
            {
                //Error
                return 0;
            }
        }
    }
}