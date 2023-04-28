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
    public class EmpresaController : Controller
    {
        private IEmpresaRepository empresaRepository = new EmpresaRepository();
        
        // GET: Empresa
        [Seguridad]
        public ActionResult Empresa()
        {
            return View();
        }

        public ActionResult _ListarEmpresa()
        {
            return PartialView();
        }

        public JsonResult ListarEmpresa()
        {
            var listEmpresa = empresaRepository.ListarEmpresa();
            return Json(new { data = listEmpresa }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarEmpresa()
        {
            var listEmpresa = empresaRepository.ListarEmpresa();
            return Json(listEmpresa, JsonRequestBehavior.AllowGet);
        }
        public int GrabarEmpresa(EmpresaModel oEmpresaModel, HttpPostedFileBase logo_empresa)
        {
            if (logo_empresa != null)
            {
                byte[] bufferLogo;
                string nombreLogo = logo_empresa.FileName;
                BinaryReader lector = new BinaryReader(logo_empresa.InputStream);
                bufferLogo = lector.ReadBytes((int)logo_empresa.ContentLength);
                oEmpresaModel.Logo = bufferLogo;
                oEmpresaModel.NombreLogo = nombreLogo;
            }
            oEmpresaModel.EstaBorrado = false;
            oEmpresaModel.CodEmpresa = oEmpresaModel.CodEmpresa == null ? "" : oEmpresaModel.CodEmpresa;
            var oUsuarioModel = new UsuarioModel();
            var oRolModel = new RolModel();
            if (oEmpresaModel.CodEmpresa == "")
            {   
                oUsuarioModel.CodUsuario = "";
                oUsuarioModel.CodPersonal = "";
                oUsuarioModel.Usuario = "admin";
                oUsuarioModel.Pass = "123";
                oUsuarioModel.Rol = 2;
                oUsuarioModel.Estado = 1;
                oUsuarioModel.Foto = null;
                oUsuarioModel.NombreFoto = "";
                oUsuarioModel.CodEmpresa = "";
                oUsuarioModel.EstaBorrado = false;

                oRolModel.CodUsuario = "";
                oRolModel.Empresa = 1;
                oRolModel.Mantenimiento = 1;
                oRolModel.Concepto = 1;
                oRolModel.Personal = 1;
                oRolModel.Autorizacion = 1;
                oRolModel.Vacaciones = 1;
                oRolModel.Reporte = 1;
                oRolModel.Usuario = 1;
                oRolModel.CodEmpresa = "";
                oRolModel.EstaBorrado = false;
            }
            return empresaRepository.GrabarEmpresa(oEmpresaModel, oUsuarioModel, oRolModel);
        }

        public JsonResult RecuperarEmpresa(string codEmpresa)
        {
            return Json(empresaRepository.RecuperarEmpresa(codEmpresa),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarEmpresaLogico(string codEmpresa)
        {
            var oEmpresa = empresaRepository.RecuperarEmpresa(codEmpresa);
            oEmpresa.EstaBorrado = true;
            return empresaRepository.EliminarEmpresaLogico(oEmpresa);
        }

        public int EliminarEmpresaFisico(string codEmpresa)
        {
            return empresaRepository.EliminarEmpresaFisico(codEmpresa);
        }
    }
}