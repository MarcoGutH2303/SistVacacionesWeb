window.onload = function () {
    ListarUsuario();
    PreviewImagen();
}

function ListarUsuario() {
    $("#progress").show();
    setTimeout(function () {
        $("#progress").hide();
        $(document).ready(function () {
            $("#divUsuario").load("_ListarUsuario")
        });
    }, 500);
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

function PreviewImagen() {
    var fupFoto = document.getElementById("fupFoto");
    fupFoto.onchange = function () {
        var file = fupFoto.files[0];
        var reader = new FileReader();
        reader.onloadend = function () {
            document.getElementById("imgFoto").src = reader.result;
        }
        reader.readAsDataURL(file);
    }
}

function BuscarPersonal() {
    $('#staticBackdrop1').modal('show');
    ListarPersonal();
}

function SelectPersonal(codPersonal, nombrePersonal, apellidoPersonal, nombreTipoDocumentoPersonal, numeroDocumentoPersonal) {
    document.getElementById("btnCerrarPersonal").click();
    $("#CodPersonal").val(codPersonal);
    $("#NombrePersonal").val(nombrePersonal);
    $("#ApellidoPersonal").val(apellidoPersonal);
    $("#NombreTipoDocumentoPersonal").val(nombreTipoDocumentoPersonal);
    $("#NumeroDocumentoPersonal").val(numeroDocumentoPersonal);
}

function LimpiarPersonal() {
    $("#CodPersonal").val("");
    $("#NombrePersonal").val("");
    $("#ApellidoPersonal").val("");
    $("#NombreTipoDocumentoPersonal").val("");
    $("#NumeroDocumentoPersonal").val("");
}

function ocRol(rol) {
    if (rol == "")
        var rol = get("cboRol");
    if (rol == "1") {
        var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
        for (i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = false;
            checkboxes[i].disabled = true;
        }
    }
    else if (rol == "2") {
        var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
        for (i = 0; i < checkboxes.length; i++)
            checkboxes[i].disabled = false;
    }
    else {
        var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
        for (i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = false;
            checkboxes[i].disabled = true;
        }
    }
}

function SeleccionarTodo() {
    if ($('#checkgeneral').prop('checked')) {
        var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
        for (i = 0; i < checkboxes.length; i++)
            checkboxes[i].checked = true;
    }
    else {
        var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
        for (i = 0; i < checkboxes.length; i++)
            checkboxes[i].checked = false;
    }
}

function NuevoUsuario() {
    LimpiarUsuario();
    document.getElementById("staticBackdropLabel").innerHTML = "Nueva Usuario";
    var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
    for (i = 0; i < checkboxes.length; i++)
        checkboxes[i].disabled = true;
}

function GuardarUsuario() {
    var valida = ValidarCampos();
    if (valida == false) {
        return;
    }

    var error = ValidarSoloNumerosEnteros("frmUsuario");
    if (error != "") {
        Error(error);
        return;
    }

    var error = ValidarLongitudMaxima("frmUsuario");
    if (error != "") {
        Error(error);
        return;
    }

    var frmUsuario = document.getElementById("frmUsuario");
    var frm = new FormData(frmUsuario);
    var valor = $("#CodUsuario").val();
    if (valor == undefined || valor == '') {
        Confirmacion(undefined, "¿Desea guardar el Usuario?", function () {
            fetchPostText("Usuario/GrabarUsuario", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarUsuario").click();
                    ListarUsuario();
                    LimpiarUsuario();
                    Correcto("Se ha guardado correctamente.");
                }
                else if (res == "2") {
                    ListarUsuario();
                    Error("Ya se le ha creado un Usuario a este Personal.");
                }
                else if (res == "3") {
                    ListarUsuario();
                    Error("El Usuario ya esta siendo usado por otro Personal, ingrese otro Usuario.");
                }
                else {
                    ListarUsuario();
                    Error("Hubo un error al guardar el Usuario.");
                }
            })
        })
    }
    else {
        Confirmacion(undefined, "¿Desea guardar los cambios del Usuario?", function () {
            fetchPostText("Usuario/GrabarUsuario", frm, function (res) {
                if (res == "1") {
                    document.getElementById("btnCerrarUsuario").click();
                    ListarUsuario();
                    LimpiarUsuario();
                    Correcto("Se ha guardado correctamente.")
                }
                else if (res == "2") {
                    ListarUsuario();
                    Error("Ya se le ha creado un Usuario a este Personal.");
                }
                else if (res == "3") {
                    ListarUsuario();
                    Error("El Usuario ya esta siendo usado por otro Personal, ingrese otro Usuario.");
                }
                else {
                    ListarUsuario();
                    Error("Hubo un error al guardar los cambios del Usuario.");
                }
            })
        })
    }
}

