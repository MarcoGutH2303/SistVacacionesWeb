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
    public class PanelAutorizacionController : Controller
    {
        private IPanelAutorizacionRepository panelAutorizacionRepository = new PanelAutorizacionRepository();
        
        // GET: PanelAutorizacion
        [Seguridad]
        public ActionResult PanelAutorizacion()
        {
            return View();
        }

        public ActionResult _ListarAutorizacionPersonal()
        {
            return PartialView();
        }

        public JsonResult ListarAutorizacionPersonal()
        {
            var listAutorizacionPersonal = panelAutorizacionRepository.ListarAutorizacionPersonal(CodPersonal.Value, CodEmpresa.Value);
            return Json(new { data = listAutorizacionPersonal }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RecuperarAutorizacionPersonal(string codAutorizacion)
        {
            return Json(panelAutorizacionRepository.RecuperarAutorizacionPersonal(codAutorizacion, CodPersonal.Value, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }
    }
}