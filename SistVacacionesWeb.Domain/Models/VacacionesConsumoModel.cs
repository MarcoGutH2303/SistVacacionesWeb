using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class VacacionesConsumoModel
    {
        public int Correlativo { get; set; }
        public int IdVacacionesConsumo { get; set; }
        public string CodVacacionesPeriodo { get; set; }
        public string CodVacacionesConsumo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DiasUso { get; set; } // Decidir si va como decimal o entero
        public string CodPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidoPersonal { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
