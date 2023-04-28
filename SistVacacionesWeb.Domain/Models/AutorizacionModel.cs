using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class AutorizacionModel
    {
        public int Correlativo { get; set; }
        public int IdAutorizacion { get; set; }
        public string CodAutorizacion { get; set; }
        public string CodSolicitud { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string NombrePersonalSolicitante { get; set; }
        public string ApellidoPersonalSolicitante { get; set; }
        public string NombreCompletoSolicitante { get; set; }
        public string CodPersonalAutorizante { get; set; }
        public string NombrePersonalAutorizante { get; set; }
        public string ApellidoPersonalAutorizante { get; set; }
        public string NombreCompletoAutorizante { get; set; }
        public DateTime FechaAutorizacion { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
