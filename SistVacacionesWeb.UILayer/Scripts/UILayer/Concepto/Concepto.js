window.onload = function () {
    ListarConcepto();
}

function ListarConcepto() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divConcepto").load("_ListarConcepto")
        });
    }, 500);
}

function NuevoConcepto() {
    LimpiarConcepto();
    document.getElementById("staticBackdropLabel").innerHTML = "Nueva Concepto";
}

function GuardarConcepto() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmConcepto");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmConcepto");
    if (error != "") {
        Error(error);
        return;
    }

    var frmConcepto = document.getElementById("frmConcepto");
    var frm = new FormData(frmConcepto);
    var valor = $("#CodConcepto").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar el Concepto?", function () {
            fetchPostText("Concepto/GrabarConcepto", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarConcepto").click();
                    ListarConcepto();
                    LimpiarConcepto();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarConcepto();
                    Error("Hubo un error al guardar el Concepto.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios del Concepto?", function () {
            fetchPostText("Concepto/GuardarConcepto", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarConcepto").click();
                    ListarConcepto();
                    LimpiarConcepto();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarConcepto();
                    Error("Hubo un error al guardar los cambios del Concepto.");
                }
            })
        })
    }
}

function LimpiarConcepto() {
    LimpiarDatos("frmConcepto");
}

function EditarConcepto(CodConcepto) {
    LimpiarConcepto();
    document.getElementById("staticBackdropLabel").innerHTML = "Editar Concepto";
    setTimeout(recuperarGenericoEspecifico("Concepto/RecuperarConcepto/?CodConcepto=" + CodConcepto,
        "frmConcepto", [], false), 100);
}

function EliminarConcepto(CodConcepto) {
    Confirmacion("¿Desea eliminar el Concepto?", "Confirmar eliminación", function (res) {
        fetchGetText("Concepto/EliminarConceptoLogico/?CodConcepto=" + CodConcepto, function (rpta) {
            if (rpta == "1") {
                ListarConcepto();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarConcepto();
                Error("Hubo un error al eliminar el Concepto.");
            }
        })
    })
}

function ValidarCampos() {
    let nombre = getN("Nombre");
    let recuperable = getN("Recuperable");
    let estado = getN("Estado");
    if (nombre != "" && nombre != undefined) {
        if (recuperable != "" && recuperable != undefined) {
            if (estado != "" && estado != undefined) {
                return true;
            }
            else {
                Error("Seleccione un Estado.");
                return false;
            }
        }
        else {
            Error("Seleccione una respuesta a ¿Es Recuperable?.");
            return false;
        }
    }
    else {
        Error("Ingrese el Nombre.");
        return false;
    }
}