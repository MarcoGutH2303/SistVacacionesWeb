function get(id) {
    return document.getElementById(id).value;
}

function Error(texto = "Ocurrio un error", contenidohtml = "") {
    if (texto != "") {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: texto,
            focusCancel: true
        })
    }
    else {
        Swal.fire({
            icon: 'error',
            title: 'Error',
            html: contenidohtml,
            focusCancel: true
        })
    }
}

function Correcto(texto = "Se realizo correctamente") {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: texto,
        showConfirmButton: false,
        timer: 1500
    })
}

function Confirmacion(title = "Confirmacion", texto = "¿Desea guardar los cambios?",
    callback) {
    return Swal.fire({
        title: title,
        text: texto,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Si',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            callback();
        }
    })
}

function Informacion(titulo = "Información Importante", contenido = "", contenidohtml = "") {
    if (contenido != "") {
        Swal.fire({
            icon: 'success',
            title: titulo,
            text: contenido,
            focusConfirm: true,
        })
    }
    else {
        Swal.fire({
            icon: 'success',
            title: titulo,
            html: contenidohtml,
            focusConfirm: true,
        })
    }
}

function InformacionImportante(titulo = "Información Importante", contenido = "", contenidohtml = "") {
    if (contenido != "") {
        Swal.fire({
            icon: 'info',
            title: titulo,
            text: contenido,
            focusConfirm: true
        })
    }
    else {
        Swal.fire({
            icon: 'info',
            title: titulo,
            html: contenidohtml,
            focusConfirm: true
        })
    }
}


function set(id, valor) {
    document.getElementById(id).value = valor;
}

function setD(id, valor) {
    document.getElementById(id).style.display = valor;
}

//No sirve por que los input radio tienen check
function setN(id, valor) {
    document.getElementsByName(id)[0].value = valor;
}

function getN(id) {
    return document.getElementsByName(id)[0].value;
}

function setC(selector) {
    document.querySelector(selector).checked = true;
}

function setSRC(id, valor) {
    document.getElementsByName(id)[0].src = valor;
}

function setURL(url) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    return urlAbsoluta;
}

var combosLlenar = [];
var radioLimpiar = [];
var radioNames = [];
function LimpiarDatos(idFormulario, excepciones = []) {
    var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    //Limpiar todo
    for (var j = 0; j < radioNames.length; j++) {
        document.querySelectorAll("[name=" + radioNames[j] + "]").forEach(x => x.checked = false);

    }
    for (var j = 0; j < radioLimpiar.length; j++) {
        document.getElementById(radioLimpiar[j]).checked = true;
    }
    var checkboxes = document.querySelectorAll("#" + idFormulario + " [type*='checkbox']")
    for (var j = 0; j < checkboxes.length; j++) {
        checkboxes[j].checked = false;
    }
    for (var i = 0; i < elementos.length; i++) {
        //Si esta incluido no se hace nada

        if (!excepciones.includes(elementos[i].name)) {
            if (elementos[i].tagName.toUpperCase() == "IMG") {
                elementos[i].src = "";
            } else {
                elementos[i].value = "";
            }
        }
    }
}

function ValidarLongitudMaxima(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='max-']")
    var control;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i];
        var arrayClase = control.className.split(" ");
        var claseMax = arrayClase.filter(p => p.includes("max-"))[0]
        var valorMaximo = claseMax.replace("max-", "") * 1;
        if (control.value.length > valorMaximo) {
            error = "La longitud actual del campo " + control.name + " es de " + control.value.length + ". El campo " + control.name + " no acepta mas de " + valorMaximo + " caracteres.";
            return error;
        }
    }
    return error;
}

function ValidarLongitudMinima(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='min-']")
    var control;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i];
        var arrayClase = control.className.split(" ");
        var claseMin = arrayClase.filter(p => p.includes("min-"))[0]
        var valorMinimo = claseMin.replace("min-", "") * 1;
        if (control.value.length < valorMinimo) {
            error = "La longitud actual del campo " + control.name + " es de " + control.value.length + "." + "El campo " + control.name + " no acepta menos de " + valorMinimo + " caracteres.";
            return error;
        }
    }
    return error;
}

