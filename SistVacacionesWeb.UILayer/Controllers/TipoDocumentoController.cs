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
    public class TipoDocumentoController : Controller
    {
        private ITipoDocumentoRepository tipoDocumentoRepository = new TipoDocumentoRepository();

        // GET: TipoDocumento
        [Seguridad]
        public ActionResult TipoDocumento()
        {
            return View();
        }

        public ActionResult _ListarTipoDocumento()
        {
            return PartialView();
        }

        public JsonResult ListarTipoDocumento()
        {
            var listTipoDocumento = tipoDocumentoRepository.ListarTipoDocumento(CodEmpresa.Value);
            return Json(new { data = listTipoDocumento }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarTipoDocumento()
        {
            var listTipoDocumento = tipoDocumentoRepository.ListarTipoDocumento(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(listTipoDocumento, JsonRequestBehavior.AllowGet);
        }

        public int GrabarTipoDocumento(TipoDocumentoModel oTipoDocumentoModel)
        {
            oTipoDocumentoModel.CodTipoDocumento = oTipoDocumentoModel.CodTipoDocumento == null ? "" : oTipoDocumentoModel.CodTipoDocumento;
            oTipoDocumentoModel.CodEmpresa = CodEmpresa.Value;
            return tipoDocumentoRepository.GrabarTipoDocumento(oTipoDocumentoModel);
        }

        public JsonResult RecuperarTipoDocumento(string codTipoDocumento)
        {
            return Json(tipoDocumentoRepository.RecuperarTipoDocumento(codTipoDocumento, CodEmpresa.Value), 
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarTipoDocumentoLogico(string codTipoDocumento)
        {
            var oTipoDocumento = tipoDocumentoRepository.RecuperarTipoDocumento(codTipoDocumento, CodEmpresa.Value);
            oTipoDocumento.EstaBorrado = true;
            return tipoDocumentoRepository.EliminarTipoDocumentoLogico(oTipoDocumento);
        }

        public int EliminarTipoDocumentoFisico(string codTipoDocumento)
        {
            return tipoDocumentoRepository.EliminarTipoDocumentoFisico(codTipoDocumento, CodEmpresa.Value);
        }
    }
}