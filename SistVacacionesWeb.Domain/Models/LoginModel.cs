using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistVacacionesWeb.Domain.Models
{
    public class LoginModel
    {
        public int Correlativo { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Icon { get; set; }
        public string Nombre { get; set; }
    }

    //------------------- DATOS ESTATICOS - USUARIO ----------------------

    public class CodPersonal
    {
        private static string codPersonal = "";

        public static string Value
        {
            get { return codPersonal; }
            set { codPersonal = value; }
        }
    }

    public class CodUsuario
    {
        private static string codUsuario = "";

        public static string Value
        {
            get { return codUsuario; }
            set { codUsuario = value; }
        }
    }

    public class Nombre
    {
        private static string nombre = "";

        public static string Value
        {
            get { return nombre; }
            set { nombre = value; }
        }
    }

    public class Apellido
    {
        private static string apellido = "";

        public static string Value
        {
            get { return apellido; }
            set { apellido = value; }
        }
    }

    public class Usuario
    {
        private static string usuario = "";

        public static string Value
        {
            get { return usuario; }
            set { usuario = value; }
        }
    }

    public class Pass
    {
        private static string pass = "";

        public static string Value
        {
            get { return pass; }
            set { pass = value; }
        }
    }

    public class Rol
    {
        private static int rol = 0;

        public static int Value
        {
            get { return rol; }
            set { rol = value; }
        }
    }

    public class Foto
    {
        private static byte[] foto;

        public static byte[] Value
        {
            get { return foto; }
            set { foto = value; }
        }
    }

    public class FotoFotobase64
    {
        private static string fotoFotobase64;

        public static string Value
        {
            get { return fotoFotobase64; }
            set { fotoFotobase64 = value; }
        }
    }

    //------------------- DATOS ESTATICOS - EMPRESA ----------------------

    public class CodEmpresa
    {
        private static string codEmpresa = "";

        public static string Value
        {
            get { return codEmpresa; }
            set { codEmpresa = value; }
        }
    }

    public class RazonSocial
    {
        private static string razonSocial = "";

        public static string Value
        {
            get { return razonSocial; }
            set { razonSocial = value; }
        }
    }
    public class Ruc
    {
        private static string ruc = "";

        public static string Value
        {
            get { return ruc; }
            set { ruc = value; }
        }
    }

    public class CorreoElectronico
    {
        private static string correoElectronico = "";

        public static string Value
        {
            get { return correoElectronico; }
            set { correoElectronico = value; }
        }
    }

    public class Logo
    {
        private static byte[] logo;

        public static byte[] Value
        {
            get { return logo; }
            set { logo = value; }
        }
    }

    public class LogoFotobase64
    {
        private static string logoFotobase64;

        public static string Value
        {
            get { return logoFotobase64; }
            set { logoFotobase64 = value; }
        }
    }
}