function ValidarObligatorios(idFormulario) {
    //No hay error
    var error = "";
    var elementos = document.querySelectorAll("#" + idFormulario + " .o")
    var contenedorcheckbox = document.querySelectorAll("#" + idFormulario + " [class*='o-']")
    for (var j = 0; j < contenedorcheckbox.length; j++) {
        var contenedor = contenedorcheckbox[j];
        var arrayClase = contenedor.className.split(" ");
        var claseMaxima = arrayClase.filter(p => p.includes("o-"))[0];
        var minimoseleccionable = claseMaxima.replace("o-", "") * 1;
        var numeroMarcados = 0;
        var hijos = contenedor.children;
        var hijo;
        var nhijos = hijos.length;
        for (var h = 0; h < nhijos; h++) {
            hijo = hijos[h];
            if (hijo.type != undefined && (hijo.type.toUpperCase() == "CHECKBOX" || hijo.type.toUpperCase() == "RADIO")) {
                if (hijo.checked == true) {
                    numeroMarcados++;
                }
            }
        }
        if (minimoseleccionable > numeroMarcados) {
            if (minimoseleccionable == 1) {
                error = "Debe seleccionar al menos " + minimoseleccionable + " opción con un check."
                return error;
            }
            else if (minimoseleccionable > 1) {
                error = "Debe seleccionar al menos " + minimoseleccionable + " opciones con un check."
                return error;
            }
        }
    }
    for (var i = 0; i < elementos.length; i++) {
        //Si esta incluido no se hace nada
        if (elementos[i].tagName.toUpperCase() == "INPUT" && elementos[i].value == "") {
            var nombre = elementos[i].name;
            var la = elementos[i].classList.contains('la');
            var el = elementos[i].classList.contains('el');
            var condicional;
            if (la == true) {
                condicional = "la";
            }
            else if (el == true) {
                condicional = "el";
            }
            else {
                condicional = " ";
            }
            nombre = nombre.replace("_", " ");
            nombre = nombre.replace(/\b\w/g, l => l.toUpperCase());
            error = "Debe ingresar " + condicional + " " + nombre;
            return error;
        }
        else if (elementos[i].tagName.toUpperCase() == "SELECT") {
            if (elementos[i].value == '' || elementos[i].value == '.') {
                var nombre = elementos[i].name;
                var un = elementos[i].classList.contains('un');
                var una = elementos[i].classList.contains('una');
                var condicional;
                if (un == true) {
                    condicional = "un";
                }
                else if (una == true) {
                    condicional = "una";
                }
                else {
                    condicional = " ";
                }
                nombre = nombre.replace("_", " ");
                nombre = nombre.replace("_", " ");
                nombre = nombre.replace("id", "");
                nombre = nombre.replace(/\b\w/g, l => l.toUpperCase());
                error = "Debe seleccionar " + condicional + " " + nombre;
                return error;
            }
        }
        else if (elementos[i].tagName.toUpperCase() == "IMG" && elementos[i].src == window.location.href) {
            var nombre = elementos[i].name;
            nombre = nombre.replace("_", " ");
            nombre = nombre.replace(/\b\w/g, l => l.toUpperCase());
            error = "Debe ingresar el/la " + nombre.replace("base64", "").replace("data", "");
            return error;
        }
    }
    return error;
}

function ValidarSoloNumerosEnteros(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='snc']")
    var control;
    var caracter;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i];
        var valor = control.value;
        var longitud = valor.length;
        for (var e = 0; e < valor.length; e++) {
            caracter = valor[e];
            if (caracter != "0" && caracter != "1" && caracter != "2" && caracter != "3" && caracter != "4" && caracter != "5" && caracter != "6" && caracter != "7" && caracter != "8" && caracter != "9") {
                error = "El " + control.name + " solo debe tener numeros enteros.";
                return error;
            }
        }
    }
    return error;
}

function ValidarSoloNumerosDecimalesControl(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='sndc']")
    var control;
    var caracter;
    for (var i = 0; i < controles.length; i++) {
        control = controles[i];
        var valor = control.value;
        var longitud = valor.length;
        if (valor[0] == ".") {
            error = "El control " + control.name + " no puede iniciar con un punto.";
            return error;
        }
        if (valor[longitud - 1] == ".") {
            error = "El control " + control.name + " no puede terminar con un punto.";
            return error;
        }
        var numeroVeces = [...valor].filter(p => p.includes(".")).length;
        if (numeroVeces > 1) {
            error = "El control " + control.name + " solo debe haber un punto decimal.";
            return error;
        }
        for (var e = 0; e < valor.length; e++) {
            caracter = valor[e];
            if (caracter != "0" && caracter != "1" && caracter != "2" && caracter != "3" && caracter != "4" && caracter != "5" && caracter != "6" && caracter != "7" && caracter != "8" && caracter != "9" && caracter != ".") {
                error = "El control " + control.name + " solo debe tener numeros enteros.";
                return error;
            }
        }
    }
    return error;
}

