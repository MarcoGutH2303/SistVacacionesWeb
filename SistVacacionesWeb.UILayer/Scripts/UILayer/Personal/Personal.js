window.onload = function () {
    ListarPersonal();
    CargarCombobox();
}

function ListarPersonal() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divPersonal").load("_ListarPersonal")
        });
    }, 500);
}

function CargarCombobox() {
    fetchGet("CentroCosto/CargarCentroCosto", function (data) {
        llenarCombo(data, "cboCentroCosto", "Nombre", "CodCentroCosto", "0");
    });
    fetchGet("TipoDocumento/CargarTipoDocumento", function (data) {
        llenarCombo(data, "cboTipoDocumento", "Nombre", "CodTipoDocumento", "0");
    });
    fetchGet("Personal/CargarPersonalJefe", function (data) {
        llenarCombo(data, "cboJefe", "NombreCompleto", "CodPersonal", "PRL");
    });
    fetchGet("Area/FiltrarArea/?codCentroCosto=" + "", function (data) {
        llenarCombo(data, "cboArea", "Nombre", "CodArea", "0");
    });
    fetchGet("Cargo/FiltrarCargo/?codArea=" + "", function (data) {
        llenarCombo(data, "cboCargo", "Nombre", "CodCargo", "0");
    });
}

function CentroCosto() {
    var codCentroCosto = get("cboCentroCosto");
    if (codCentroCosto != "" || codCentroCosto != undefined) {
        fetchGet("Area/FiltrarArea/?codCentroCosto=" + codCentroCosto, function (data) {
            llenarCombo(data, "cboArea", "Nombre", "CodArea", "0");
        })
    }
    else {
        fetchGet("Area/FiltrarArea/?codCentroCosto=" + "", function (data) {
            llenarCombo(data, "cboArea", "Nombre", "CodArea", "0");
        })
        fetchGet("Cargo/FiltrarCargo/?codArea=" + "", function (data) {
            llenarCombo(data, "cboCargo", "Nombre", "CodCargo", "0");
        })
    }
}

function Area() {
    var codArea = get("cboArea");
    fetchGet("Cargo/FiltrarCargo/?codArea=" + codArea, function (data) {
        llenarCombo(data, "cboCargo", "Nombre", "CodCargo", "0");
    })
}

function NuevoPersonal() {
    LimpiarPersonal();
    CargarCombobox();
    document.getElementById("staticBackdropLabel").innerHTML = "Nuevo Personal";
}

function GuardarPersonal() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmPersonal");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmPersonal");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarCorreo("frmPersonal");
    if (error != "") {
        Error(error);
        return;
    }

    var frmPersonal = document.getElementById("frmPersonal");
    var frm = new FormData(frmPersonal);
    var valor = $("#CodPersonal").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar el Personal?", function () {
            fetchPostText("Personal/GrabarPersonal", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarPersonal").click();
                    ListarPersonal();
                    LimpiarPersonal();
                    Correcto("Se ha guardado correctamente.");
                }
                else if (res == "2") {
                    ListarPersonal();
                    Error("Ya existe un Personal creado con el Tipo y N° Documento ingresado.");
                }
                else {
                    ListarPersonal();
                    Error("Hubo un error al guardar el Personal.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios del Personal?", function () {
            fetchPostText("Personal/GrabarPersonal", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarPersonal").click();
                    ListarPersonal();
                    LimpiarPersonal();
                    Correcto("Se ha guardado correctamente.")
                }
                else if (res == "2") {
                    ListarPersonal();
                    Error("Ya existe un Personal creado con el Tipo y N° Documento ingresado.");
                }
                else {
                    ListarPersonal();
                    Error("Hubo un error al guardar los cambios del Personal.");
                }
            })
        })
    }
}

function LimpiarPersonal() {
    LimpiarDatos("frmPersonal");
}

function EditarPersonal(CodPersonal) {
    LimpiarPersonal();
    setTimeout(function () {
        fetchGet("Personal/RecuperarPersonal/?codPersonal=" + CodPersonal, function (data1) {
            fetchGet("Area/FiltrarArea/?codCentroCosto=" + data1["CodCentroCosto"], function (data2) {
                llenarCombo(data2, "cboArea", "Nombre", "CodArea", "0");
                fetchGet("Cargo/FiltrarCargo/?codArea=" + data1["CodArea"], function (data3) {
                    llenarCombo(data3, "cboCargo", "Nombre", "CodCargo", "0");
                    Complemento(CodPersonal);
                });
            });
        });
    }, 300)
}

