window.onload = function () {
    $('#divNuevoPeriodo').hide();
    $('#divCargarVacaciones').show();
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

function ListarVacacionesPeriodo(codPersonal) {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divVacacionesPeriodo").load("_ListarVacacionesPeriodo/?codPersonal=" + codPersonal)
        });
    }, 500);
}

function ListarVacacionesConsumo(codVacacionesPeriodo, codPersonal) {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divVacacionesConsumo").load("_ListarVacacionesConsumo/?codVacacionesPeriodo=" + codVacacionesPeriodo + "&codPersonal=" + codPersonal)
        });
    }, 500);
}

var codPersonalSeleccionado = "";

function BuscarPersonal() {
    ListarPersonal();
}

function SelectPersonal(codPersonal, nombre) {
    document.getElementById("btnCerrarPersonal").click();
    $("#NombrePersonal").val(nombre);
    ListarVacacionesPeriodo(codPersonal)
    $('#divNuevoPeriodo').show();
    $('#divCargarVacaciones').hide();
    codPersonalSeleccionado = codPersonal;
}

function LimpiarPersonal() {
    codPersonalSeleccionado = 0;
    $("#NombrePersonal").val("");
    $('#divVacacionesPeriodo div').empty();
    $('#divNuevoPeriodo').hide();
    $('#divCargarVacaciones').show();
}

function NuevoVacacionesPeriodo() {
    var val = $("#NombrePersonal").val();
    if (val != "") {
        Limpiar("frmVacacionesPeriodo");
        $("#CodPersonalPeriodo").val(codPersonalSeleccionado);
        document.getElementById("staticBackdropLabel").innerHTML = "Nuevo Periodo";
        $("#staticBackdrop").modal("show");
        document.getElementById("AplicarAumentoDiasAdquiridosAutomatico").value = "1";
        document.getElementById("AplicarConsumoDiasAdquiridos").value = "1";
    }
    else {
        Error("Seleccione un Personal.");
    }
}

function GuardarVacacionesPeriodo() {
    var valida = ValidarCampos(1);
    if (valida == false) {
        return;
    }

    //var error = ValidarObligatorios("frmVacacionesPeriodo");
    //if (error != "") {
    //    Error(error);
    //    return;
    //}

    var error = ValidarSoloNumerosEnteros("frmVacacionesPeriodo");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmVacacionesPeriodo");
    if (error != "") {
        Error(error);
        return;
    }

    var frmVacacionesPeriodo = document.getElementById("frmVacacionesPeriodo");
    var frm = new FormData(frmVacacionesPeriodo);
    var valor = $("#CodVacacionesPeriodo").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar el Periodo?", function () {
            fetchPostText("Vacaciones/GrabarVacacionesPeriodo", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarVacacionesPeriodo").click();
                    ListarVacacionesPeriodo(codPersonalSeleccionado);
                    Limpiar("frmVacacionesPeriodo");
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarVacacionesPeriodo(codPersonalSeleccionado);
                    Error("Hubo un error al guardar el Periodo.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios del Periodo?", function () {
            fetchPostText("Vacaciones/GrabarVacacionesPeriodo", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarVacacionesPeriodo").click();
                    ListarVacacionesPeriodo(codPersonalSeleccionado);
                    Limpiar("frmVacacionesPeriodo");
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarVacacionesPeriodo(codPersonalSeleccionado);
                    Error("Hubo un error al guardar los cambios del Periodo.");
                }
            })
        })
    }
}

function Limpiar(idform, excepciones = []) {
    LimpiarDatos(idform, excepciones);
}

function EditarVacacionesPeriodo(codVacacionesPeriodo) {
    Limpiar("frmVacacionesPeriodo");
    document.getElementById("AplicarAumentoDiasAdquiridosAutomatico").value = "1";
    document.getElementById("AplicarConsumoDiasAdquiridos").value = "1";
    document.getElementById("staticBackdropLabel").innerHTML = "Editar Periodo";
    setTimeout(recuperarGenericoEspecifico("Vacaciones/RecuperarVacacionesPeriodo/?codVacacionesPeriodo=" + codVacacionesPeriodo + "&codPersonal=" + codPersonalSeleccionado,
        "frmVacacionesPeriodo", [], false), 250);
}

