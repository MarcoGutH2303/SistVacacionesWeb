function BuscarPersonalSolicitud() {
    $(document).ready(function () {
        $("#divPersonalSolicitud").load("_ListarPersonalSolicitud")
    });
}

function SelectPersonalSolicitud(codPersonalSolicitud, nombre) {
    $("#CodPersonalSolicitud").val(codPersonalSolicitud);
    $("#NombrePersonalSolicitud").val(nombre);
    document.getElementById("btnCerrarPersonalSolicitud").click();
}

function VerReporteSolicitud() {
    var id = get("cboTipoReporteSolicitud");
    if (id == 1) {
        var codPersonal= get("CodPersonalSolicitud");
        $("#visorReporteSolicitud").attr("src", "ReporteSolicitudPersonal/?codPersonal=" + codPersonal).load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 2) {
        $("#visorReporteSolicitud").attr("src", "ReporteSolicitudGeneral").load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 3) {
        var fechaInicio = get("FechaInicioSolicitud");
        var fechaFin = get("FechaFinSolicitud");
        $("#visorReporteSolicitud").attr("src", "ReporteSolicitudFecha/?fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin).load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 0) {
        Error("Seleccione un Tipo Reporte")
    }
}

function DescargarReporteSolicitud() {
    var id = get("cboTipoReporteSolicitud");
    if (id == 1) {
        var codPersonal = get("CodPersonalSolicitud");
        window.open("ReporteSolicitudPersonal/?codPersonal=" + codPersonal);
    }
    else if (id == 2) {
        window.open("ReporteSolicitudGeneral");
    }
    else if (id == 3) {
        var fechaInicio = get("FechaInicioSolicitud");
        var fechaFin = get("FechaFinSolicitud");
        window.open("ReporteSolicitudFecha/?fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin);
    }
    else if (id == 0) {
        Error("Seleccione un Tipo Reporte")
    }
}

function ChangeSolicitud() {
    var id = get("cboTipoReporteSolicitud");
    let fechaInicio = document.getElementById("FechaInicioSolicitud");
    let fechaFin = document.getElementById("FechaFinSolicitud");
    let btn_personal = document.getElementById("btnBuscarPersonalSolicitud");
    if (id == 0) {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = true;
        $('#CodPersonalSolicitud').val('');
        $('#FechaInicioSolicitud').val('');
        $('#FechaFinSolicitud').val('');
        $('#NombrePersonalSolicitud').val('');
    }
    else if (id == 1) {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = false;
        $('#CodPersonalSolicitud').val('');
        $('#fechaInicioSolicitud').val('');
        $('#fechaFinSolicitud').val('');
        $('#NombrePersonalSolicitud').val('');
    }
    else if (id == 2) {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = true;
        $('#CodPersonalSolicitud').val('');
        $('#FechaInicioSolicitud').val('');
        $('#FechaFinSolicitud').val('');
        $('#NombrePersonalSolicitud').val('');
    }
    else if (id == 3) {
        fechaInicio.disabled = false;
        fechaFin.disabled = false;
        btn_personal.disabled = true;
        $('#CodPersonalSolicitud').val('');
        $('#FechaInicioSolicitud').val('');
        $('#FechaFinSolicitud').val('');
        $('#NombrePersonalSolicitud').val('');
    }
    else {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = true;
        $('#CodPersonalSolicitud').val('');
        $('#FechaInicioSolicitud').val('');
        $('#FechaFinSolicitud').val('');
        $('#NombrePersonalSolicitud').val('');
    }
}

function BuscarPersonalAutorizacion() {
    $(document).ready(function () {
        $("#divPersonalAutorizacion").load("_ListarPersonalAutorizacion")
    });
}

function SelectPersonalAutorizacion(codPersonalAutorizacion, nombre) {
    $("#CodPersonalAutorizacion").val(codPersonalAutorizacion);
    $("#NombrePersonalAutorizacion").val(nombre);
    document.getElementById("btnCerrarPersonalAutorizacion").click();
}

function VerReporteAutorizacion() {
    var id = get("cboTipoReporteAutorizacion");
    if (id == 1) {
        var codPersonal = get("CodPersonalAutorizacion");
        $("#visorReporteAutorizacion").attr("src", "ReporteAutorizacionPersonal/?codPersonal=" + codPersonal).load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 2) {
        $("#visorReporteAutorizacion").attr("src", "ReporteAutorizacionGeneral").load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 3) {
        var fechaInicio = get("FechaInicioAutorizacion");
        var fechaFin = get("FechaFinAutorizacion");
        $("#visorReporteAutorizacion").attr("src", "ReporteAutorizacionFecha/?fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin).load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 0) {
        Error("Seleccione un Tipo Reporte")
    }
}

function DescargarReporteAutorizacion() {
    var id = get("cboTipoReporteAutorizacion");
    if (id == 1) {
        var codPersonal = get("CodPersonalAutorizacion");
        window.open("ReporteAutorizacionPersonal/?codPersonal=" + codPersonal);
    }
    else if (id == 2) {
        window.open("ReporteAutorizacionGeneral");
    }
    else if (id == 3) {
        var fechaInicio = get("FechaInicioAutorizacion");
        var fechaFin = get("FechaFinAutorizacion");
        window.open("ReporteAutorizacionFecha/?fechaInicio=" + fechaInicio + "&fechaFin=" + fechaFin);
    }
    else if (id == 0) {
        Error("Seleccione un Tipo Reporte")
    }
}

function ChangeAutorizacion() {
    var id = get("cboTipoReporteAutorizacion");
    let fechaInicio = document.getElementById("FechaInicioAutorizacion");
    let fechaFin = document.getElementById("FechaFinAutorizacion");
    let btn_personal = document.getElementById("btnBuscarPersonalAutorizacion");
    if (id == 0) {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = true;
        $('#CodPersonalAutorizacion').val('');
        $('#FechaInicioAutorizacion').val('');
        $('#FechaFinAutorizacion').val('');
        $('#NombrePersonaAutorizacion').val('');
    }
    else if (id == 1) {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = false;
        $('#CodPersonalAutorizacion').val('');
        $('#FechaInicioAutorizacion').val('');
        $('#FechaFinAutorizacion').val('');
        $('#NombrePersonaAutorizacion').val('');
    }
    else if (id == 2) {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = true;
        $('#CodPersonalAutorizacion').val('');
        $('#FechaInicioAutorizacion').val('');
        $('#FechaFinAutorizacion').val('');
        $('#NombrePersonaAutorizacion').val('');
    }
    else if (id == 3) {
        fechaInicio.disabled = false;
        fechaFin.disabled = false;
        btn_personal.disabled = true;
        $('#CodPersonalAutorizacion').val('');
        $('#FechaInicioAutorizacion').val('');
        $('#FechaFinAutorizacion').val('');
        $('#NombrePersonaAutorizacion').val('');
    }
    else {
        fechaInicio.disabled = true;
        fechaFin.disabled = true;
        btn_personal.disabled = true;
        $('#CodPersonalAutorizacion').val('');
        $('#FechaInicioAutorizacion').val('');
        $('#FechaFinAutorizacion').val('');
        $('#NombrePersonaAutorizacion').val('');
    }
}

function BuscarPersonalVacaciones() {
    $(document).ready(function () {
        $("#divPersonalVacaciones").load("_ListarPersonalVacaciones")
    });
}

function SelectPersonalVacaciones(codPersonalVacaciones, nombre) {
    $("#CodPersonalVacaciones").val(codPersonalVacaciones);
    $("#NombrePersonalVacaciones").val(nombre);
    document.getElementById("btnCerrarPersonalVacaciones").click();
}

function VerReporteVacaciones() {
    var id = get("cboTipoReporteVacaciones");
    if (id == 1) {
        var codPersonal = get("CodPersonalVacaciones");
        $("#visorReporteVacaciones").attr("src", "ReporteVacacionesPersonal/?codPersonal=" + codPersonal).load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 2) {
        $("#visorReporteVacaciones").attr("src", "ReporteVacacionesGeneral").load;
        Correcto("Reporte cargado correctamente");
    }
    else if (id == 0) {
        Error("Seleccione un Tipo Reporte")
    }
}

function DescargarReporteVacaciones() {
    var id = get("cboTipoReporteVacaciones");
    if (id == 1) {
        var codPersonal = get("CodPersonalVacaciones");
        window.open("ReporteVacacionesPersonal/?codPersonal=" + codPersonal);
    }
    else if (id == 2) {
        window.open("ReporteVacacionesGeneral");
    }
    else if (id == 0) {
        Error("Seleccione un Tipo Reporte")
    }
}

function ChangeVacaciones() {
    var id = get("cboTipoReporteVacaciones");
    let btn_personal = document.getElementById("btnBuscarPersonalVacaciones");
    if (id == 0) {
        btn_personal.disabled = true;
        $('#CodPersonalVacaciones').val('');
        $('#NombrePersonalVacaciones').val('');
    }
    else if (id == 1) {
        btn_personal.disabled = false;
        $('#CodPersonalVacaciones').val('');
        $('#NombrePersonalVacaciones').val('');
    }
    else if (id == 2) {
        btn_personal.disabled = true;
        $('#CodPersonalVacaciones').val('');
        $('#NombrePersonalVacaciones').val('');
    }
    else {
        btn_personal.disabled = true;
        $('#CodPersonalVacaciones').val('');
        $('#NombrePersonalVacaciones').val('');
    }
}