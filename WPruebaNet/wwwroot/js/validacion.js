function ValidaCorreoElectronico(txtTexto) {
    var validate = false;
    var erCorreo = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
    if ($("#" + txtTexto).val().match(erCorreo)) {
        validate = true;
        $("#" + txtTexto).removeClass("validate-error");
    }
    else {
        $("#" + txtTexto).addClass("validate-error");
        validate = false;
    }
    return validate;
}
function ValidaCajaTexto(txtTexto) {
    var validate = false;
    if ($("#" + txtTexto).val() != "" && $("#" + txtTexto).val() != null && $("#" + txtTexto).val() != undefined) {
        validate = true;
        $("#" + txtTexto).removeClass("validate-error");
    }
    else {
        $("#" + txtTexto).addClass("validate-error");
        validate = false;
    }
    return validate;
}

function isNullEmpty(txtTexto) {
    var validate = false;
    if ($("#" + txtTexto).val() === "" || $("#" + txtTexto).val() === null || $("#" + txtTexto).val() === undefined) {
        validate = true;
    }
    else {
        validate = false;
    }
    return validate;
}

function isNullEmptyDato(dato) {
    var validate = false;
    if (dato === null || dato === undefined || dato==="") {
        validate = true;
    }
    else {
        validate = false;
    }
    return validate;
}

function ValidaMultiplesCorreoElectronico(txtTexto) {
    var validate = false;
    //var erCorreo = "((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)(\s*,\s*|\s*$))*";
    var erCorreo = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
    var lsCorreos = $("#" + txtTexto).val().split(",");
    console.log(lsCorreos);
    if (lsCorreos.length > 0) {
        for (let element of lsCorreos) {
            if (element.match(erCorreo))
                validate = true;
            else {
                validate = false;
                break;
            }
        }
    }
    if (validate)
        $("#" + txtTexto).removeClass("validate-error");
    else 
        $("#" + txtTexto).addClass("validate-error");
    return validate;
}



function ValidaControlFile(nombreControl, extencionesValidas) {
    var validate = false;
    var archivoPath = $("#" + nombreControl).val();
    var archivoExtension = archivoPath.substring(archivoPath.lastIndexOf(".") + 1, archivoPath.length).toLowerCase();
    if (archivoPath.length === 0) {
        $("#" + nombreControl).addClass("validate-error");
        validate = false;
    }
    var arrayExts = extencionesValidas.toLowerCase().split(",");
    if (arrayExts.includes("." + archivoExtension)) {
        validate = true;
        $("#" + nombreControl).removeClass("validate-error");
    } else {
        $("#" + nombreControl).addClass("validate-error");
        validate = false;
    }
    return validate;
}

function ValidarCombo(cboControl, valorDiferente) {
    var validate = false;
    if ($("#" + cboControl + " option:selected").attr('value') != valorDiferente) {
        validate = true;
        $("#" + cboControl).removeClass("validate-error");
    }
    else {
        $("#" + cboControl).addClass("validate-error");
        validate = false;
    }
    return validate;
}

function ValidaNombreFile(nombreFile, extencionesValidas) {
    var validate = false;
    var archivoExtension = nombreFile.substring(nombreFile.lastIndexOf(".") + 1, nombreFile.length).toLowerCase();
    var arrayExts = extencionesValidas.toLowerCase().split(",");
    if (arrayExts.includes("." + archivoExtension)) {
        validate = true;
    } else {
        $("#" + nombreControl).addClass("validate-error");
    }
    return validate;
}

function ValidaCajaTamanio(txtTexto,min) {
    var validate = false;
    if ($("#" + txtTexto).val().length >= min) {
        validate = true;
        $("#" + txtTexto).removeClass("validate-error");
    }
    else {
        $("#" + txtTexto).addClass("validate-error");
        validate = false;
    }
    return validate;
}

function ValidaConfirmacion(txtTexto, txtTextoCompara) {
    var validate = false;
    if ($("#" + txtTexto).val() === $("#" + txtTextoCompara).val()) {
        validate = true;
        $("#" + txtTexto).removeClass("validate-error");
    }
    else {
        $("#" + txtTexto).addClass("validate-error");
        validate = false;
    }
    return validate;
}

function ValidaPassword(txtTexto) {
    var myregex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{9,50}[^'\s]/;
    if (myregex.test($("#" + txtTexto).val())) {
        $("#" + txtTexto).removeClass("validate-error");
        validate = true;
    } else {
        $("#" + txtTexto).addClass("validate-error");
        validate = false;     
    }
    return validate;
}

function ValidaGpoRadio(nameGpo) {
    if ($('input[name="' + nameGpo+'"]').is(':checked')) {
        $('input[name="' + nameGpo + '"]').removeClass("validate-error-chk");
        validate = true;
    } else {
        $('input[name="' + nameGpo + '"]').addClass("validate-error-chk");
        validate = false;
    }
    return validate;
}