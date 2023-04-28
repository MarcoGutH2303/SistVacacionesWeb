using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class PanelControlController : Controller
    {
        private IPanelControlRepository panelRepository = new PanelControlRepository();
        
        private IPersonalRepository personalRepository = new PersonalRepository();
        //private IConceptoRepository conceptoRepository = new ConceptoRepository();
        private ISolicitudRepository solicitudRepository = new SolicitudRepository();
        private IAutorizacionRepository autorizacionRepository = new AutorizacionRepository();
        private IVacacionesPeriodoRepository vacacionesPeriodoRepository = new VacacionesPeriodoRepository();
        //private IUsuarioRepository usuarioRepository = new UsuarioRepository();

        // GET: PanelControl
        public ActionResult Administrador()
        {
            return View();
        }

        public ActionResult Empleado()
        {
            return View();
        }

        //public int CantidadPersonalActivo()
        //{
        //    return personalRepository.ListarPersonal(CodEmpresa.Value)
        //        .Count(x => x.Estado == 1);
        //}

        //public int CantidadConceptoActivo()
        //{
        //    return conceptoRepository.ListarConcepto(CodEmpresa.Value)
        //        .Count(x => x.Estado == 1);
        //}

        // ----------------- Panel Control Administrador ------------------

        public ActionResult PanelControlAdministrador()
        {
            var oPanelControlAdministrador = panelRepository.RecuperarPanelControlAdministrador(CodEmpresa.Value);
            return Json(oPanelControlAdministrador, JsonRequestBehavior.AllowGet);
        }

        // ----------------- Panel Control Empleado ------------------

        public ActionResult PanelControlEmpleado()
        {
            var oPanelControlEmpleado = panelRepository.RecuperarPanelControlEmpleado(CodPersonal.Value, CodEmpresa.Value);
            return Json(oPanelControlEmpleado, JsonRequestBehavior.AllowGet);
        }
    }
}