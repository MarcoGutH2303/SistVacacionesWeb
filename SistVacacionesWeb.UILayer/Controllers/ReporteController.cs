using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.UILayer.Helpers;
using SistVacacionesWeb.UILayer.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        [Seguridad]
        public ActionResult ReporteSolicitud()
        {
            return View();
        }

        public ActionResult _ListarPersonalSolicitud()
        {
            return PartialView();
        }

        public ActionResult ReporteSolicitudPersonal(string codPersonal)
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptSolicitudPersonal.rpt");
            //Parametros
            reporte.SetParameterValue("@CodPersonal", codPersonal);
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReporteSolicitudGeneral()
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptSolicitudGeneral.rpt");
            //Parametros
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReporteSolicitudFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptSolicitudFecha.rpt");
            //Parametros
            reporte.SetParameterValue("@FechaInicio", fechaInicio);
            reporte.SetParameterValue("@FechaFin", fechaFin);
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        [Seguridad]
        public ActionResult ReporteAutorizacion()
        {
            return View();
        }

        public ActionResult _ListarPersonalAutorizacion()
        {
            return PartialView();
        }

        public ActionResult ReporteAutorizacionPersonal(string codPersonal)
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptAutorizacionPersonal.rpt");
            //Parametros
            reporte.SetParameterValue("@CodPersonal", codPersonal);
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReporteAutorizacionGeneral()
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptAutorizacionGeneral.rpt");
            //Parametros
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReporteAutorizacionFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptAutorizacionFecha.rpt");
            //Parametros
            reporte.SetParameterValue("@FechaInicio", fechaInicio);
            reporte.SetParameterValue("@FechaFin", fechaFin);
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        [Seguridad]
        public ActionResult ReporteVacaciones()
        {
            return View();
        }

        public ActionResult _ListarPersonalVacaciones()
        {
            return PartialView();
        }

        public ActionResult ReporteVacacionesPersonal(string codPersonal)
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptVacacionesPersonal.rpt");
            //Parametros
            reporte.SetParameterValue("@CodPersonal", codPersonal);
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        public ActionResult ReporteVacacionesGeneral()
        {
            var reporte = new ReportClass();
            reporte.FileName = Server.MapPath("/Reports/rptVacacionesGeneral.rpt");
            //Parametros
            reporte.SetParameterValue("@CodEmpresa", CodEmpresa.Value);
            var stream = ProcesoReporte(reporte);
            return new FileStreamResult(stream, "application/pdf");
        }

        private Stream ProcesoReporte(ReportClass reporte)
        {
            var coninfo = ConexionReports.GetConnectionInfo();
            TableLogOnInfo logoninfo = new TableLogOnInfo();
            Tables tables;
            tables = reporte.Database.Tables;
            foreach (Table item in tables)
            {
                logoninfo = item.LogOnInfo;
                logoninfo.ConnectionInfo = coninfo;
                item.ApplyLogOnInfo(logoninfo);
            }
            Response.Buffer = false;
            Response.ClearHeaders();
            Response.ClearContent();
            Stream stream = reporte.ExportToStream(ExportFormatType.PortableDocFormat);
            return stream;
        }
    }
}