window.onload = function () {
    Llenado();
}

function Llenado() {
    fetchGet("PanelControl/PanelControlEmpleado", function (data) {
        document.querySelector("#labelSolicitudPendiente").innerText = data.CantSolicitudPendiente;
        document.querySelector("#labelSolicitudResuelto").innerText = data.CantAutorizacionRealizado;
        document.querySelector("#labelVacacionesPeriodo").innerText = data.CantVacacionesPeriodo;
    });
}