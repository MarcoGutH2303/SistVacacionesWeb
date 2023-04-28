using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class CargoModel
    {
        public int Correlativo { get; set; }
        public int IdCargo { get; set; }
        public string CodCargo { get; set; }
        public string Nombre { get; set; }
        public string CodArea { get; set; }
        public string NombreArea { get; set; }
        public string CodCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
