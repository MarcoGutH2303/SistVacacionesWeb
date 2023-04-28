window.onload = function () {
    ListarVacacionesPeriodo();
}

function ListarVacacionesPeriodo() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divVacacionesPeriodo").load("_ListarVacacionesPeriodoPersonal")
        });
    }, 500);
}

function VerConsumo(codVacacionesPeriodo) {
    ListarVacacionesConsumo(codVacacionesPeriodo);
}

function ListarVacacionesConsumo(codVacacionesPeriodo) {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divVacacionesConsumo").load("_ListarVacacionesConsumoPersonal/?codVacacionesPeriodo=" + codVacacionesPeriodo)
        });
    }, 500);
}