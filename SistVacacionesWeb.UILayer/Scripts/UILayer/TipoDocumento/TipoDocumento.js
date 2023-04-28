window.onload = function () {
    ListarTipoDocumento();
}

function ListarTipoDocumento() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divTipoDocumento").load("_ListarTipoDocumento")
        });
    }, 500);
}

function NuevoTipoDocumento() {
    LimpiarTipoDocumento();
    document.getElementById("staticBackdropLabel").innerHTML = "Nuevo Tipo Documento";
}

function GuardarTipoDocumento() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmTipoDocumento");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmTipoDocumento");
    if (error != "") {
        Error(error);
        return;
    }

    var frmTipoDocumento = document.getElementById("frmTipoDocumento");
    var frm = new FormData(frmTipoDocumento);
    var valor = $("#CodTipoDocumento").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar el Tipo Documento?", function () {
            fetchPostText("TipoDocumento/GrabarTipoDocumento", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarTipoDocumento").click();
                    ListarTipoDocumento();
                    LimpiarTipoDocumento();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarTipoDocumento();
                    Error("Hubo un error al guardar el Tipo Documento.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios del Tipo Documento?", function () {
            fetchPostText("TipoDocumento/GuardarTipoDocumento", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarTipoDocumento").click();
                    ListarTipoDocumento();
                    LimpiarTipoDocumento();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarTipoDocumento();
                    Error("Hubo un error al guardar los cambios del Tipo Documento.");
                }
            })
        })
    }
}

function LimpiarTipoDocumento() {
    LimpiarDatos("frmTipoDocumento");
}

function EditarTipoDocumento(CodTipoDocumento) {
    LimpiarTipoDocumento();
    document.getElementById("staticBackdropLabel").innerHTML = "Editar Tipo Documento";
    recuperarGenericoEspecifico("TipoDocumento/RecuperarTipoDocumento/?CodTipoDocumento=" + CodTipoDocumento,
        "frmTipoDocumento", [], false);
}

function EliminarTipoDocumento(CodTipoDocumento) {
    Confirmacion("¿Desea eliminar el Tipo Documento?", "Confirmar eliminación", function (res) {
        fetchGetText("TipoDocumento/EliminarTipoDocumentoLogico/?CodTipoDocumento=" + CodTipoDocumento, function (rpta) {
            if (rpta == "1") {
                ListarTipoDocumento();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarTipoDocumento();
                Error("Hubo un error al eliminar el Tipo Documento.");
            }
        })
    })
}

function ValidarCampos() {
    let nombre = getN("Nombre");
    let estado = getN("Estado");
    if (nombre != "" && nombre != undefined) {
        if (estado != "" && estado != undefined) {
            return true;
        }
        else {
            Error("Seleccione un Estado.");
            return false;
        }
    }
    else {
        Error("Ingrese el Nombre.");
        return false;
    }
}