function ValidarCorreo(idFormulario) {
    var error = "";
    var controles = document.querySelectorAll("#" + idFormulario + " [class*='crro']")
    var control;
    if (controles != undefined) {
        for (var i = 0; i < controles.length; i++) {
            control = controles[i];
            var valor = control.value;
            if (valor != '') {
                if (/^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i.test(valor)) {

                } else {
                    error = "La dirección email " + valor + " es incorrecta. Verifique la dirección email ingresado.";
                    return error;
                }
            }
        }
    }
    return error;
}

function fetchGet(url, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    //setD("cargando", "block");
    fetch(urlAbsoluta).then(res => res.json())
        .then(res => {
            //setD("cargando", "none");
            callback(res)
        }).catch(err => {
            //setD("cargando", "none");
            console.log(err)
        })

}

function fetchGetText(url, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    //setD("cargando", "block");
    fetch(urlAbsoluta).then(res => res.text())
        .then(res => {
            callback(res)
            //setD("cargando", "none");
        }).catch(err => {
            console.log(err)
            //setD("cargando", "none");
        })

}

function fetchPostText(url, frm, callback) {
    var raiz = document.getElementById("hdfOculto").value;
    var urlAbsoluta = window.location.protocol + "//" +
        window.location.host + raiz + url;
    //setD("cargando", "block");
    fetch(urlAbsoluta, {
        method: "POST",
        body: frm
    }).then(res => res.text())
        .then(res => {
            callback(res)
            //setD("cargando", "none");
        }).catch(err => {
            console.log(err)
            //setD("cargando", "none");
        })
    /*
    fetch(urlAbsoluta).then(res => res.json())
        .then(res => {
            callback(res)
        }).catch(err => {
            console.log(err)
        })
        */
}

function recuperarGenerico(url, idFormulario, excepciones = [], adicional = false) {
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
                    //RECUPERAMOS
                    var propiedad = nombreName.replace("[]", "");
                    var valores = res[propiedad];
                    var valor;
                    for (var k = 0; k < valores.length; k++) {
                        valor = valores[k];
                        setC("[type='checkbox'][value='" + valor + "']")
                    }
                }
                else {
                    if (elementos[i].type != undefined && elementos[i].type.toUpperCase() != "FILE") {
                        setN(nombreName, res[nombreName])
                    }
                    else if (elementos[i].tagName.toUpperCase() == "IMG") {
                        setSRC(nombreName, res[nombreName]);
                    }
                }

            }

        }
        if (adicional == true) {
            //objConfiguracionGlobal.callbackeditar(res);
            objConfiguracionGlobal.callbackeditar(res);
        }
    });
}

