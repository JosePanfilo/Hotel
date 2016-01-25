$().ready(function () {

    $("a#enlaceEditar").click(function () {
        //Se obtiene el numero  a consultar
        var enlaceClickeado = $(this);
        var id = enlaceClickeado.attr("huespedID");

        //Definir la transaccione AJAX al server
        $.ajax({
            url: "/Huesped/AjaxEdit",
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { huespedID: id }
        }).success(function (result) {
            var huesped = JSON.parse(result);
            $("#modalEditar #nombre").val(huesped.nombre);
            $("#modalEditar #apellidoP").val(huesped.apellidoP);
            $("#modalEditar #apellidoM").val(huesped.apellidoM);
            $("#modalEditar #telefono").val(huesped.telefono);
            //$("#modalEditar #grupoID").val(huesped.grupoID);


        }).error(function () {
            /*Notificar al usuario de un error de comunicacion
            con el server*/

        })
    })




})