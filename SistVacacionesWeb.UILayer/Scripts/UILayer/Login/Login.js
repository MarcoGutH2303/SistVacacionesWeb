window.onload = function () {
    listarCombo();
    $('#usuario').focus();
}

function listarCombo() {
    fetchGet("Empresa/CargarEmpresa", function (data) {
        llenarCombo(data, "cboEmpresa", "RazonSocial", "CodEmpresa", "0");
    })
    fetchGet("Empresa/CargarEmpresa", function (data) {
        llenarCombo(data, "cboEmpresaRecuperar", "RazonSocial", "CodEmpresa", "0");
    })
}

function Login() {
    var usuario = getN("usuario");
    var pass = getN("pass");
    var codEmpresa = getN("codEmpresa");
    fetchGet("Login/ValidarDatosUsuario/?usuario=" + usuario + "&pass=" + pass + "&codEmpresa=" + codEmpresa, function (data) {
        if (data.CodUsuario == null) {
            Error("Usuario o Contraseña Incorrecto");
        }
        else {
            if (data.Rol == "2") {
                Swal.fire({
                    title: 'Bienvenido Al Sistema',
                    text: '¿Que sesión desea ingresar?',
                    showDenyButton: true,
                    showCancelButton: true,
                    confirmButtonText: 'Administrador',
                    denyButtonText: 'Empleado',
                    cancelButtonText: 'Cancelar',
                }).then((result) => {
                    if (result.isConfirmed) {
                        fetchGetText("Login/CargarSesion/?usuario=" + usuario + "&pass=" + pass + "&codEmpresa=" + codEmpresa + "&rolUser=Administrador&codUsuario=" + data.CodUsuario, function (rpta) {
                            if (rpta == "1") {
                                document.location.href = setURL("PanelControl/Administrador");
                            }
                        })
                    } else if (result.isDenied) {
                        fetchGetText("Login/CargarSesion/?usuario=" + usuario + "&pass=" + pass + "&codEmpresa=" + codEmpresa + "&rolUser=Empleado&codUsuario=" + data.CodUsuario, function (rpta) {
                            if (rpta == "1") {
                                document.location.href = setURL("PanelControl/Empleado");
                            }
                        })
                    }
                })
            }
            else if (data.Rol == "1") {
                Correcto("Bienvenido");
                fetchGetText("Login/CargarSesion/?usuario=" + usuario + "&pass=" + pass + "&codEmpresa=" + codEmpresa + "&rolUser=Empleado&codUsuario=" + data.CodUsuario, function (rpta) {
                    if (rpta == "1") {
                        document.location.href = setURL("PanelControl/Empleado");
                    }
                })
            }
        }
    })
}

function RecuperarPass() {
    document.getElementById("UsuarioRecuperar").value = "";
    document.getElementById("cboEmpresaRecuperar").value = 0;
    setTimeout(function () {
        $('#UsuarioRecuperar').focus();
    }, 500);
}

function EnviarCredencial() {
    var usuario = get("UsuarioRecuperar");
    var codEmpresa = get("cboEmpresaRecuperar");
    Confirmacion("Se procedera a validar el Usuario.", "Confirmación", function (res) {
        $("#progress").show();
        fetchGet("Login/RecuperarPass/?usuario=" + usuario + "&codEmpresa=" + codEmpresa, function (rpta) {
            if (rpta == 1) {
                $("#progress").hide();
                document.getElementById("btnCerrar").click();
                InformacionImportante("Información", "Se ha validado correctamente el Usuario. Por consecuencia, se enviado las credenciales de ingreso al correo electronico del Usuario", "")
            }
            else if (rpta == 2) {
                $("#progress").hide();
                Error("No existe el Usuario ingresado.");
            }
            else if (rpta == 3) {
                $("#progress").hide();
                Error("El Usuario no tiene correo electronico registrado.");
            }
            else if (rpta == 0) {
                $("#progress").hide();
                Error("Hubo un error al validar el Usuario.");
            }
        })
    })
}