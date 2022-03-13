$(document).ready(function () {

    $("#btnGuardar").click(function () {
        GuardaRegistro();
    });

    function GuardaRegistro() {
        if (ValidaCajaTexto('txtCorreo')) {
            if (ValidaCorreoElectronico('txtCorreo')) {
                if (ValidaCajaTexto('txtUsuario')) {
                    if (ValidaCajaTamanio('txtUsuario',7)) {
                        if (ValidaCajaTexto('txtPassword')) {
                            if (ValidaCajaTexto('txtPasswordCon')) {
                                if (ValidaConfirmacion('txtPasswordCon', 'txtPassword')) {
                                    if (ValidaPassword('txtPassword')) {

                                        if (ValidaGpoRadio('rbGpoSexo')) {
                                            swal.fire({
                                                title: 'Un momento por favor',
                                                html: 'Guardando información.',
                                                showConfirmButton: false,
                                                allowOutsideClick: false,
                                                allowEscapeKey: false,
                                                icon: 'info',
                                                onBeforeOpen: function () {
                                                    Swal.showLoading();
                                                }
                                            });

                                            var url = $("#hdUrlAddUsr").val();

                                            var jData = {};
                                            {
                                                jData["correo"] = $('#txtCorreo').val();
                                                jData["usuario"] = $('#txtUsuario').val();
                                                jData["contrasenia"] = $('#txtPassword').val();
                                                jData["sexo"] = $('input:radio[name=rbGpoSexo]:checked').val();
                                            };

                                            $.ajaxSetup({ cache: false });

                                            $.ajax({
                                                type: "PUT",
                                                url: url,
                                                data: jData,
                                                statusCode: {
                                                    200: function (data) {
                                                        swal.fire({
                                                            title: "",
                                                            text: "El usuario se registró correctamente.",
                                                            icon: "success",
                                                            confirmButtonText: "Aceptar",
                                                        }).then((result) => {
                                                            if (result.value) {
                                                                window.location = $("#hdUrlLogin").val();
                                                            }
                                                        });
                                                    },
                                                    204: function () {
                                                        swal.fire({
                                                            title: "",
                                                            text: "El correo ya se encuentra registrado \n Intente con otro correo.",
                                                            icon: "error",
                                                            confirmButtonText: "Aceptar",
                                                        });
                                                    },
                                                    409: function (data) {
                                                        swal.fire({
                                                            title: "",
                                                            text: "El usuario que intenta registrar ya existe \n Intente con otro usuario.",
                                                            icon: "error",
                                                            confirmButtonText: "Aceptar",
                                                        });
                                                    },
                                                    500: function () {
                                                        swal.fire({
                                                            title: "",
                                                            text: "Ocurrió un al registrar la información \n Intente mas tarde.",
                                                            icon: "error",
                                                            confirmButtonText: "Aceptar",
                                                        });
                                                    }
                                                }
                                            }).fail(function (data) {
                                                swal.close();
                                            });
                                        }
                                    }
                                    else {
                                        swal.fire({
                                            title: "",
                                            html: "<p>Estimado usuario la contraseña debe tener una longitud mínima de 10 caracteres los cuales debe contener los siguientes campos:</p><ul><li>mayúscula</li><li>minúscula</li><li>símbolo</li><li>numero</li></ul>",
                                            icon: "info",
                                            button: "Aceptar",
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
});