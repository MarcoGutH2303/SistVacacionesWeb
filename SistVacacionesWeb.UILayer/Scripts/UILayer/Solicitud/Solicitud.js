window.onload = function () {
    ListarSolicitudPendiente();
}

function ListarSolicitudPendiente() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divSolicitudPendiente").load("_ListarSolicitudPendiente")
        });
    }, 500);
}

function ListarSolicitudGeneral() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divSolicitud").load("_ListarSolicitud")
        });
    }, 500);
}

function CargarCombobox() {
    fetchGet("Concepto/CargarConcepto", function (data) {
        llenarCombo(data, "cboConcepto", "Nombre", "CodConcepto", "0");
    });
}

function NuevaSolicitud() {
    LimpiarSolicitud();
    CargarCombobox();
    document.getElementById("staticBackdropLabel").innerHTML = "Nueva Solicitud";
}

function VerSolicitudGeneral() {
    ListarSolicitudGeneral();
}

function GuardarSolicitud() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmSolicitud");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmSolicitud");
    if (error != "") {
        Error(error);
        return;
    }

    var frmSolicitud = document.getElementById("frmSolicitud");
    var frm = new FormData(frmSolicitud);
    var valor = $("#CodSolicitud").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar la Solicitud?", function () {
            fetchPostText("Solicitud/GrabarSolicitud", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarSolicitud").click();
                    ListarSolicitudPendiente();
                    LimpiarSolicitud();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarSolicitud();
                    Error("Hubo un error al guardar la Solicitud.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios de la Solicitud?", function () {
            fetchPostText("Solicitud/GrabarSolicitud", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarSolicitud").click();
                    ListarSolicitudPendiente();
                    LimpiarSolicitud();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarSolicitud();
                    Error("Hubo un error al guardar los cambios de la Solicitud.");
                }
            })
        })
    }
}

function LimpiarSolicitud() {
    LimpiarDatos("frmSolicitud");
}

function EditarSolicitud(CodSolicitud) {
    LimpiarSolicitud();
    CargarCombobox();
    document.getElementById("staticBackdropLabel").innerHTML = "Editar Solicitud";
    setTimeout(recuperarGenericoEspecifico("Solicitud/RecuperarSolicitud/?CodSolicitud=" + CodSolicitud,
        "frmSolicitud", [], false), 250);
}

function EliminarSolicitud(CodSolicitud) {
    Confirmacion("¿Desea eliminar el Solicitud?", "Confirmar eliminación", function (res) {
        fetchGetText("Solicitud/EliminarSolicitudLogico/?CodSolicitud=" + CodSolicitud, function (rpta) {
            if (rpta == "1") {
                ListarSolicitudPendiente();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarSolicitudPendiente();
                Error("Hubo un error al eliminar la Solicitud.");
            }
        })
    })
}

function CalculoTiempo() {
    var f1 = $("#FechaSalida").val();
    var f2 = $("#FechaRetorno").val();
    if (f1 == undefined || f1 == '' || f2 == undefined || f2 == '') {

    }
    else {
        if (f1 <= f2) {
            fetchGet("Solicitud/CalcularTiempo/?fechaSalida=" + f1 + "&fechaRetorno=" + f2, function (res) {
                $("#NumeroDias").val(res["Dias"]);
                $("#NumeroHoras").val(res["Horas"]);
                $("#NumeroMinutos").val(res["Minutos"]);
            });
        }
        else {
            $("#NumeroDias").val("");
            $("#NumeroHoras").val("");
            $("#NumeroMinutos").val("");
        }
    }
}

function EnviarSolicitudEmail(CodSolicitud) {
    Confirmacion("¿Desea enviar una notificación de la solicitud a su superior?", "Confirmar envio", function (res) {
        $("#progress").show();
        fetchGetText("Solicitud/EnviarSolicitudCorreo/?codSolicitud=" + CodSolicitud, function (rpta) {
            if (rpta == "1") {
                $("#progress").hide();
                Correcto("Se envio la notificación correctamente");
            }
            else if (rpta == "0") {
                $("#progress").hide();
                Error("Hubo un error al enviar la notificación");
            }
            else if (rpta == "2") {
                $("#progress").hide();
                Error("Usted no tiene un Jefe superior registrado");
            }
        })
    })
}

function ValidarCampos() {
    let fechaSolicitud = getN("FechaSolicitud");
    let codConcepto = getN("CodConcepto");
    let fechaSalida = getN("FechaSalida");
    let fechaRetorno = getN("FechaRetorno");
    let estado = getN("Estado");
    if (fechaSolicitud != "" && fechaSolicitud != undefined) {
        if (codConcepto != "0" && codConcepto != undefined) {
            if (fechaSalida != "" && fechaSalida != undefined) {
                if (fechaRetorno != "" && fechaRetorno != undefined) {
                    if (estado != "" && estado != undefined) {
                        return true;
                    }
                    else {
                        Error("Seleccione el Estado.");
                        return false;
                    }
                }
                else {
                    Error("Seleccione la Fecha y Hora de Retorno.");
                    return false;
                }
            }
            else {
                Error("Seleccione la Fecha y Hora de Salida.");
                return false;
            }
        }
        else {
            Error("Seleccione el Concepto.");
            return false;
        }
    }
    else {
        Error("Seleccione la Fecha de Solicitud.");
        return false;
    }
}