function EliminarVacacionesPeriodo(codVacacionesPeriodo) {
    Confirmacion("¿Desea eliminar el Perdiodo?", "Confirmar eliminación", function (res) {
        fetchGetText("Vacaciones/EliminarVacacionesPeridoLogico/?codVacacionesPeriodo=" + codVacacionesPeriodo + "&codPersonal=" + codPersonalSeleccionado, function (rpta) {
            if (rpta == "1") {
                ListarVacacionesPeriodo(codPersonalSeleccionado);
                Correcto("Se elimino correctamente");
            }
            else {
                ListarVacacionesPeriodo(codPersonalSeleccionado);
                Error("Hubo un error al eliminar el Periodo.");
            }
        })
    })
}

function VerConsumo(codVacacionesPeriodo, codPersonal, dias_ad, dias_con, dias_por) {
    $("#CodVacacionesPeriodoConsumo").val(codVacacionesPeriodo);
    $("#CodPersonalConsumo").val(codPersonal);
    $("#DiasAdquiridosConsumo").val(dias_ad);
    $("#DiasConsumidosConsumo").val(dias_con);
    $("#DiasPorConsumirConsumo").val(dias_por);
    ListarVacacionesConsumo(codVacacionesPeriodo, codPersonal)
}

function CalcularDias() {
    var f1 = $("#FechaInicio").val();
    var f2 = $("#FechaFin").val();
    if (f1 == undefined || f1 == '' || f2 == undefined || f2 == '') {

    }
    else {
        if (f1 <= f2) {
            var fecha1 = moment(f1);
            var fecha2 = moment(f2);
            var res = fecha2.diff(fecha1, 'days');
            $("#DiasUso").val(res + 1);
        }
        else {
            $("#DiasUso").val("");
        }
    }
}

var dias_consumo = 0;
function NuevoVacacionesConsumo() {
    Limpiar("frmVacacionesConsumo", ["CodVacacionesPeriodo", "CodPersonal", "DiasAdquiridos", "DiasConsumidos", "DiasPorConsumir"]);
}

