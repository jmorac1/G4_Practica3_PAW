$(function () {

    $("#formRegistroVendedor").validate({
        rules: {
            Cedula: {
                required: true
            },
            Nombre: {
                required: true
            },
            Correo: {
                required: true,
                email: true
            }
        },
        messages: {
            Cedula: {
                required: "Campo obligatorio"
            },
            Nombre: {
                required: "Campo obligatorio"
            },
            Correo: {
                required: "Campo obligatorio",
                email: "Formato incorrecto"
            }
        },
        errorElement: "span",
        errorClass: "text-danger",   
        highlight: function (element) {
            $(element).addClass("is-invalid");
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid");
        }
    });

});