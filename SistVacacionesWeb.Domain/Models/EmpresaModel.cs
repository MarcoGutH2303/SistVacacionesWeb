using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class EmpresaModel
    {
        public int Correlativo { get; set; }
        public int IdEmpresa { get; set; }
        public string CodEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
        public string DomicilioFiscal { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public int Estado { get; set; }
        public byte[] Logo { get; set; }
        public string NombreLogo { get; set; }
        public bool EstaBorrado { get; set; }
        public string Fotobase64 { get; set; }
    }
}
