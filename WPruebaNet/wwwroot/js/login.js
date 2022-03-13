$(document).ready(function () {

    $("#btnIniciar").click(function () {
        AutenticarUsuario();
    });

    function AutenticarUsuario() {
        if (ValidaCajaTexto('txtUsuario')) {
            if (ValidaCajaTexto('txtPassword')) {

                swal.fire({
                    title: 'Un momento por favor',
                    html: 'Validando información del usuario.',
                    showConfirmButton: false,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    icon: 'info',
                    onBeforeOpen: function () {
                        Swal.showLoading();
                    }
                });

                var url = $("#hfUrlAtentificar").val();

                var jData = {};
                {
                    jData["usuario"] = $('#txtUsuario').val();
                    jData["contrasenia"] = $('#txtPassword').val();
                };

                $.ajaxSetup({ cache: false });

                $.ajax({
                    type: "POST",
                    url: url,
                    data: jData,
                    statusCode: {
                        200: function (data) {
                            window.location.href = $("#hfUrlInicio").val();
                        },
                        400: function () {
                            swal.fire({
                                title: "",
                                text: "El usuario o contraseña incorrecto \n Verifique su información.",
                                icon: "error",
                                confirmButtonText: "Aceptar",
                            });
                        },
                        203: function (data) {
                            swal.fire({
                                title: "",
                                text: "La contraseña es incorrecta \n Verifique su información.",
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
    }
});