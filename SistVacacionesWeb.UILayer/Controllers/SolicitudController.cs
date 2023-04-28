using MailKit.Net.Smtp;
using MimeKit;
using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.UILayer.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class SolicitudController : Controller
    {
        private ISolicitudRepository solicitudRepository = new SolicitudRepository();
        private IPersonalRepository personalRepository = new PersonalRepository();
        
        // GET: Solicitud
        [Seguridad]
        public ActionResult Solicitud()
        {
            return View();
        }

        public ActionResult _ListarSolicitud()
        {
            return PartialView();
        }

        public ActionResult _ListarSolicitudPendiente()
        {
            return PartialView();
        }

        public JsonResult ListarSolicitud()
        {
            var listSolicitud = solicitudRepository.ListarSolicitud(CodEmpresa.Value).Where(x => x.CodPersonal == CodPersonal.Value && x.Estado == 2).ToList();
            return Json(new { data = listSolicitud }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListarSolicitudPendiente()
        {
            var listSolicitud = solicitudRepository.ListarSolicitud(CodEmpresa.Value).Where(x => x.CodPersonal == CodPersonal.Value && x.Estado == 1).ToList();
            return Json(new { data = listSolicitud }, JsonRequestBehavior.AllowGet);
        }

        public int GrabarSolicitud(SolicitudModel oSolicitudModel)
        {
            oSolicitudModel.CodSolicitud = oSolicitudModel.CodSolicitud == null ? "" : oSolicitudModel.CodSolicitud;
            oSolicitudModel.CodPersonal = CodPersonal.Value;
            oSolicitudModel.CodEmpresa = CodEmpresa.Value;
            return solicitudRepository.GrabarSolicitud(oSolicitudModel);
        }

        public JsonResult RecuperarSolicitud(string codSolicitud)
        {
            return Json(solicitudRepository.RecuperarSolicitud(codSolicitud, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarSolicitudLogico(string codSolicitud)
        {
            var oSolicitud = solicitudRepository.RecuperarSolicitud(codSolicitud, CodEmpresa.Value);
            oSolicitud.EstaBorrado = true;
            return solicitudRepository.EliminarSolicitudLogico(oSolicitud);
        }

        public JsonResult CalcularTiempo(DateTime fechaSalida, DateTime fechaRetorno)
        {
            try
            {
                TimeSpan span = (fechaRetorno - fechaSalida);
                string dias = string.Format(CultureInfo.CurrentCulture, "{00}", span.Days);
                string horas = string.Format(CultureInfo.CurrentCulture, "{00}", span.Hours);
                string minutos = string.Format(CultureInfo.CurrentCulture, "{00}", span.Minutes);
                CalculoTiempoModel oCalculoTiempoModel = new CalculoTiempoModel();
                oCalculoTiempoModel.Dias = dias;
                oCalculoTiempoModel.Horas = horas;
                oCalculoTiempoModel.Minutos = minutos;
                return Json(oCalculoTiempoModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception) { return null; }
        }

        public int EnviarSolicitudCorreo(string codSolicitud)
        {
            try
            {
                var datosPersonal = personalRepository.RecuperarPersonal(CodPersonal.Value, CodEmpresa.Value);
                var datosJefe = personalRepository.RecuperarPersonal(datosPersonal.CodJefe, CodEmpresa.Value);
                var datosSolicitud = solicitudRepository.RecuperarSolicitud(codSolicitud, CodEmpresa.Value);
                if (datosJefe.CodPersonal != null)
                {
                    String servidor = "smtp.gmail.com";
                    int puerto = 587;
                    String userEmisor = "limpterz@gmail.com";
                    String pass = "iswdxvgjbpvcdari";
                    String userReceptor = datosJefe.CorreoElectronico;

                    MimeMessage mensaje = new MimeMessage();
                    mensaje.From.Add(new MailboxAddress("Marco Gutierrez", userEmisor));
                    mensaje.To.Add(new MailboxAddress(datosJefe.NombreCompleto, userReceptor));
                    mensaje.Subject = $"Solicitud Pendiente Codigo {datosSolicitud.CodSolicitud} - Sistema De Vacaciones";

                    BodyBuilder cuerpo = new BodyBuilder();
                    string estado; if (datosSolicitud.Estado == 1) { estado = "Pendiente"; } else { estado = "Resuelto"; }
                    cuerpo.HtmlBody = $"<div><label>Buenos dias estimado/a{" " + datosJefe.NombreCompleto }. </label><br/><br/> <label>Por medio del Sistema De Vacaciones se ha hecho una solicitud con los siguientes datos: </label> <br/><br/> <label >Codigo Solicitud: {datosSolicitud.CodSolicitud}</label> <br/> <label>Personal Solicitante: {datosPersonal.NombreCompleto}</label> <br/> <label>Tipo Documento: {datosPersonal.NombreTipoDocumento}</label><br/> <label>N° Documento: {datosPersonal.NumeroDocumento}</label><br/> <label>Concepto: {datosSolicitud.NombreConcepto}</label><br/> <label>Fecha - Hora Salida: {datosSolicitud.FechaSalida}</label><br/> <label>Fecha - Hora Retorno: {datosSolicitud.FechaRetorno}</label><br/> <label>Tiempo Solicitado: {datosSolicitud.TiempoCompleto}</label><br/> <label>Estado: {estado}</label><br/><br/><label>Ingrese al Sistema de Vacaciones para aprobar o denegar la solicitud presentada.</label><br/><br/><label>Saludos Cordiales.</label> </div>";

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