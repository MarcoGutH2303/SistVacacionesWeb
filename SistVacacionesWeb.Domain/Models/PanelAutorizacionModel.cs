using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class PanelAutorizacionModel
    {
        public int Correlativo { get; set; }
        public string CodAutorizacion { get; set; }
        public string CodSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string CodPersonal { get; set; }
        public string NombreCompletoPersonal { get; set; }
        public string CodConcepto { get; set; }
        public string NombreConcepto { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRetorno { get; set; }
        public int NumeroDias { get; set; }
        public int NumeroHoras { get; set; }
        public int NumeroMinutos { get; set; }
        public string TiempoCompleto { get; set; }
        public string NombreCompletoAutorizante { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public int EstadoAutorizacion { get; set; }
        public string CodEmpresa { get; set; }
    }
}
