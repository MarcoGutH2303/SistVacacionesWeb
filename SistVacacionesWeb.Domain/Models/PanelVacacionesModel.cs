using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class PanelVacacionesPeriodoModel
    {
        public int Correlativo { get; set; }
        public string CodPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidoPersonal { get; set; }
        public string NombreCompletoPersonal { get; set; }
        public string CodVacacionesPeriodo { get; set; }
        public DateTime FechaInicioPeriodo { get; set; }
        public DateTime FechaFinPeriodo { get; set; }
        public decimal DiasAdquiridos { get; set; }
        public decimal DiasConsumidos { get; set; }
        public decimal DiasPorConsumir { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
    }

    public class PanelVacacionesConsumoModel
    {
        public int Correlativo { get; set; }
        public string CodPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidoPersonal { get; set; }
        public string NombreCompletoPersonal { get; set; }
        public string CodVacacionesPeriodo { get; set; }
        public string CodVacacionesConsumo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public int DiasUso { get; set; }
        public string CodEmpresa { get; set; }
    }
}
