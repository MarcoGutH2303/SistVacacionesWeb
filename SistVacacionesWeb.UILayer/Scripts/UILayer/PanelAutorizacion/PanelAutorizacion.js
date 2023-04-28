window.onload = function () {
    ListarAutorizacion();
}

function ListarAutorizacion() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divPanelAutorizacion").load("_ListarAutorizacionPersonal")
        });
    }, 500);
}

function Ver(codAutorizacion) {
    LimpiarDetalle();
    document.getElementById("staticBackdropLabel").innerHTML = "Detalle Autorización";
    fetchGet("PanelAutorizacion/RecuperarAutorizacionPersonal/?codAutorizacion=" + codAutorizacion, function (data) {
        $("#CodAutorizacion").val(data["CodAutorizacion"]);
        $("#CodSolicitud").val(data["CodSolicitud"]);
        $("#NombreConcepto").val(data["NombreConcepto"]);
        var f_auto = ConvertirFecha(data["FechaAutorizacion"]);
        $("#FechaAutorizacion").val(f_auto);
        var f_sal = ConvertirFechaHora(data["FechaSalida"]);
        $("#FechaSalida").val(f_sal);
        var f_ret = ConvertirFechaHora(data["FechaRetorno"]);
        $("#FechaRetorno").val(f_ret);
        $("#TiempoCompleto").val(data["TiempoCompleto"]);
        $("#NombreCompletoAutorizante").val(data["NombreCompletoAutorizante"]);
        if (data["EstadoAutorizacion"] == "1") {
            $("#Estado").val("Aprobado");
        }
        else if (data["EstadoAutorizacion"] == "2") {
            $("#Estado").val("Denegado");
        }
        else if (data["EstadoAutorizacion"] == "3") {
            $("#Estado").val("Anulado");
        }
    })
}

function ConvertirFecha(fecha) {
    if (fecha == null) {
        return '';
    }
    else {
        var codigo_fecha = parseInt(fecha.replace("/Date(", "").replace(")/", ""));
        var fecha1 = new Date(codigo_fecha).toLocaleDateString("en-GB");
        return fecha1;
    }
}

function ConvertirFechaHora(fecha) {
    if (fecha == null) {
        return '';
    }
    else {
        var codigo_fecha = parseInt(fecha.replace("/Date(", "").replace(")/", ""));
        var fecha1 = new Date(codigo_fecha).toLocaleString("en-GB");
        return fecha1;
    }
}

function LimpiarDetalle() {
    $("#CodAutorizacion").val("");
    $("#CodSolicitud").val("");
    $("#FechaAutorizacion").val("");
    $("#NombreConcepto").val("");
    $("#FechaSalida").val("");
    $("#FechaRetorno").val("");
    $("#TiempoCompleto").val("");
    $("#NombrePersonalAutorizante").val("");
    $("#Estado").val("");
}