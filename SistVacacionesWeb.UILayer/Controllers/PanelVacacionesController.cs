using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.UILayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class PanelVacacionesController : Controller
    {
        private IPanelVacacionesRepository panelVacacionesRepository = new PanelVacacionesRepository(); 
        
        // GET: PanelVacaciones
        [Seguridad]
        public ActionResult PanelVacaciones()
        {
            return View();
        }

        public ActionResult _ListarVacacionesPeriodoPersonal()
        {
            return PartialView();
        }

        public ActionResult _ListarVacacionesConsumoPersonal(string codVacacionesPeriodo)
        {
            ViewData["codVacacionesPeriodo"] = codVacacionesPeriodo;
            return PartialView();
        }

        public JsonResult ListarVacacionesPeriodoPersonal()
        {
            var listVacacionesPeriodoPersonal = panelVacacionesRepository.ListarVacacionesPeriodo(CodPersonal.Value, CodEmpresa.Value);
            return Json(new { data = listVacacionesPeriodoPersonal }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarVacacionesConsumoPersonal(string codVacacionesPeriodo)
        {
            codVacacionesPeriodo = codVacacionesPeriodo.Replace('{', ' ').Replace('}', ' ').Trim();
            var listVacacionesConsumoPersonal = panelVacacionesRepository.ListarVacacionesConsumo(codVacacionesPeriodo, CodPersonal.Value, CodEmpresa.Value);
            return Json(new { data = listVacacionesConsumoPersonal }, JsonRequestBehavior.AllowGet);
        }
    }
}