using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class PersonalModel
    {
        public int Correlativo { get; set; }
        public int IdPersonal { get; set; }
        public string CodPersonal { get; set; }
        public string CodigoAnterior { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreCompleto { get; set; }
        public string CodTipoDocumento { get; set; }
        public string NombreTipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public int Sexo { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string CodCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public string CodArea { get; set; }
        public string NombreArea { get; set; }
        public string CodCargo { get; set; }
        public string NombreCargo { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CodJefe { get; set; }
        public string NombreJefe { get; set; }
        public string ApellidoJefe { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
    }
}
