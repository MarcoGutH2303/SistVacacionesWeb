window.onload = function () {
    ListarCentroCosto();
}

function ListarCentroCosto() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divCentroCosto").load("_ListarCentroCosto")
        });
    }, 500);
}

function NuevoCentroCosto() {
    LimpiarCentroCosto();
    $("#staticBackdropLabel").html("Nuevo Centro De Costo");
}

function GuardarCentroCosto() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmCentroCosto");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmCentroCosto");
    if (error != "") {
        Error(error);
        return;
    }

    var frmCentroCosto = document.getElementById("frmCentroCosto");
    var frm = new FormData(frmCentroCosto);
    var codCentroCosto = $("#CodCentroCosto").val();
    if (codCentroCosto == undefined || codCentroCosto == '') {
        Confirmacion("Confirmación", "¿Desea guardar el Centro Costo?", function () {
            fetchPostText("CentroCosto/GrabarCentroCosto", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarCentroCosto").click();
                    ListarCentroCosto();
                    LimpiarCentroCosto();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarCentroCosto();
                    Error("Hubo un error al guardar el Centro Costo.");
                }
            })
        })
    }
    else {
        Confirmacion("Confirmación", "¿Desea guardar los cambios del Centro Costo?", function () {
            fetchPostText("CentroCosto/GuardarCentroCosto", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarCentroCosto").click();
                    ListarCentroCosto();
                    LimpiarCentroCosto();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarCentroCosto();
                    Error("Hubo un error al guardar los cambios del Centro Costo.");
                }
            })
        })
    }
}

function LimpiarCentroCosto() {
    LimpiarDatos("frmCentroCosto");
}

function EditarCentroCosto(CodCentroCosto) {
    LimpiarCentroCosto();
    $("#staticBackdropLabel").html("Editar Centro De Costo");
    setTimeout(recuperarGenericoEspecifico("CentroCosto/RecuperarCentroCosto/?CodCentroCosto=" + CodCentroCosto,
        "frmCentroCosto", [], false), 100);
}

function EliminarCentroCosto(CodCentroCosto) {
    Confirmacion("Confirmación", "¿Desea eliminar el Centro Costo?", function (res) {
        fetchGetText("CentroCosto/EliminarCentroCostoLogico/?CodCentroCosto=" + CodCentroCosto, function (rpta) {
            if (rpta == "1") {
                ListarCentroCosto();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarCentroCosto();
                Error("Hubo un error al eliminar el Centro Costo.");
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