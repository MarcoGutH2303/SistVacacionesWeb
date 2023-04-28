using MailKit.Net.Smtp;
using MimeKit;
using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.UILayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class AutorizacionController : Controller
    {
        IAutorizacionRepository autorizacionRepository = new AutorizacionRepository();
        ISolicitudRepository solicitudRepository = new SolicitudRepository();
        IPersonalRepository personalRepository = new PersonalRepository();

        // GET: Autorizacion
        [Seguridad]
        public ActionResult Autorizacion()
        {
            return View();
        }

        public ActionResult _ListarAutorizacion()
        {
            return PartialView();
        }

        public ActionResult _ListarSolicitudPendiente()
        {
            return PartialView();
        }

        public ActionResult _ListarPersonal()
        {
            return PartialView();
        }

        public JsonResult ListarAutorizacion()
        {
            var listAutorizacion = autorizacionRepository
                .ListarAutorizacion(CodEmpresa.Value);
            return Json(new { data = listAutorizacion }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarSolicitudPendiente()
        {
            var listSolicitudPendiente = solicitudRepository
                .ListarSolicitud(CodEmpresa.Value)
                .Where(x => x.Estado == 1)
                .ToList();
            return Json(new { data = listSolicitudPendiente }, JsonRequestBehavior.AllowGet);
        }

        public int GrabarAutorizacion(AutorizacionModel oAutorizacionModel)
        {
            oAutorizacionModel.CodAutorizacion = oAutorizacionModel.CodAutorizacion == null ? "" : oAutorizacionModel.CodAutorizacion;
            oAutorizacionModel.CodEmpresa = CodEmpresa.Value;
            int result = autorizacionRepository.GrabarAutorizacion(oAutorizacionModel);
            if (result > 0)
            {
                var oSolicitudModel = solicitudRepository.RecuperarSolicitud(oAutorizacionModel.CodSolicitud, CodEmpresa.Value);
                oSolicitudModel.Estado = 2;
                solicitudRepository.GrabarSolicitud(oSolicitudModel);
            }
            return result;
        }

        public JsonResult RecuperarAutorizacion(string codAutorizacion)
        {
            return Json(autorizacionRepository.RecuperarAutorizacion(codAutorizacion, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarAutorizacionLogico(string codAutorizacion)
        {
            var oAutorizacion = autorizacionRepository.RecuperarAutorizacion(codAutorizacion, CodEmpresa.Value);
            oAutorizacion.EstaBorrado = true;
            return autorizacionRepository.EliminarAutorizacionLogico(oAutorizacion);
        }

        public int EnviarAutorizacionCorreo(string codAutorizacion)
        {
            try
            {
                var datosAutorizacion = autorizacionRepository.RecuperarAutorizacion(codAutorizacion, CodEmpresa.Value);
                var datosSolicitud = solicitudRepository.RecuperarSolicitud(datosAutorizacion.CodSolicitud, CodEmpresa.Value);
                var datosPersonal = personalRepository.RecuperarPersonal(datosSolicitud.CodPersonal, CodEmpresa.Value);
                if (datosPersonal != null)
                {
                    String servidor = "smtp.gmail.com";
                    int puerto = 587;
                    String userEmisor = "limpterz@gmail.com";
                    String pass = "iswdxvgjbpvcdari";
                    String userReceptor = datosPersonal.CorreoElectronico;

                    MimeMessage mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress("Sistema De Vacaciones - SistCorp", userEmisor));
                    mensaje.To.Add(new MailboxAddress(datosPersonal.NombreCompleto, userReceptor));
                    mensaje.Subject = $"Autorizacion Codigo {datosAutorizacion.CodAutorizacion} - Sistema De Vacaciones";

                    BodyBuilder cuerpo = new BodyBuilder();
                    string estado; if (datosAutorizacion.Estado == 1) { estado = "APROBADO"; } else if (datosAutorizacion.Estado == 2) { estado = "DENEGADO"; } else { estado = "ANULADO"; }
                    cuerpo.HtmlBody = $"<div><label>Buenos dias estimado/a{" " + datosPersonal.NombreCompleto }. </label><br/><br/> <label>Por medio del Sistema De Vacaciones se ha realizado la autorización de la solicitud presentada con los siguientes datos: </label> <br/><br/> <label >Codigo Autorización: {datosAutorizacion.CodAutorizacion}</label> <br/><label >Codigo Solicitud: {datosAutorizacion.CodSolicitud}</label> <br/> <label>Personal Solicitante: {datosPersonal.NombreCompleto}</label> <br/> <label>Tipo Documento: {datosPersonal.NombreTipoDocumento}</label><br/> <label>N° Documento: {datosPersonal.NumeroDocumento}</label><br/> <label>Concepto: {datosSolicitud.NombreConcepto}</label><br/> <label>Fecha - Hora Salida: {datosSolicitud.FechaSalida}</label><br/> <label>Fecha - Hora Retorno: {datosSolicitud.FechaRetorno}</label><br/> <label>Tiempo Solicitado: {datosSolicitud.TiempoCompleto}</label><br/><br/><label>Dicha solicitud ha sido {estado}. Ingrese al Sistema De Vacaciones para verificar el estado.</label><br/><br/><label>Saludos Cordiales.</label> </div>";

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
                    return 2;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}