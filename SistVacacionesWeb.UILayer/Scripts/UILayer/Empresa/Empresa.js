window.onload = function () {
    ListarEmpresa();
    PreviewLogo();
}

function ListarEmpresa() {
    $('#progress').show();
    setTimeout(function () {
        $("#divEmpresa").load("_ListarEmpresa")
        $('#progress').hide();
    }, 500);
}

function PreviewLogo() {
    var fupLogo = document.getElementById("fupLogo");
    fupLogo.onchange = function () {
        var file = fupLogo.files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById("imgLogo").src = reader.result;
        }
        reader.readAsDataURL(file);
    }
}

function NuevaEmpresa() {
    LimpiarEmpresa();
    $("#staticBackdropLabel").html("Nueva Empresa");
}

function GuardarEmpresa() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmEmpresa");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmEmpresa");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarCorreo("frmEmpresa");
    if (error != "") {
        Error(error);
        return;
    }

    var frmEmpresa = document.getElementById("frmEmpresa");
    var frm = new FormData(frmEmpresa);
    var codEmpresa = $("#CodEmpresa").val();
    if (codEmpresa == undefined || codEmpresa == '') {
        Confirmacion("Confirmación", "¿Desea crear la Empresa?", function () {
            fetchPostText("Empresa/GrabarEmpresa", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarEmpresa").click();
                    ListarEmpresa();
                    LimpiarEmpresa();
                    Correcto("Se ha guardado correctamente.");
                    Informacion("Información Importante", "", 'Credenciales De Ingreso: <br/>' +
                        'Usuario: admin<br/>' + 'Contraseña: 123');
                }
                else if (res == "2") {
                    ListarEmpresa();
                    Error("El RUC de la Empresa ya ha sido registrado anteriormemte.");
                }
                else {
                    ListarEmpresa();
                    Error("Hubo un error al crear la Empresa.");
                }
            })
        })
    }
    else {
        Confirmacion("Confirmación", "¿Desea guardar los cambios de la Empresa?", function () {
            fetchPostText("Empresa/GrabarEmpresa", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarEmpresa").click();
                    ListarEmpresa();
                    LimpiarEmpresa();
                    Correcto("Se ha guardado correctamente.")
                }
                else if (res == "2") {
                    ListarEmpresa();
                    Error("El RUC de la Empresa ya ha sido registrado anteriormemte.");
                }
                else {
                    ListarEmpresa();
                    Error("Hubo un error al guardar los cambios de la Empresa.");
                }
            })
        })
    }
}

function LimpiarEmpresa() {
    LimpiarDatos("frmEmpresa");
}

function EditarEmpresa(CodEmpresa) {
    LimpiarEmpresa();
    $("#staticBackdropLabel").html("Editar Empresa");
    setTimeout(recuperarGenericoEspecifico("Empresa/RecuperarEmpresa/?CodEmpresa=" + CodEmpresa,
        "frmEmpresa", [], false), 1000);
}

function recuperarEspecifico(res) {
    document.getElementById("imgLogo").src = res.fotobase64;
}

function EliminarEmpresa(CodEmpresa) {
    Confirmacion("Confirmación", "¿Desea eliminar la Empresa?", function (res) {
        fetchGetText("Empresa/EliminarEmpresaLogico/?CodEmpresa=" + CodEmpresa, function (rpta) {
            if (rpta == "1") {
                ListarEmpresa();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarEmpresa();
                Error("Hubo un error al eliminar la Empresa.");
            }
        })
    })
}

function ValidarCampos() {
    let razonSocial = getN("RazonSocial");
    let ruc = getN("Ruc");
    let correoElectronico = getN("CorreoElectronico");
    let estado = getN("Estado");
    if (razonSocial != "" && razonSocial != undefined) {
        if (ruc != "" && ruc != undefined) {
            if (correoElectronico != "" && correoElectronico != undefined) {
                if (estado != "" && estado != undefined) {
                    return true;
                }
                else {
                    Error("Seleccione un Estado.");
                    return false;
                }
            }
            else {
                Error("Ingrese el Correo Electronico.");
                return false;
            }
        }
        else {
            Error("Ingrese el RUC.");
            return false;
        }
    }
    else {
        Error("Ingrese la Razon Social.");
        return false;
    }
}