function Complemento(CodPersonal) {
    document.getElementById("staticBackdropLabel").innerHTML = "Editar Personal";
    setTimeout(function () {
        recuperarGenericoEspecifico("Personal/RecuperarPersonal/?CodPersonal=" + CodPersonal,
            "frmPersonal", [], false);
    }, 200)
}

function EliminarPersonal(CodPersonal) {
    Confirmacion("¿Desea eliminar el Personal?", "Confirmar eliminación", function (res) {
        fetchGetText("Personal/EliminarPersonalLogico/?CodPersonal=" + CodPersonal, function (rpta) {
            if (rpta == "1") {
                ListarPersonal();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarPersonal();
                Error("Hubo un error al eliminar el Personal.");
            }
        })
    })
}

function DescargarPlantilla() {
    document.location.href = setURL("Personal/DescargarPlantilla");
}

function CargarPlantilla() {
    LimpiarDatos("frmPlantillaPersonal");
}

function ProcesoCarga() {
    var frmPlantillaPersonal = document.getElementById("frmPlantillaPersonal");
    var frm = new FormData(frmPlantillaPersonal);
    $('#progress').show();
    fetchPostText("Personal/CargarPlantilla", frm, function (res) {
        if (res == "1") {
            $('#progress').hide();
            CargaComplemento(res);
            Informacion("Información", "", "Se ha cargado todos los registros de Personal correctamente");
        }
        else if (res == "2") {
            $('#progress').hide();
            Error("No hay Personal para cargar.");
        }
        else {
            $('#progress').hide();
            Error("", 'Hubo un error al cargar los registros del Personal' + '<br/>' +
                'Lista de Personal no cargado:' + '<br/>' + res);
        }
    })
}

function CargaComplemento(res) {
    document.getElementById("btnCerrarCarga").click();
    Correcto("Se ha cargado el personal correctamente.");
    ListarPersonal();
}

function ValidarCampos() {
    let nombre = getN("Nombre");
    let apellido = getN("Apellido");
    let codTipoDocumento = getN("CodTipoDocumento");
    let numeroDocumento = getN("NumeroDocumento");
    let sexo = getN("Sexo");
    let correoElectronico = getN("CorreoElectronico");
    let fechaNacimiento = getN("FechaNacimiento");
    let codCentroCosto = getN("CodCentroCosto");
    let codArea = getN("CodArea");
    let codCargo = getN("CodCargo");
    let fechaIngreso = getN("FechaIngreso");
    let estado = getN("Estado");
    if (nombre != "" && nombre != undefined) {
        if (apellido != "" && apellido != undefined) {
            if (codTipoDocumento != "0" && codTipoDocumento != undefined) {
                if (numeroDocumento != "" && numeroDocumento != undefined) {
                    if (sexo != "" && sexo != undefined) {
                        if (correoElectronico != "" && correoElectronico != undefined) {
                            if (fechaNacimiento != "" && fechaNacimiento != undefined) {
                                if (codCentroCosto != "0" && codCentroCosto != undefined) {
                                    if (codArea != "0" && codArea != undefined) {
                                        if (codCargo != "0" && codCargo != undefined) {
                                            if (fechaIngreso != "" && fechaIngreso != undefined) {
                                                if (estado != "" && estado != undefined) {
                                                    return true;
                                                }
                                                else {
                                                    Error("Seleccione un Estado.");
                                                    return false;
                                                }
                                            }
                                            else {
                                                Error("Seleccione una Fecha De Ingreso.");
                                                return false;
                                            }
                                        }
                                        else {
                                            Error("Seleccione un Cargo.");
                                            return false;
                                        }
                                    }
                                    else {
                                        Error("Seleccione un Área.");
                                        return false;
                                    }
                                }
                                else {
                                    Error("Seleccione un Centro De Costo.");
                                    return false;
                                }
                            }
                            else {
                                Error("Seleccione una Fecha De Nacimiento.");
                                return false;
                            }
                        }
                        else {
                            Error("Ingrese el Correo Electronico.");
                            return false;
                        }
                    }
                    else {
                        Error("Seleccione un Sexo.");
                        return false;
                    }
                }
                else {
                    Error("Ingrese el N° De Documento.");
                    return false;
                }
            }
            else {
                Error("Seleccione un Tipo De Documento.");
                return false;
            }
        }
        else {
            Error("Ingrese el Apellido.");
            return false;
        }
    }
    else {
        Error("Ingrese el Nombre.");
        return false;
    }
}