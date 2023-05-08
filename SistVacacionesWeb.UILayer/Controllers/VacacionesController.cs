using ClosedXML.Excel;
using ExcelDataReader;
using SistVacacionesWeb.DataAccessLayer.Repository;
using SistVacacionesWeb.Domain.Models;
using SistVacacionesWeb.Domain.RepositoriesContracts;
using SistVacacionesWeb.UILayer.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistVacacionesWeb.UILayer.Controllers
{
    public class VacacionesController : Controller
    {
        IVacacionesPeriodoRepository vacacionesPeriodoRepository = new VacacionesPeriodoRepository();
        IVacacionesConsumoRepository vacacionesConsumoRepository = new VacacionesConsumoRepository();
        IPersonalRepository personalRepository = new PersonalRepository();

        // GET: Vacaciones
        [Seguridad]
        public ActionResult Vacaciones()
        {
            return View();
        }

        public ActionResult _ListarPersonal()
        {
            return PartialView();
        }

        public ActionResult _ListarVacacionesPeriodo(string codPersonal)
        {
            ViewData["codPersonal"] = codPersonal;
            return PartialView();
        }

        public ActionResult _ListarVacacionesConsumo(string codVacacionesPeriodo, string codPersonal)
        {
            ViewData["codVacacionesPeriodo"] = codVacacionesPeriodo;
            ViewData["codPersonal"] = codPersonal;
            return PartialView();
        }

        #region
        public JsonResult ListarVacacionesPeriodo(string codPersonal)
        {
            codPersonal = codPersonal.Replace('{', ' ').Replace('}', ' ').Trim();
            var listVacacionesPeriodo = vacacionesPeriodoRepository.ListarVacacionesPeriodo(codPersonal, CodEmpresa.Value);
            return Json(new { data = listVacacionesPeriodo }, JsonRequestBehavior.AllowGet);
        }
        public int GrabarVacacionesPeriodo(VacacionesPeriodoModel oVacacionesPeriodoModel)
        {
            oVacacionesPeriodoModel.CodVacacionesPeriodo = oVacacionesPeriodoModel.CodVacacionesPeriodo == null ? "" : oVacacionesPeriodoModel.CodVacacionesPeriodo;
            oVacacionesPeriodoModel.AplicarAumentoDiasAdquiridosAutomatico = oVacacionesPeriodoModel.AplAmtDsAdquiridosAuto == 1 ? true : false;
            oVacacionesPeriodoModel.AplicarConsumoDiasAdquiridos = oVacacionesPeriodoModel.AplCsmDsAdquiridos == 1 ? true : false;
            oVacacionesPeriodoModel.CodEmpresa = CodEmpresa.Value;
            int result = vacacionesPeriodoRepository.GrabarVacacionesPeriodo(oVacacionesPeriodoModel);
            vacacionesPeriodoRepository.AplicarAumentoAutomatico(CodEmpresa.Value);
            return result;
        }

        public JsonResult RecuperarVacacionesPeriodo(string codVacacionesPeriodo, string codPersonal)
        {
            var oVacacionesPeriodo = vacacionesPeriodoRepository.RecuperarVacacionesPeriodo(codVacacionesPeriodo, codPersonal, CodEmpresa.Value);
            oVacacionesPeriodo.AplAmtDsAdquiridosAuto = oVacacionesPeriodo.AplicarAumentoDiasAdquiridosAutomatico == true ? 1 : 0;
            oVacacionesPeriodo.AplCsmDsAdquiridos = oVacacionesPeriodo.AplicarConsumoDiasAdquiridos == true ? 1 : 0;
            return Json(oVacacionesPeriodo,
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarVacacionesPeriodoLogico(string codVacacionesPeriodo, string codPersonal)
        {
            var oVacacionesPeriodo = vacacionesPeriodoRepository.RecuperarVacacionesPeriodo(codVacacionesPeriodo, codPersonal, CodEmpresa.Value);
            oVacacionesPeriodo.EstaBorrado = true;
            return vacacionesPeriodoRepository.EliminarVacacionesPeriodoLogico(oVacacionesPeriodo);
        }

        public int EliminarVacacionesPeriodoFisico(string codVacacionesPeriodo, string codPersonal)
        {
            return vacacionesPeriodoRepository.EliminarVacacionesPeriodoFisico(codVacacionesPeriodo, codPersonal, CodEmpresa.Value);
        }
        #endregion
        #region
        public JsonResult ListarVacacionesConsumo(string codVacacionesPeriodo, string codPersonal)
        {
            codVacacionesPeriodo = codVacacionesPeriodo.Replace('{', ' ').Replace('}', ' ').Trim();
            codPersonal = codPersonal.Replace('{', ' ').Replace('}', ' ').Trim();
            var listVacacionesConsumo = vacacionesConsumoRepository.ListarVacacionesConsumo(codVacacionesPeriodo, codPersonal, CodEmpresa.Value);
            return Json(new { data = listVacacionesConsumo }, JsonRequestBehavior.AllowGet);
        }
        public int GrabarVacacionesConsumo(VacacionesConsumoModel oVacacionesConsumoModel)
        {
            oVacacionesConsumoModel.CodVacacionesConsumo = oVacacionesConsumoModel.CodVacacionesConsumo == null ? "" : oVacacionesConsumoModel.CodVacacionesConsumo;
            oVacacionesConsumoModel.CodEmpresa = CodEmpresa.Value;
            return vacacionesConsumoRepository.GrabarVacacionesConsumo(oVacacionesConsumoModel);
        }
        public JsonResult RecuperarVacacionesConsumo(string codVacacionesConsumo, string codVacacionesPeriodo, string codPersonal)
        {
            return Json(vacacionesConsumoRepository.RecuperarVacacionesConsumo(codVacacionesConsumo, codVacacionesPeriodo, codPersonal, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarVacacionesConsumoLogico(string codVacacionesConsumo, string codVacacionesPeriodo, string codPersonal)
        {
            var oVacacionesConsumo = vacacionesConsumoRepository.RecuperarVacacionesConsumo(codVacacionesConsumo, codVacacionesPeriodo, codPersonal, CodEmpresa.Value);
            oVacacionesConsumo.EstaBorrado = true;
            return vacacionesConsumoRepository.EliminarVacacionesConsumoLogico(oVacacionesConsumo);
        }

        public int EliminarVacacionesConsumoFisico(string codVacacionesConsumo, string codVacacionesPeriodo, string codPersonal)
        {
            return vacacionesConsumoRepository.EliminarVacacionesConsumoFisico(codVacacionesConsumo, codVacacionesPeriodo, codPersonal, CodEmpresa.Value);
        }
        #endregion
        #region
        public FileResult DescargarPlantilla()
        {
            EliminarArchivo();
            string rutaOriginal = Server.MapPath("~/Plantillas/Plantilla Vacaciones.xlsx");
            var listPersonal = personalRepository.ListarPersonal(CodEmpresa.Value).Where(x => x.Estado == 1);
            var random = new Random().Next(0, 5000);
            string rutaCopia = Server.MapPath($"~/Plantillas/Plantilla Vacaciones - {random}.xlsx");
            System.IO.File.Copy(rutaOriginal, rutaCopia);
            try
            {
                var libro = new XLWorkbook(rutaCopia);
                var hoja = libro.Worksheet(1);
                int col = 1, row = 2;
                foreach (var item in listPersonal)
                {
                    hoja.Cell(row, col).Value = item.CodPersonal; hoja.Cell(row, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 1).Value = item.CodigoAnterior; hoja.Cell(row, col + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 2).Value = item.NombreCompleto; hoja.Cell(row, col + 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 3).Value = item.NombreTipoDocumento; hoja.Cell(row, col + 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 4).Value = item.NumeroDocumento; hoja.Cell(row, col + 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 5).Value = item.FechaIngreso; hoja.Cell(row, col + 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    string estado; if (item.Estado == 1) { estado = "Activo"; } else { estado = "Inactivo"; }; hoja.Cell(row, col + 6).Value = estado; hoja.Cell(row, col + 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    row++;
                }
                libro.SaveAs(rutaCopia);
                return File(rutaCopia, "application/xlsx", "Plantilla Vacaciones.xlsx");
            }
            catch (Exception)
            {
                return File(rutaCopia, "application/xlsx", "Plantilla Vacaciones.xlsx");
            }
        }
        public void EliminarArchivo()
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Plantillas"));
                FileInfo[] files = di.GetFiles("*.xlsx");
                List<string> lista = new List<string>();
                foreach (var file in files)
                {
                    if (file.Name.Contains("-"))
                    {
                        string x = Server.MapPath("~/Plantillas/" + file);
                        System.IO.File.Delete(x);
                    }
                }
            }
            catch (Exception) { }
        }
        #endregion
        #region
        public string CargarPlantilla(HttpPostedFileBase plantillaPeriodo)
        {
            Stream fsSource = plantillaPeriodo.InputStream;
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fsSource);
            DataSet ds = new DataSet();
            ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            DataTable data = (DataTable)(ds.Tables[1]);
            int filas = data.Rows.Count;
            int correctos = 0, incorrectos = 0;
            string infoIncorrectos = "";
            if (filas > 0)
            {
                foreach (DataRow dr in data.Rows)
                {
                    VacacionesPeriodoModel oVacacionesPeriodoModel = new VacacionesPeriodoModel();
                    oVacacionesPeriodoModel.CodPersonal = dr[0] == DBNull.Value ? "" : dr[0].ToString();
                    oVacacionesPeriodoModel.CodVacacionesPeriodo = "";
                    oVacacionesPeriodoModel.FechaInicioPeriodo = dr[1] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr[1].ToString());
                    oVacacionesPeriodoModel.FechaFinPeriodo = dr[2] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr[2].ToString());
                    oVacacionesPeriodoModel.AplicarAumentoDiasAdquiridosAutomatico = dr[3] == DBNull.Value ? false : (int.Parse(dr[3].ToString()) == 1 ? true : false);
                    oVacacionesPeriodoModel.AplicarConsumoDiasAdquiridos = dr[4] == DBNull.Value ? false : (int.Parse(dr[4].ToString()) == 1 ? true : false);
                    oVacacionesPeriodoModel.DiasAdquiridos = dr[5] == DBNull.Value ? 0 : decimal.Parse(dr[5].ToString());
                    oVacacionesPeriodoModel.DiasConsumidos = dr[6] == DBNull.Value ? 0 : decimal.Parse(dr[6].ToString());
                    oVacacionesPeriodoModel.DiasPorConsumir = dr[7] == DBNull.Value ? 0 : decimal.Parse(dr[7].ToString());
                    oVacacionesPeriodoModel.Estado = dr[8] == DBNull.Value ? 0 : int.Parse(dr[8].ToString());
                    oVacacionesPeriodoModel.CodEmpresa = CodEmpresa.Value;
                    oVacacionesPeriodoModel.EstaBorrado = false;
                    if (vacacionesPeriodoRepository.GrabarVacacionesPeriodo(oVacacionesPeriodoModel) == 1)
                        correctos++;
                    else
                    {
                        incorrectos++;
                        infoIncorrectos += oVacacionesPeriodoModel.CodPersonal + "<br/>";
                    }
                }
                if (correctos == filas)
                {
                    return "1:" + correctos;
                }
                
                else
                {
                    return infoIncorrectos;
                }
            }
            else
            {
                return "2";
            }
        }
        #endregion
    }
}