function recuperarGenericoEspecifico(url, idFormulario, excepciones = [], adicional = false) {
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
                    //RECUPERAMOS
                    var propiedad = nombreName.replace("[]", "");
                    var valor = res[propiedad];
                    //var valor;
                    //for (var k = 0; k < valores.length; k++) {
                    //    valor = valores[k];
                    //    setC("[type='checkbox'][value='" + valor + "']")
                    //}
                    if (valor == "1") {
                        document.getElementsByName(propiedad)[0].checked = true;
                    }
                    else {
                        document.getElementsByName(propiedad)[0].checked = false;
                    }
                }
                else {
                    if (elementos[i].type != undefined && elementos[i].type.toUpperCase() != "FILE") {
                        if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "DATE") {
                            var fecha = fixFecha(res[nombreName]);
                            setN(nombreName, fecha)
                        }
                        else if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "DATETIME-LOCAL") {
                            var fecha = fixFechaHora(res[nombreName]);
                            setN(nombreName, fecha)
                        }
                        else {
                            setN(nombreName, res[nombreName])
                        }
                    }
                    else if (elementos[i].tagName.toUpperCase() == "IMG") {
                        if (res[nombreName] != null) {
                            setSRC(nombreName, res[nombreName]);
                        }
                        else {
                            setSRC(nombreName, "");
                        }
                    }
                }
            }

        }
        if (adicional == true) {
            //objConfiguracionGlobal.callbackeditar(res);
            recuperarEspecifico(res);
        }
    });

    //var elementos = document.querySelectorAll("#" + idFormulario + " [name]")
    //var nombreName;
    //fetchGet(url, function (res) {
    //    for (var i = 0; i < elementos.length; i++) {
    //        nombreName = elementos[i].name
    //        if (!excepciones.includes(elementos[i].name)) {
    //            if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "RADIO") {
    //                setC("[type='radio'][name='" + nombreName + "'][value='" + res[nombreName] + "']")
    //            }
    //            else if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "CHECKBOX") {
    //                //RECUPERAMOS
    //                var propiedad = nombreName.replace("[]", "");
    //                var valores = res[propiedad];
    //                var valor;
    //                for (var k = 0; k < valores.length; k++) {
    //                    valor = valores[k];
    //                    setC("[type='checkbox'][value='" + valor + "']")
    //                }
    //            }
    //            else {
    //                if (elementos[i].type != undefined && elementos[i].type.toUpperCase() != "FILE") {
    //                    if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "DATE") {
    //                        var fecha = fixFecha(res[nombreName]);
    //                        setN(nombreName, fecha)
    //                    }
    //                    else if (elementos[i].type != undefined && elementos[i].type.toUpperCase() == "DATETIME-LOCAL") {
    //                        var fecha = fixFechaHora(res[nombreName]);
    //                        setN(nombreName, fecha)
    //                    }
    //                    else {
    //                        setN(nombreName, res[nombreName])
    //                    }
    //                }
    //                else if (elementos[i].tagName.toUpperCase() == "IMG") {
    //                    if (res[nombreName] != null) {
    //                        setSRC(nombreName, res[nombreName]);
    //                    }
    //                    else {
    //                        setSRC(nombreName, "");
    //                    }
    //                }
    //            }
    //        }

    //    }
    //    if (adicional == true) {
    //        //objConfiguracionGlobal.callbackeditar(res);
    //        recuperarEspecifico(res);
    //    }
    //});
}

function fixFecha(fecha) {
    if (fecha == null) {
        return '';
    }
    else {
        var fecha1 = moment.utc(fecha).format('YYYY-MM-DD');
        return fecha1;
    }
}

function fixFechaHora(fecha) {
    if (fecha == null) {
        return '';
    }
    else {
        var codigo_fecha = parseInt(fecha.replace("/Date(", "").replace(")/", ""));
        var fecha1 = new Date(codigo_fecha).toISOString();
        var fecha2 = moment(fecha1).format();
        var fecha3 = fecha2.substring(0, (fecha2.indexOf("T") | 0) + 6 | 0);
        //var fecha_formato = $.format.date(fecha1, "dd-MM-yyyy");

        return fecha3;
    }
}

function validarSoloNumeros(e) {
    var codigoAscii = e.keyCode;
    if (codigoAscii < 48 || codigoAscii > 57) {
        //No Mostrar
        e.preventDefault();
    }
}

function validarSoloNumerosDecimales(e) {
    var codigoAscii = e.keyCode;
    if ((codigoAscii < 48 && codigoAscii != 46) || codigoAscii > 57) {
        //No Mostrar
        e.preventDefault();
    }
    if (String.fromCharCode(e.keyCode) == ".") {
        if (e.target.value.includes(".")) e.preventDefault();
    }
    if (e.target.value.length == 0 && String.fromCharCode(e.keyCode) == ".") {
        e.preventDefault();
    }
}

function encontroClase(clase, claseBuscar = "snc") {
    var arrayClase = clase.split(" ");
    var numeroEncontrados = arrayClase.filter(p => p.includes(claseBuscar)).length;
    if (numeroEncontrados == 0) {
        return false;
    }
    else {
        return true;
    }
}

function previewImage(control, img) {
    var file = control.files[0];
    var imgfoto = document.getElementById(img);
    var reader = new FileReader();
    reader.onloadend = function () {
        imgfoto.src = reader.result;
    }
    reader.readAsDataURL(file)
}

function LimpiarGenerico(idFormulario) {
    LimpiarDatos(idFormulario, objFormularioGlobal.limpiarExcepciones.concat(radioNames))
}

function llenarCombo(data, id, propiedadMostrar, propiedadId, valueDefecto = "") {
    var contenido = ""
    var elemento;
    contenido += "<option selected='selected' value='" + valueDefecto + "'>--Seleccione--</option>"
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option value='" + elemento[propiedadId] + "' >" + elemento[propiedadMostrar] + "</option>"
    }
    contenido += "";
    document.getElementById(id).innerHTML = contenido;
}