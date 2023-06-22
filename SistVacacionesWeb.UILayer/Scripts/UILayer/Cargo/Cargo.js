window.onload = function () {
    ListarCargo();
}

function ListarCargo() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divCargo").load("_ListarCargo")
        });
    }, 500);
}

function CargarCombobox() {
    fetchGet("Area/CargarArea", function (data) {
        llenarCombo(data, "cboArea", "Nombre", "CodArea", "0");
    });
}

function NuevoCargo() {
    LimpiarCargo();
    CargarCombobox();
    $("#staticBackdropLabel").html("Nuevo Cargo");
}

function GuardarCargo() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmCargo");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmCargo");
    if (error != "") {
        Error(error);
        return;
    }

    var frmCargo = document.getElementById("frmCargo");
    var frm = new FormData(frmCargo);
    var codCargo = $("#CodCargo").val();
    if (codCargo == undefined || codCargo == '') {
        Confirmacion("Confirmación", "¿Desea guardar el Cargo?", function () {
            fetchPostText("Cargo/GrabarCargo", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarCargo").click();
                    ListarCargo();
                    LimpiarCargo();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarCargo();
                    Error("Hubo un error al guardar el Cargo.");
                }
            })
        })
    }
    else {
        Confirmacion("Confirmación", "¿Desea guardar los cambios del Cargo?", function () {
            fetchPostText("Cargo/GuardarCargo", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarCargo").click();
                    ListarCargo();
                    LimpiarCargo();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarCargo();
                    Error("Hubo un error al guardar los cambios del Cargo.");
                }
            })
        })
    }
}

function LimpiarCargo() {
    LimpiarDatos("frmCargo");
}

function EditarCargo(CodCargo) {
    LimpiarCargo();
    CargarCombobox();
    $("#staticBackdropLabel").html("Editar Cargo");
    setTimeout(recuperarGenericoEspecifico("Cargo/RecuperarCargo/?CodCargo=" + CodCargo,
        "frmCargo", [], false), 100);
}

function EliminarCargo(CodCargo) {
    Confirmacion("Confirmación", "¿Desea eliminar el Cargo?", function (res) {
        fetchGetText("Cargo/EliminarCargoLogico/?CodCargo=" + CodCargo, function (rpta) {
            if (rpta == "1") {
                ListarCargo();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarCargo();
                Error("Hubo un error al eliminar el Cargo.");
            }
        })
    })
}

function ValidarCampos() {
    let nombre = getN("Nombre");
    let codArea = getN("CodArea");
    let estado = getN("Estado");
    if (nombre != "" && nombre != undefined) {
        if (codArea != "0" && codArea != undefined) {
            if (estado != "" && estado != undefined) {
                return true;
            }
            else {
                Error("Seleccione un Estado.");
                return false;
            }
        }
        else {
            Error("Seleccione un Área.");
            return false;
        }
    }
    else {
        Error("Ingrese el Nombre.");
        return false;
    }
}