function LimpiarUsuario() {
    LimpiarDatos("frmUsuario", ["Empresa", "Mantenimiento", "Personal", "Concepto", "Autorizacion", "Vacaciones", "Reporte", "User"]);
}

function EditarUsuario(CodUsuario, datoRol) {
    LimpiarUsuario();
    if (CodUsuario != "" && CodUsuario != undefined) {
        ocRol(datoRol);
        document.getElementById("staticBackdropLabel").innerHTML = "Editar Usuario";
        LLenarDatosUsuario("Usuario/RecuperarUsuario/?codUsuario=" + CodUsuario,
            "frmUsuario", [], false);
        LLenarDatosRol("Usuario/RecuperarRol/?codUsuario=" + CodUsuario,
            "frmUsuario", [], false, datoRol);
    }
}

function LLenarDatosUsuario(url, idFormulario, excepciones = [], adicional = false) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    var nombreName;
    fetchGet(url, function (res) {
        for (var i = 0; i < elementos.length; i++) {
            nombreName = elementos[i].name
            if (!excepciones.includes(elementos[i].name)) {
                if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "RADIO") {
                    setC("[type='radio'][name='" + nombreName + "'][value='" + res[nombreName] + "']")
                }
                else if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "CHECKBOX") {

                }
                else {
                    if (elementos[i].type != undefined && elementos[i].type.toUpperCase() != "FILE") {
                        if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "DATE") {
                            var fecha = fixFecha(res[nombreName]);
                            setN(nombreName, fecha)
                        }
                        else {
                            setN(nombreName, res[nombreName])
                        }
                    }
                    else if (elementos[i].tagName.toUpperCase() == "IMG") {
                        setSRC(nombreName, res[nombreName]);
                    }
                }
            }
        }
        if (adicional == true) {
            //objConfiguracionGlobal.callbackeditar(res);
            recuperarEspecifico(res);
        }
    })
}

function LLenarDatosRol(url, idFormulario, excepciones = [], adicional = false, datoRol) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    var nombreName;
    var modulos = ["General", "Empresa", "Mantenimiento", "Concepto", "Personal", "Autorizacion", "Vacaciones", "Reporte", "User"]
    fetchGet(url, function (data) {
        var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
        for (i = 0; i < checkboxes.length; i++) {
            if (modulos[i] == "General") {
                checkboxes[i].checked = false;
            }
            else {
                if (datoRol == "2") {
                    if (data[modulos[i]] == 1) {
                        checkboxes[i].checked = true;
                    }
                    else {
                        checkboxes[i].checked = false;
                    }
                }
                else if (datoRol == "1") {
                    var checkboxes = document.querySelectorAll("#frmUsuario [type*='checkbox']");
                    for (i = 0; i < checkboxes.length; i++) {
                        checkboxes[i].checked = false;
                        checkboxes[i].disabled = true;
                    }
                }
            }
        }

    })
}

function EliminarUsuario(CodUsuario) {
    Confirmacion("¿Desea eliminar el Usuario?", "Confirmar eliminación", function (res) {
        fetchGetText("Usuario/EliminarUsuarioLogico/?CodUsuario=" + CodUsuario, function (rpta) {
            if (rpta == "1") {
                ListarUsuario();
                Correcto("Se elimino correctamente");
            }
            else {
                ListarUsuario();
                Error("Hubo un error al eliminar el Usuario.");
            }
        })
    })
}

function EnviarCredencialesEmail(CodUsuario) {
    Confirmacion("¿Desea enviar las credenciales al correo electronico del Personal?", "Confirmar envio", function (res) {
        fetchGetText("Usuario/EnviarCredencialesCorreo/?codUsuario=" + CodUsuario, function (rpta) {
            if (rpta == "1") {
                Correcto("Se envio las credenciales correctamente");
            }
            else {
                Error("Hubo un error al enviar las credenciales");
            }
        })
    })
}

function ValidarCampos() {
    let codPersonal = getN("CodPersonal");
    let usuario = getN("Usuario");
    let pass = getN("Pass");
    let rol = getN("Rol");
    let estado = getN("Estado");
    if (codPersonal != "" && codPersonal != undefined) {
        if (usuario != "" && usuario != undefined) {
            if (pass != "" && pass != undefined) {
                if (rol != "" && rol != undefined) {
                    if (estado != "" && estado != undefined) {
                        return true;
                    }
                    else {
                        Error("Seleccione el Estado.");
                        return false;
                    }
                }
                else {
                    Error("Seleccione el Rol.");
                    return false;
                }
            }
            else {
                Error("Ingrese la Contraseña.");
                return false;
            }
        }
        else {
            Error("Ingrese el Usuario.");
            return false;
        }
    }
    else {
        Error("Busque y seleccione el Personal.");
        return false;
    }
}