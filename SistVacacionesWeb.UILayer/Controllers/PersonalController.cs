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
    public class PersonalController : Controller
    {
        private IPersonalRepository personalRepository = new PersonalRepository();
        private ITipoDocumentoRepository tipoDocumentoRepository = new TipoDocumentoRepository();
        private ICargoRepository cargoRepository = new CargoRepository();

        private static string codTipoDocumento;
        private static string numeroDocumento;

        // GET: Personal
        [Seguridad]
        public ActionResult Personal()
        {
            return View();
        }

        public ActionResult _ListarPersonal()
        {
            return PartialView();
        }

        public JsonResult ListarPersonal()
        {
            var listPersonal = personalRepository.ListarPersonal(CodEmpresa.Value);
            return Json(new { data = listPersonal }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarPersonal()
        {
            var listPersonal = personalRepository.ListarPersonal(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(new { data = listPersonal }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CargarPersonalJefe()
        {
            var listPersonal = personalRepository.ListarPersonal(CodEmpresa.Value).Where(x => x.Estado == 1).ToList();
            return Json(listPersonal, JsonRequestBehavior.AllowGet);
        }

        public bool ValidarPersonal(string codTipoDocumento, string numeroDocumento)
        {
            var listPersonal = personalRepository.ListarPersonal(CodEmpresa.Value);
            return listPersonal.
                Any(x => x.CodTipoDocumento == codTipoDocumento &&
                    x.NumeroDocumento == numeroDocumento &&
                    x.CodEmpresa == CodEmpresa.Value);
        }

        public int GrabarPersonal(PersonalModel oPersonalModel)
        {
            oPersonalModel.CodPersonal = oPersonalModel.CodPersonal == null ? "" : oPersonalModel.CodPersonal;
            oPersonalModel.CodigoAnterior = oPersonalModel.CodigoAnterior == null ? "" : oPersonalModel.CodigoAnterior;
            oPersonalModel.CodEmpresa = CodEmpresa.Value;
            bool siExiste;
            if (oPersonalModel.CodPersonal == "")
            {
                siExiste = ValidarPersonal(oPersonalModel.CodTipoDocumento, oPersonalModel.NumeroDocumento);
                if (siExiste == false)
                    return personalRepository.GrabarPersonal(oPersonalModel);
                else
                    return 2;
            }
            else
            {
                if (oPersonalModel.CodTipoDocumento == codTipoDocumento && oPersonalModel.NumeroDocumento == numeroDocumento)
                {
                    return personalRepository.GrabarPersonal(oPersonalModel);
                }
                else
                {
                    siExiste = ValidarPersonal(oPersonalModel.CodTipoDocumento, oPersonalModel.NumeroDocumento);
                    if (siExiste == false)
                        return personalRepository.GrabarPersonal(oPersonalModel);
                    else
                        return 2;
                }
            }
        }

        public JsonResult RecuperarPersonal(string codPersonal)
        {
            var oPersonalModel = personalRepository.RecuperarPersonal(codPersonal, CodEmpresa.Value);
            codTipoDocumento = oPersonalModel.CodTipoDocumento;
            numeroDocumento = oPersonalModel.NumeroDocumento;
            return Json(personalRepository.RecuperarPersonal(codPersonal, CodEmpresa.Value),
                JsonRequestBehavior.AllowGet);
        }

        public int EliminarPersonalLogico(string codPersonal)
        {
            var oPersonal = personalRepository.RecuperarPersonal(codPersonal, CodEmpresa.Value);
            oPersonal.EstaBorrado = true;
            return personalRepository.EliminarPersonalLogico(oPersonal);
        }

        public int EliminarPersonalFisico(string codPersonal)
        {
            return personalRepository.EliminarPersonalFisico(codPersonal, CodEmpresa.Value);
        }

        public FileResult DescargarPlantilla()
        {
            EliminarArchivo();
            string rutaOriginal = Server.MapPath("~/Plantillas/Plantilla Personal.xlsx");
            var listTipoDocumento = tipoDocumentoRepository.ListarTipoDocumento(CodEmpresa.Value);
            var listCargo= cargoRepository.ListarCargo(CodEmpresa.Value);
            var random = new Random().Next(0, 5000);
            string rutaCopia = Server.MapPath($"~/Plantillas/Plantilla Personal - {random}.xlsx");
            System.IO.File.Copy(rutaOriginal, rutaCopia);
            try
            {
                var libro = new XLWorkbook(rutaCopia);
                var hoja = libro.Worksheet("Mantenimiento");
                int col = 1, row = 3;
                foreach (var dr in listTipoDocumento)
                {
                    hoja.Cell(row, col).Value = dr.CodTipoDocumento; hoja.Cell(row, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 1).Value = dr.Nombre; hoja.Cell(row, col + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    row++;
                }
                col = 4; row = 3;
                foreach (var dr in listCargo)
                {
                    hoja.Cell(row, col).Value = dr.CodCentroCosto; hoja.Cell(row, col).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 1).Value = dr.NombreCentroCosto; hoja.Cell(row, col + 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 2).Value = dr.CodArea; hoja.Cell(row, col + 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 3).Value = dr.NombreArea; hoja.Cell(row, col + 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 4).Value = dr.CodCargo; hoja.Cell(row, col + 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    hoja.Cell(row, col + 5).Value = dr.Nombre; hoja.Cell(row, col + 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    row++;
                }
                libro.SaveAs(rutaCopia);
                return File(rutaCopia, "application/xlsx", "Plantilla Personal.xlsx");
            }
            catch (Exception)
            {
                return File(rutaOriginal, "application/xlsx", "Plantilla Personal.xlsx");
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
        public string CargarPlantilla(HttpPostedFileBase plantillaPersonal)
        {
            Stream fsSource = plantillaPersonal.InputStream;
            IExcelDataReader reader = ExcelReaderFactory.CreateReader(fsSource);
            DataSet ds = new DataSet();
            ds = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                }
            });
            DataTable data = (DataTable)(ds.Tables[0]);
            int filas = data.Rows.Count;
            int correctos = 0, incorrectos = 0;
            string infoIncorrectos = ""; 
            if (filas > 0)
            {
                foreach (DataRow dr in data.Rows)
                {
                    bool siEsta = ValidarPersonal(dr[3].ToString(), dr[4].ToString());
                    if (siEsta == false)
                    {
                        PersonalModel oPersonalModel = new PersonalModel();
                        oPersonalModel.CodPersonal = "";
                        oPersonalModel.CodigoAnterior = dr[0] == DBNull.Value ? "" : dr[0].ToString();
                        oPersonalModel.Nombre = dr[1] == DBNull.Value ? "" : dr[1].ToString();
                        oPersonalModel.Apellido = dr[2] == DBNull.Value ? "" : dr[2].ToString();
                        oPersonalModel.CodTipoDocumento = dr[3] == DBNull.Value ? "" : dr[3].ToString();
                        oPersonalModel.NumeroDocumento = dr[4] == DBNull.Value ? "" : dr[4].ToString();
                        oPersonalModel.Sexo = dr[5] == DBNull.Value ? 0 : int.Parse(dr[5].ToString());
                        oPersonalModel.FechaNacimiento = dr[6] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr[6].ToString());
                        oPersonalModel.Direccion = dr[7] == DBNull.Value ? "" : dr[7].ToString();
                        oPersonalModel.Telefono = dr[8] == DBNull.Value ? "" : dr[8].ToString();
                        oPersonalModel.CorreoElectronico = dr[9] == DBNull.Value ? "" : dr[9].ToString();
                        oPersonalModel.CodCentroCosto = dr[10] == DBNull.Value ? "" : dr[10].ToString();
                        oPersonalModel.CodArea = dr[11] == DBNull.Value ? "" : dr[11].ToString();
                        oPersonalModel.CodCargo = dr[12] == DBNull.Value ? "" : dr[12].ToString();
                        oPersonalModel.FechaIngreso = dr[13] == DBNull.Value ? DateTime.Now : DateTime.Parse(dr[13].ToString());
                        oPersonalModel.Estado = dr[14] == DBNull.Value ? 0 : int.Parse(dr[14].ToString());
                        oPersonalModel.CodJefe = "PRL";
                        oPersonalModel.CodEmpresa = CodEmpresa.Value;
                        oPersonalModel.EstaBorrado = false;
                        if (personalRepository.GrabarPersonal(oPersonalModel) == 1)
                            correctos++;
                        else
                        {
                            incorrectos++;
                            infoIncorrectos += oPersonalModel.CodigoAnterior + " " + oPersonalModel.Nombre + " " + oPersonalModel.Apellido + "<br/>";
                        }
                    }
                    else 
                    { 
                        incorrectos = incorrectos + 1;
                        infoIncorrectos += dr[0].ToString() + " " + dr[1].ToString() + " " + dr[2].ToString() + "<br/>";
                    }
                }
                if (correctos == filas)
                {
                    return "1";
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
    }
}