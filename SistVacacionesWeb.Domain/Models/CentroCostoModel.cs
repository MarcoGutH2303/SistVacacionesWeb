using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class CentroCostoModel
    {
        public int Correlativo { get; set; }
        public int IdCentroCosto { get; set; }
        public string CodCentroCosto { get; set; }
        public string CodigoAnterior { get; set; }
        public string Nombre { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
