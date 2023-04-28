window.onload = function () {
    Llenado();
}

function Llenado() {
    fetchGet("PanelControl/PanelControlAdministrador", function (data) {
        document.querySelector("#labelSolicitudPendiente").innerText = data.CantSolicitudPendiente;
        document.querySelector("#labelSolicitudResuelto").innerText = data.CantSolicitudResuelto;
        document.querySelector("#labelAutorizacionRealizado").innerText = data.CantAutorizacionRealizado;
        document.querySelector("#labelVacacionesPeriodo").innerText = data.CantVacacionesPeriodo;
    });
}