function GuardarVacacionesConsumo() {
    var valida = ValidarCampos(2);
    if (valida == false) {
        return;
    }

    //var error = ValidarObligatorios("frmVacacionesConsumo");
    //if (error != "") {
    //    Error(error);
    //    return;
    //}

    var error = ValidarSoloNumerosEnteros("frmVacacionesConsumo");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmVacacionesConsumo");
    if (error != "") {
        Error(error);
        return;
    }

    var dias_ad = parseInt($("#DiasAdquiridosConsumo").val());
    var dias_con = parseInt($("#DiasConsumidosConsumo").val());
    var dias_por = parseInt($("#DiasPorConsumirConsumo").val());
    var dias_uso = parseInt($("#DiasUso").val());
    var id_vac_con = $("#CodVacacionesConsumoConsumo").val();

    if (id_vac_con == '') {
        if (dias_uso > dias_por) {
            Error("El N° de Dias Uso no puede ser mayor al N° de Dias Por Consumir");
            return
        }
    }
    else {
        // Edite dias uso por un numero mayor al que tenia
        if (dias_consumo < dias_uso) {
            var residuo = dias_uso - dias_consumo;
            if (residuo > dias_por) {
                Error("El N° de Dias Uso no puede ser mayor al N° de Dias Por Consumir");
                return
            }
        }
        else { }
    }

    var frmVacacionesConsumo = document.getElementById("frmVacacionesConsumo");
    var frm = new FormData(frmVacacionesConsumo);
    var valor = $("#CodVacacionesConsumoConsumo").val();
    var codpe = $("#CodPersonalConsumo").val();
    var codvp = $("#CodVacacionesPeriodoConsumo").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar el Consumo?", function () {
            fetchPostText("Vacaciones/GrabarVacacionesConsumo", frm, function (res) {
                if (res == "1") {
                    ListarVacacionesPeriodo(codpe);
                    ListarVacacionesConsumo(codvp, codpe);
                    ComplementoGuardarConsumo(dias_ad, dias_con, dias_uso)
                    Limpiar("frmVacacionesConsumo", ["CodVacacionesPeriodo", "CodPersonal", "DiasAdquiridos", "DiasConsumidos", "DiasPorConsumir"]);
                    Correcto("Se ha guardado correctamente.");
                }
                else {
                    ListarVacacionesPeriodo(codpe);
                    ListarVacacionesConsumo(codvp, codpe);
                    Error("Hubo un error al guardar el Consumo.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios del Consumo?", function () {
            fetchPostText("Vacaciones/GrabarVacacionesConsumo", frm, function (res) {
                if (res == "1") {
                    ListarVacacionesPeriodo(codpe);
                    ListarVacacionesConsumo(codvp, codpe);
                    ComplementoActualizarConsumo(dias_ad, dias_con, dias_uso);
                    Limpiar("frmVacacionesConsumo", ["CodVacacionesPeriodo", "CodPersonal", "DiasAdquiridos", "DiasConsumidos", "DiasPorConsumir"]);
                    Correcto("Se ha guardado correctamente.")
                }
                else {
                    ListarVacacionesPeriodo(codpe);
                    ListarVacacionesConsumo(codvp, codpe);
                    Error("Hubo un error al guardar los cambios del Consumo.");
                }
            })
        })
    }
}

function ComplementoGuardarConsumo(dias_ad, dias_con, dias_uso) {
    var dias_consumidos = dias_con + dias_uso;
    var dias_por_consumir = dias_ad - dias_consumidos;
    $("#DiasConsumidosConsumo").val(dias_consumidos);
    $("#DiasPorConsumirConsumo").val(dias_por_consumir);
}

function ComplementoActualizarConsumo(dias_ad, dias_con, dias_uso) {
    if (dias_consumo > dias_uso) {
        var residuo = dias_consumo - dias_uso;
        var dias_consumidos = dias_con - residuo;
        var dias_por_consumir = dias_ad - dias_consumidos;
        $("#DiasConsumidosConsumo").val(dias_consumidos);
        $("#DiasPorConsumirConsumo").val(dias_por_consumir);
    }
    else if (dias_consumo < dias_uso) {
        var residuo = dias_uso - dias_consumo;
        var dias_consumidos = dias_con + residuo;
        var dias_por_consumir = dias_ad - dias_consumidos;
        $("#DiasConsumidosConsumo").val(dias_consumidos);
        $("#DiasPorConsumirConsumo").val(dias_por_consumir);
    }
}

function EditarVacacionesConsumo(codVacacionesConsumo, codVacacionesPeriodo, diasUso) {
    recuperarGenericoEspecifico("Vacaciones/RecuperarVacacionesConsumo/?codVacacionesConsumo=" + codVacacionesConsumo + "&codVacacionesPeriodo=" + codVacacionesPeriodo + "&codPersonal=" + codPersonalSeleccionado,
        "frmVacacionesConsumo", [], false);
    dias_consumo = diasUso;
}

function EliminarVacacionesConsumo(codVacacionesConsumo, codVacacionesPeriodo, diasUso) {
    Confirmacion("¿Desea eliminar el Consumo?", "Confirmar eliminación", function (res) {
        fetchGetText("Vacaciones/EliminarVacacionesConsumoLogico/?codVacacionesConsumo=" + codVacacionesConsumo + "&codVacacionesPeriodo=" + codVacacionesPeriodo + "&codPersonal=" + codPersonalSeleccionado, function (rpta) {
            if (rpta == "1") {
                ListarVacacionesPeriodo(codPersonalSeleccionado);
                ListarVacacionesConsumo(codVacacionesPeriodo, codPersonalSeleccionado);
                ComplementoEliminarConsumo(diasUso);
                Correcto("Se elimino correctamente");
            }
            else {
                ListarVacacionesPeriodo(codPersonalSeleccionado);
                ListarVacacionesConsumo(codVacacionesPeriodo, codPersonalSeleccionado);
                Error("Hubo un error al eliminar el Consumo.");
            }
        })
    })
}

function ComplementoEliminarConsumo(dias_uso) {
    var dias_adquiridos = $("#DiasAdquiridosConsumo").val();
    var dias_consumidos = $("#DiasConsumidosConsumo").val();
    var dias_por_consumir = $("#DiasPorConsumirConsumo").val();
    dias_consumidos = dias_consumidos - dias_uso;
    dias_por_consumir = dias_adquiridos - dias_consumidos;
    $("#DiasConsumidosConsumo").val(dias_consumidos);
    $("#DiasPorConsumirConsumo").val(dias_por_consumir);
}

function DescargarPlantillaVacaciones() {
    document.location.href = setURL("Vacaciones/DescargarPlantilla");
}

function CargarPlantillaVacaciones() {
    LimpiarDatos("frmPlantillaPeriodo");
}

function ProcesoCarga() {
    var frmPlantillaPeriodo = document.getElementById("frmPlantillaPeriodo");
    var frm = new FormData(frmPlantillaPeriodo);
    fetchPostText("Vacaciones/CargarPlantilla", frm, function (res) {
        if (res.substring(0, 1) == "1") {
            CargaComplemento(res);
        }
        else if (res == "2") {
            Error("No hay periodos para cargar.");
        }
        else {
            Error("", 'Hubo un error al cargar los Periodos' + '<br/>' +
                'Lista de Personal con Periodo no cargado:' + '<br/>' + res);
        }
    })
}

function CargaComplemento(res) {
    document.getElementById("btnCerrarCarga").click();
    Correcto("Se ha cargado los Periodos correctamente.");
    Informacion("Información", "", 'Cantidad de periodos cargado: ' + res.split(':')[1]);
}

function changeFechaInicioPeriodo() {
    var fechaString = getN("FechaInicioPeriodo");
    var date = new Date(fechaString);
    date.setMonth(date.getMonth() + 12);
    date.setDate(date.getDate() - 1);
    setN("FechaFinPeriodo", fixFecha(date));
}

function changeAplicarAumentoDiasAdquiridosAutomatico() {
    if (document.getElementById("AplicarAumentoDiasAdquiridosAutomatico").checked == true) {
        //alert("Activo");
        $("#DiasAdquiridos").attr("readonly", "readonly");
        $("#DiasAdquiridos").val(0);
        $("#DiasConsumidos").attr("readonly", "readonly");
        $("#DiasConsumidos").val(0);
        $("#DiasPorConsumir").attr("readonly", "readonly");
        $("#DiasPorConsumir").val(0);
    }
    else {
        //alert("Inactivo");
        $("#DiasAdquiridos").removeAttr("readonly");
        $("#DiasAdquiridos").val("");
        $("#DiasConsumidos").removeAttr("readonly");
        $("#DiasConsumidos").val("");
        $("#DiasPorConsumir").removeAttr("readonly");
        $("#DiasPorConsumir").val("");
    }
}

function ValidarCampos(tipo) {
    if (tipo == 1) {
        let fechaInicioPeriodo = getN("FechaInicioPeriodo");
        let fechaFinPeriodo = getN("FechaFinPeriodo");
        let diasAdquiridos = getN("DiasAdquiridos");
        let diasConsumidos = getN("DiasConsumidos");
        let diasPorConsumir = getN("DiasPorConsumir");
        let estado = getN("Estado");
        if (fechaInicioPeriodo != "" && fechaInicioPeriodo != undefined) {
            if (fechaFinPeriodo != "" && fechaFinPeriodo != undefined) {
                if (diasAdquiridos != "" && diasAdquiridos != undefined) {
                    if (diasConsumidos != "" && diasConsumidos != undefined) {
                        if (diasPorConsumir != "" && diasPorConsumir != undefined) {
                            if (estado != "" && estado != undefined) {
                                return true;
                            }
                            else {
                                Error("Seleccione el Estado.");
                                return false;
                            }
                        }
                        else {
                            Error("Ingrese los Dias por Consumir.");
                            return false;
                        }
                    }
                    else {
                        Error("Ingrese los Dias Consumidos.");
                        return false;
                    }
                }
                else {
                    Error("Ingrese los Dias Adquiridos.");
                    return false;
                }
            }
            else {
                Error("Seleccione la Fecha Fin del Periodo.");
                return false;
            }
        }
        else {
            Error("Seleccione la Fecha Inicio del Periodo.");
            return false;
        }
    }
    else if (tipo == 2){
        let fechaInicio = getN("FechaInicio");
        let fechaFin = getN("FechaFin");
        let diasUso = getN("DiasUso");
        if (fechaInicio != "" && fechaInicio != undefined) {
            if (fechaFin != "" && fechaFin != undefined) {
                if (diasUso != "" && diasUso != undefined) {
                    return true;
                }
                else {
                    Error("Ingrese los Dias de Uso.");
                    return false;
                }
            }
            else {
                Error("Seleccione la Fecha Fin.");
                return false;
            }
        }
        else {
            Error("Seleccione la Fecha Inicio.");
            return false;
        }
    }
}