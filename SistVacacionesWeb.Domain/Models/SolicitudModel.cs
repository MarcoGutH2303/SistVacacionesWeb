using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class SolicitudModel
    {
        public int Correlativo { get; set; }
        public int IdSolicitud { get; set; }
        public string CodSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string CodPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidoPersonal { get; set; }
        public string NombreCompletoPersonal { get; set; }
        public string CodConcepto { get; set; }
        public string NombreConcepto { get; set; }
        public DateTime FechaSalida { get; set; }
        public DateTime FechaRetorno { get; set; }
        public int NumeroDias { get; set; }
        public int NumeroHoras { get; set; }
        public int NumeroMinutos { get; set; }
        public string TiempoCompleto { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }

    public class CalculoTiempoModel
    {
        public string Dias { get; set; }
        public string Horas { get; set; }
        public string Minutos { get; set; }
    }
}
