using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class ConceptoModel
    {
        public int Correlativo { get; set; }
        public int IdConcepto { get; set; }
        public string CodConcepto { get; set; }
        public string Nombre { get; set; }
        public int Recuperable { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
