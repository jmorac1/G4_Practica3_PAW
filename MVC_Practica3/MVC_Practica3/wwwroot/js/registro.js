let saldoActual = 0;

const apiUrl = 'https://localhost:7002/api/Home';

$(document).ready(function () {
    $.ajax({
        url: apiUrl + '/ComprasPendientes',
        type: 'GET',
        success: function (data) {
            $.each(data, function (i, compra) {
                $('#Id_Compra').append(
                    $('<option>', {
                        value: compra.id_Compra,
                        text: compra.descripcion
                    })
                );
            });
        },
        error: function () {
            alert('Error al cargar las compras pendientes.');
        }
    });
});

$('#Id_Compra').on('change', function () {
    var id = $(this).val();

    if (!id) {
        saldoActual = 0;
        $('#SaldoAnterior').val('');
        $('#Monto').val('');
        $('#errorAbono').addClass('d-none');
        return;
    }

    $.ajax({
        url: apiUrl + '/SaldoCompra/' + id,
        type: 'GET',
        success: function (data) {
            saldoActual = parseFloat(data.saldo);
            $('#SaldoAnterior').val(saldoActual.toFixed(2));
            $('#Monto').val('');
            $('#errorAbono').addClass('d-none');
        },
        error: function () {
            alert('Error al obtener el saldo de la compra seleccionada.');
        }
    });
});

$('#Monto').on('input', function () {
    var monto = parseFloat($(this).val());
    if (!isNaN(monto) && monto > saldoActual) {
        $('#errorAbono').removeClass('d-none');
    } else {
        $('#errorAbono').addClass('d-none');
    }
});

$('#formAbono').on('submit', function (e) {
    var id = $('#Id_Compra').val();
    var monto = parseFloat($('#Monto').val());

    // Verificar que haya compra seleccionada
    if (!id) {
        e.preventDefault();
        alert('Debe seleccionar una compra.');
        return;
    }

    // Verificar monto válido
    if (isNaN(monto) || monto <= 0) {
        e.preventDefault();
        alert('El abono debe ser un monto mayor a cero.');
        return;
    }

    // Verificar que el abono no supere el saldo
    if (monto > saldoActual) {
        e.preventDefault();
        $('#errorAbono').removeClass('d-none');
        return;
    }

});