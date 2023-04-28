using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class UsuarioModel
    {
        public int Correlativo { get; set; }
        public int IdUsuario { get; set; }
        public string CodUsuario { get; set; }
        public string CodPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string ApellidoPersonal { get; set; }
        public string NombreCompletoPersonal { get; set; }
        public string NombreTipoDocumentoPersonal { get; set; }
        public string NumeroDocumentoPersonal { get; set; }
        public string Usuario { get; set; }
        public string Pass { get; set; }
        public int Rol { get; set; }
        public byte[] Foto { get; set; }
        public string NombreFoto { get; set; }
        public int Estado { get; set; }
        public string CodEmpresa { get; set; }
        public bool EstaBorrado { get; set; }
        public string Fotobase64 { get; set; }
    }
}
