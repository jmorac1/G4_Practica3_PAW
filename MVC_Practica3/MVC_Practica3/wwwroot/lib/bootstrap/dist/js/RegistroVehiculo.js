$(function () {

    $("#formRegistroVehiculo").validate({
        rules: {
            Marca: {
                required: true
            },
            Modelo: {
                required: true
            },
            Color: {
                required: true
            },
            Precio: {
                required: true,
                number: true,
                min: 0.01
            },
            IdVendedor: {
                required: true,
                digits: true,
                min: 1
            }
        },
        messages: {
            Marca: {
                required: "Campo obligatorio"
            },
            Modelo: {
                required: "Campo obligatorio"
            },
            Color: {
                required: "Campo obligatorio"
            },
            Precio: {
                required: "Campo obligatorio",
                number: "Debe ser un número válido",
                min: "Debe ser mayor a 0"
            },
            IdVendedor: {
                required: "Campo obligatorio",
                digits: "Seleccione un vendedor válido",
                min: "Seleccione un vendedor válido"
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