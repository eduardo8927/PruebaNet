
$(document).ready(function () {
    CargaGridUsuario(function () {
        $('#gvUsuario').DataTable();
    });

    $('#gvUsuario').on('click', '.delete-usr', function () {
        Swal.fire({
            icon: "question",
            title: "¿Estas seguro de eliminar el usuario?",
            confirmButtonText: "Aceptar",
            confirmButtonColor: 'green',
            showCancelButton: true,
            cancelButtonText: "Cancelar",
            cancelButtonColor: "red",
        }).then(resultado => {
            if (resultado.isConfirmed) {
                EliminarUsuario(parseInt($(this).data("id")));;
            }
        });
    });

    $('#gvUsuario').on('click', '.edit-usr', function () {
        console.log("Entra");
        CargaInformacionUsuario(parseInt($(this).data("id")),function () {
            $('#mpEditUsr').modal();
        });
    });

    $('#cboEstatus').change(function () {
        CargaGridUsuario(function () {
            $('#gvUsuario').DataTable();
        });
    });

    $('#btnGuardar').click(function () {
        ActualizarUsuario();
    });


    function EliminarUsuario(idUsuario) {
        swal.fire({
            title: 'Un momento por favor',
            html: 'Eliminado información.',
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false,
            icon: 'info',
            onBeforeOpen: function () {
                Swal.showLoading();
            }
        });

        var url = $("#hfUrlEliminarUsuario").val();

        var jData = {};
        {
            jData["idUsario"] = idUsuario;
        };

        $.ajaxSetup({ cache: false });

        $.ajax({
            type: "DELETE",
            url: url,
            data: jData,
            statusCode: {
                200: function (data) {
                    swal.fire({
                        title: "",
                        text: "El usuario se eliminó correctamente.",
                        icon: "success",
                        confirmButtonText: "Aceptar",
                    });
                },
                404: function () {
                    swal.fire({
                        title: "",
                        text: "El usuario que intenta eliminar no existe.",
                        icon: "error",
                        confirmButtonText: "Aceptar",
                    });
                },
                409: function (data) {
                    swal.fire({
                        title: "",
                        text: "El usuario ya se encuentra eliminado.",
                        icon: "error",
                        confirmButtonText: "Aceptar",
                    });
                },
                500: function () {
                    swal.fire({
                        title: "",
                        text: "Ocurrió un al eliminar el usuario.",
                        icon: "error",
                        confirmButtonText: "Aceptar",
                    });
                }
            }
        }).fail(function (data) {
            swal.close();
        });

    }

    function CargaGridUsuario(callback) {

        swal.fire({
            title: 'Un momento por favor',
            html: 'Cargando información.',
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false,
            icon: 'info',
            onBeforeOpen: function () {
                Swal.showLoading();
            }
        });
        $('#gvUsuario tbody').empty();
        var url = $("#hfUrlGetUsuario").val();
        var jData = {};
        {
            jData["estatus"] = $('#cboEstatus option:selected').attr('value');
        };
        $.ajaxSetup({ cache: false });
        $.ajax({
            type: "POST",
            url: url,
            data: jData,
        }).done(function (data) {
            swal.close();
            console.log(data);
            if (data.length > 0) {
                $.each(data, function (index, element) {
                    if (element.bitEstatus)
                        $('#gvUsuario tbody').append("<tr><td>" + element.id + "</td><td>" + element.usuario + "</td ><td>" + element.correo + "</td><td>" + element.sexo + "</td><td>" + element.estatus + "</td><td><i class='fa-solid fa-trash-can font-25 text-danger delete-usr' data-id='" + element.id + "'></i><i class='fa-solid fa-pencil font-25 text-primary edit-usr ml-3' data-id='" + element.id +"'></i>  </td></tr>");
                    else
                        $('#gvUsuario tbody').append("<tr><td>" + element.id + "</td><td>" + element.usuario + "</td ><td>" + element.correo + "</td><td>" + element.sexo + "</td><td>" + element.estatus + "</td><td></td></tr>");
                });
            } else {
                $('#gvUsuario tbody').append("<tr><td colspan='6'>No existe informacion</td></tr>");
            }
            if (typeof callback === 'function') {
                callback();
            }
        }).fail(function (data) {
            swal.fire({
                title: "Error en el servidor",
                text: "Ha ocurrido un error en el servidor, intente más tarde.",
                icon: "error",
                confirmButtonText: "Aceptar",
            });
        });

    }

    function LimiarControlesEdicio() {
        $("#hfIdUsuario").val("");
        $("#txtCorreo").val("");
        $("#txtUsuario").val("");
        $("#txtPassword").val("");
        $("#txtPasswordCon").val("");
    }

    function CargaInformacionUsuario(idUsuario,callback) {
        swal.fire({
            title: 'Un momento por favor',
            html: 'Cargando información.',
            showConfirmButton: false,
            allowOutsideClick: false,
            allowEscapeKey: false,
            icon: 'info',
            target: document.getElementById('pnlUpdateUsuario'),
            onBeforeOpen: function () {
                Swal.showLoading();
            }          
        });

        LimiarControlesEdicio();

        var url = $("#hfUrlGetUsuarioId").val();

        var jData = {};
        {
            jData["idUsuario"] = idUsuario;
        };

        $.ajaxSetup({ cache: false });

        $.ajax({
            type: "POST",
            url: url,
            data: jData,           
        }).done(function (data) {         
            if (!isNullEmptyDato(data)) {
                $("#hfIdUsuario").val(data.intIdusuario);
                $("#txtCorreo").val(data.strCorreo);
                $("#txtUsuario").val(data.strUsuario);
                $("#txtPassword").val("");
                $("#txtPasswordCon").val("");
            }
            swal.close();
            if (typeof callback === 'function') {
                callback();
            }
        }).fail(function (data) {
            swal.fire({
                title: "Error en el servidor",
                text: "Ha ocurrido un error en el servidor, intente más tarde.",
                icon: "error",
                confirmButtonText: "Aceptar",
            });
        });
    }

    function ActualizarUsuario() {
        if (ValidaCajaTexto('txtCorreo')) {
            if (ValidaCorreoElectronico('txtCorreo')) {
                if (ValidaCajaTexto('txtUsuario')) {
                    if (ValidaCajaTamanio('txtUsuario', 7)) {
                        if (ValidaCajaTexto('txtPassword')) {
                            if (ValidaCajaTexto('txtPasswordCon')) {
                                if (ValidaConfirmacion('txtPasswordCon', 'txtPassword')) {
                                    if (ValidaPassword('txtPassword')) {
                                        swal.fire({
                                            title: 'Un momento por favor',
                                            html: 'Actualizando información.',
                                            showConfirmButton: false,
                                            allowOutsideClick: false,
                                            allowEscapeKey: false,
                                            icon: 'info',
                                            onBeforeOpen: function () {
                                                Swal.showLoading();
                                            }
                                        });

                                        var url = $("#hfUrlActualizarUsuario").val();

                                        var jData = {};
                                        {
                                            jData["idUsuario"] = $('#hfIdUsuario').val();
                                            jData["correo"] = $('#txtCorreo').val();
                                            jData["usuario"] = $('#txtUsuario').val();
                                            jData["contrasenia"] = $('#txtPassword').val();
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
                                                        text: "El usuario se actualizó correctamente.",
                                                        icon: "success",
                                                        confirmButtonText: "Aceptar",
                                                    }).then((result) => {
                                                        if (result.value) {
                                                            CargaGridUsuario();
                                                            $('#mpEditUsr').modal('hide');
                                                        }
                                                    });
                                                },
                                                204: function () {
                                                    swal.fire({
                                                        title: "",
                                                        text: "El usuario que intenta actualizar ya se no se encuentra.",
                                                        icon: "error",
                                                        confirmButtonText: "Aceptar",
                                                    });
                                                },
                                                400: function (data) {
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
                                                        text: "Ocurrió un al actualizar la información \n Intente mas tarde.",
                                                        icon: "error",
                                                        confirmButtonText: "Aceptar",
                                                    });
                                                }
                                            }
                                        }).fail(function (data) {
                                            swal.close();
                                        });
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