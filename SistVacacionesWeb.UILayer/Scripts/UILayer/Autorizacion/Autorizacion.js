﻿window.onload = function () {
    ListarAutorizacion();
}

function ListarAutorizacion() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divAutorizacion").load("_ListarAutorizacion")
        });
    }, 500);
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

function ListarPersonal() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divPersonalAutorizante").load("_ListarPersonal")
        });
    }, 500);
}

function BuscarPersonalAutorizante() {
    $('#staticBackdrop2').modal('show');
    ListarPersonal();
}

function SelectPersonalAutorizante(codPersonal, nombreCompleto) {
    $("#CodPersonalAutorizante").val(codPersonal);
    $("#NombreCompletoAutorizante").val(nombreCompleto);
    document.getElementById("btnCerrarPersonalAutorizante").click();
}

function NuevaAutorizacion() {
    LimpiarAutorizacion();
    document.getElementById("staticBackdropLabel").innerHTML = "Nueva Autorizacion";
}

function GuardarAutorizacion() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmAutorizacion");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmAutorizacion");
    if (error != "") {
        Error(error);
        return;
    }

    var frmAutorizacion = document.getElementById("frmAutorizacion");
    var frm = new FormData(frmAutorizacion);
    var valor = $("#CodAutorizacion").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar la Autorización?", function () {
            fetchPostText("Autorizacion/GrabarAutorizacion", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarAutorizacion").click();
                    ListarAutorizacion();
                    LimpiarAutorizacion();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarAutorizacion();
                    Error("Hubo un error al guardar la Autorización.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios de la Autorización?", function () {
            fetchPostText("Autorizacion/GrabarAutorizacion", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarAutorizacion").click();
                    ListarAutorizacion();
                    LimpiarAutorizacion();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarAutorizacion();
                    Error("Hubo un error al guardar los cambios de la Autorización.");
                }
            })
        })
    }
}

function LimpiarAutorizacion() {
    LimpiarDatos("frmAutorizacion");
}

function EditarAutorizacion(CodAutorizacion) {
    LimpiarAutorizacion();
    document.getElementById("staticBackdropLabel").innerHTML = "Editar Autorización";
    setTimeout(recuperarGenericoEspecifico("Autorizacion/RecuperarAutorizacion/?CodAutorizacion=" + CodAutorizacion,
        "frmAutorizacion", [], false), 250);
}

function EliminarAutorizacion(CodAutorizacion) {
    Confirmacion("¿Desea eliminar la Autorización?", "Confirmar eliminación", function (res) {
        fetchGetText("Autorizacion/EliminarAutorizacionLogico/?CodAutorizacion=" + CodAutorizacion, function (rpta) {
            if (rpta == "1") {
                ListarAutorizacion();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarAutorizacion();
                Error("Hubo un error al eliminar el Autorizacion.");
            }
        })
    })
}

function BuscarSolicitud() {
    $('#staticBackdrop1').modal('show');
    ListarSolicitudPendiente();
}

function SelecSolicitud(CodSolicitud, FechaSolicitud, NombreCompleto) {
    $("#CodSolicitud").val(CodSolicitud);
    $("#NombreCompletoSolicitante").val(NombreCompleto);
    $("#FechaSolicitud").val(fixFecha(FechaSolicitud));
    document.getElementById("btnCerrarSolicitudPendiente").click();
}

function EnviarAutorizacionEmail(CodAutorizacion) {
    Confirmacion("¿Desea enviar una notificación de la autorización a su personal?", "Confirmar envio", function (res) {
        $("#progress").show();
        fetchGetText("Autorizacion/EnviarAutorizacionCorreo/?codAutorizacion=" + CodAutorizacion, function (rpta) {
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
                Error("No existe el Personal solicitante");
            }
        })
    })
}

function ValidarCampos() {
    let fechaAutorizacion = getN("FechaAutorizacion");
    let codSolicitud = getN("CodSolicitud");
    let codPersonalAutorizante = getN("CodPersonalAutorizante");
    let estado = getN("Estado");
    if (fechaAutorizacion != "" && fechaAutorizacion != undefined) {
        if (codSolicitud != "" && codSolicitud != undefined) {
            if (codPersonalAutorizante != "" && codPersonalAutorizante != undefined) {
                if (estado != "" && estado != undefined) {
                    return true;
                }
                else {
                    Error("Seleccione un Estado.");
                    return false;
                }
            }
            else {
                Error("Busque y seleccione un Personal Autorizante.");
                return false;
            }
        }
        else {
            Error("Busque y seleccione una Solicitud.");
            return false;
        }
    }
    else {
        Error("Seleccione la Fecha Autorización.");
        return false;
    }
}