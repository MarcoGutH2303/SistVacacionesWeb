using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class VacacionesPeriodoModel
    {
        public int Correlativo { get; set; }
        public int IdVacacionesPeriodo { get; set; }
        public string CodVacacionesPeriodo { get; set; }
        public DateTime FechaInicioPeriodo { get; set; }
        public DateTime FechaFinPeriodo { get; set; }
        public decimal DiasAdquiridos { get; set; }
        public decimal DiasConsumidos { get; set; }
        public decimal DiasPorConsumir { get; set; }
        public bool AplicarAumentoDiasAdquiridosAutomatico { get; set; }
        public int AplAmtDsAdquiridosAuto { get; set; }
        public bool AplicarConsumoDiasAdquiridos { get; set; }
        public int AplCsmDsAdquiridos { get; set; }
        public int Estado { get; set; }
        public string CodPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidoPersonal { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
