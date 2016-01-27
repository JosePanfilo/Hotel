//Se asocian las llamadas a eventos ocurridos en la vista
$('a[data-target="#modalDetalles"]').click(function () {
    var noMatricula = $(this).attr("huespedID");
    $.ajax({
        url: "/Huesped/AjaxDetails",
        contentType: "application/html; charset=utf-8",
        type: "GET",
        dataType: "html",
    })
    .success(function (result) {
        huesped_llenarFormaDetalles(result);
    })
    .error(function (xhr, status) {
        notificarError(status);
    })
});

//Se definen las funciones para mostrar resultados de transacciones
function huesped_llenarFormaDetalles(result) {
    $("input#huespedID").val(result.noMatricula);
}

function notificarError(status) {
    alert(status)
}