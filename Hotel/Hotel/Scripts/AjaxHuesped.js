$().ready(function () {

    function rellenarIndexHuesped() {
        var strBuscado = $("input[name='strBuscado']").val();
        $.ajax({
            url: "/Huesped/AjaxIndex", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { strBuscado: strBuscado } //Dato enviado al server
        }).success(function (result) {
            var tablaHuesped = $("#tablaHuesped tbody");
            tablaHuesped.html("");
            var conjutoHuesped = JSON.parse(result);

            for (var indice in conjutoHuesped) {
                var huesped = conjutoHuesped[indice];
                tablaHuesped.append("<tr>" +
                    "<td>" + huesped.huespedID + "</td>" + //Nombre grupo
                    "<td>" + huesped.nombre + "</td>" + //nombre
                    "<td>" + huesped.apellidoP + "</td>" + //apellidoP
                    "<td>" + huesped.apellidoM + "</td>" + //apellidoM
                    "<td>" + huesped.telefono + "</td>" + //tel
                    "<a id='enlaceDetalles' data-toggle='modal' data-target='#modalDetalles' huespedId='" + huesped.huespedID + "'>Detalles</a> |" +
                    "<a id='enlaceBorrar' data-toggle='modal' data-target='#modalBorrar' huespedId='" + huesped.huespedID + "'>Borrar</a> |" +
                    "<a id='enlaceEditar' data-toggle='modal' data-target='#modalEditar' huespedId='" + huesped.huespedID + "'>Editar</a> |" +
                    "</td>" +
                    "</tr>")
            }

        }).error(function (xhr, status) {

        })
    }


    //Abrir pantalla de Editar y mostrar datos de alumno
    $("a#enlaceEditar").click(function () {
        //Se obtiene el numero de matricula a consultar
        var enlaceClickeado = $(this);
        var id = enlaceClickeado.attr("huespdID");

        //Definir la transaccione AJAX al server
        $.ajax({
            url: "/Huesped/AjaxEdit", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { huespedID: id } //Dato enviado al server
        }).success(function (result) { //result = {mensaje, status}
            //Se obtiene la respuesta del server en forma de objeto
            var huesped = JSON.parse(result);

            //Con la información recibida, se rellena el formulario
            $("#modalEditar #huespedID").val(huesped.huespedID);
            $("#modalEditar #nombre").val(huesped.nombre);
            $("#modalEditar #apellidoP").val(huesped.apellidoP);
            $("#modalEditar #apellidoM").val(huesped.apellidoM);
            $("#modalEditar #telefono").val(huesped.telefono);

        }).error(function (xhr, status) {
            /*Notificar al usuario de un error de comunicacion
            con el server*/
            $("#mensaje").removeClass('alert-danger alert-info');
            $("#mensaje").html("Ha ocurrido un error: " + status).addClass('alert-danger');
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
        })
    })

    /*Confirmar edicion de cambios en registro de huespedes*/
    $("#btnEditar").click(function () {
       huespedModificado = {
           huespedID: $("#modalEditar #huespedID").val(),
            nombre: $("#modalEditar #nombre").val(),
            apellidoP: $("#modalEditar #apellidoP").val(),
            apellidoM: $("#modalEditar #apellidoM").val(),
            telefono: $("#modalEditar #telefono").val(),
        };

        $.ajax({
            url: '/Huesped/AjaxEdit',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(huespedModificado),
            type: 'post',
        }).success(function (result) {
            rellenarIndexHuesped();
        }).error(function (xhr, status) {
            alert("No se encontro el servidor,"+
                " verifique si se encuentra conectado a internet.");

        })
        $("#modalEditar").modal("toggle");
    })

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Abrir pantalla de Detalles y mostrar datos de huesped
    $("a#enlaceDetalles").click(function () {
        //Se obtiene el numero de matricula a consultar
        var enlaceClickeado = $(this);
        var id = enlaceClickeado.attr("huespedID");

        //Definir la transaccione AJAX al server
        $.ajax({
            url: "/Huesped/AjaxDetails", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { huespedID: id } //Dato enviado al server
        }).success(function (result) { //result = {mensaje, status}
            //Se obtiene la respuesta del server en forma de objeto
            var huesped = JSON.parse(result);

            //Con la información recibida, se rellena el formulario
            $("#modalDetalles #huespedID").val(huesped.huespedID);
            $("#modalDetalles #nombre").val(huesped.nombre);
            $("#modalDetalles #apellidoP").val(huesped.apellidoP);
            $("#modalDetalles #apellidoM").val(huesped.apellidoM);
            $("#modalDetalles #telefono").val(huesped.telefono);

        }).error(function (xhr, status) {
            /*Notificar al usuario de un error de comunicacion
            con el server*/
            $("#mensaje").removeClass('alert-danger alert-info');
            $("#mensaje").html("Ha ocurrido un error: " + status).addClass('alert-danger');
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
        })
    })

})
