using SistVacacionesWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Helpers
{
    public class Seguridad : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usuario = HttpContext.Current.Session["usuario"];
            List<LoginModel> listaMenu = (List<LoginModel>)HttpContext.Current.Session["menu"];
            if (usuario == null || listaMenu == null)
            {
                filterContext.Result = new RedirectResult("~/Login/Login");
            }
            else
            {
                string nombreController = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string nombreAccion = filterContext.ActionDescriptor.ActionName;
                int? cantidad = listaMenu.Where(p => p.ControllerName == nombreController && p.ActionName == nombreAccion).Count();
                if (cantidad == 0)
                {
                    filterContext.Result = new RedirectResult("~/Login/Login");
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}