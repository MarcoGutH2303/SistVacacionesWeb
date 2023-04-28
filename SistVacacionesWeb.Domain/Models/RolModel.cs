using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class RolModel
    {
        public int Correlativo { get; set; }
        public int IdRol { get; set; }
        public string CodUsuario { get; set; }
        public int Empresa { get; set; }
        public int Mantenimiento { get; set; }
        public int Concepto { get; set; }
        public int Personal { get; set; }
        public int Autorizacion { get; set; }
        public int Vacaciones { get; set; }
        public int Reporte { get; set; }
        public int User { get; set; }
        public int Usuario { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
