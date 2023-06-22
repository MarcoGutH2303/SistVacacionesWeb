window.onload = function () {
    ListarArea();
}

function ListarArea() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divArea").load("_ListarArea")
        });
    }, 500);
}

function CargarCombobox() {
    fetchGet("CentroCosto/CargarCentroCosto", function (data) {
        llenarCombo(data, "cboCentroCosto", "Nombre", "CodCentroCosto", "0");
    });
}

function NuevaArea() {
    LimpiarArea();
    CargarCombobox();
    $("#staticBackdropLabel").html("Nueva Area");
}

function GuardarArea() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmArea");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmArea");
    if (error != "") {
        Error(error);
        return;
    }

    var frmArea = document.getElementById("frmArea");
    var frm = new FormData(frmArea);
    var codArea = $("#CodArea").val();
    if (codArea == undefined || codArea == '') {
        Confirmacion("Confirmación", "¿Desea guardar el Area?", function () {
            fetchPostText("Area/GrabarArea", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarArea").click();
                    ListarArea();
                    LimpiarArea();
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarArea();
                    Error("Hubo un error al guardar el Area.");
                }
            })
        })
    }
    else {
        Confirmacion("Confirmación", "¿Desea guardar los cambios del Area?", function () {
            fetchPostText("Area/GrabarArea", frm, function (res) {
                if (res == "1") {
                    $("#btnCerrarArea").click();
                    ListarArea();
                    LimpiarArea();
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarArea();
                    Error("Hubo un error al guardar los cambios del Area.");
                }
            })
        })
    }
}

function LimpiarArea() {
    LimpiarDatos("frmArea");
}

function EditarArea(CodArea) {
    LimpiarArea();
    CargarCombobox();
    $("#staticBackdropLabel").html("Editar Area");
    setTimeout(recuperarGenericoEspecifico("Area/RecuperarArea/?CodArea=" + CodArea,
        "frmArea", [], false), 100);
}

function EliminarArea(CodArea) {
    Confirmacion("Confirmación", "¿Desea eliminar el Area?", function (res) {
        fetchGetText("Area/EliminarAreaLogico/?CodArea=" + CodArea, function (rpta) {
            if (rpta == "1") {
                ListarArea();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarArea();
                Error("Hubo un error al eliminar el Area.");
            }
        })
    })
}

function ValidarCampos() {
    let nombre = getN("Nombre");
    let codCentroCosto = getN("CodCentroCosto");
    let estado = getN("Estado");
    if (nombre != "" && nombre != undefined) {
        if (codCentroCosto != "0" && codCentroCosto != undefined) {
            if (estado != "" && estado != undefined) {
                return true;
            }
            else {
                Error("Seleccione un Estado.");
                return false;
            }
        }
        else {
            Error("Seleccione un Centro De Costo.");
            return false;
        }
    }
    else {
        Error("Ingrese el Nombre.");
        return false;
    }
}