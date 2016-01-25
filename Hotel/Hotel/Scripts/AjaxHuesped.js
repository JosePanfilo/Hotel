$().ready(function () {
    //Abrir pantalla de editar y motar datos de alumno
    $("a#enlaceEditar").click(function () {
        //Se obtiene  el numero de huesped a consultar
        var enlaceClickeado = $(this);
        var huespedId = enlaceClickeado.attr("huespedId");

        //Definir la transaccion AJAX al server
        $.ajax({
            url: "/Huesped/AjaxEedit", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            data: { huespedId: huespedId } //Dato enviado al server
        }).success(function (result) {
            //Senobtiene la respuesta del servidor en forma de objeto
            var huesped = JSON.parse(result);

            //Con la informacion recibida, se rellena el formulario
            $("#modalEditar #huespedID").val(huesped.huespedId);
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

    /*Confirmar edicion de cambios en registro de alumnos*/
    $("#btnEditar").click(function () {
        //Se construye un objeto JSON que representa los valores nuevos para los atributos del huesped
        var huespedModificado = {
            noMatricula: $("#huespedID").val(),
            nombre: $("#nombre").val(),
            apellidoP: $("#apellidoP").val(),
            apellidoM: $("#apellidoM").val(),
            telefono:$("#telefono").val(),
        }
        
        $.ajax({
            url: "/Huesped/AjaxEdit", //Accion a ejecutar en el server
            contentType: "application/json; charset=utf-8",
            type: "POST", //Accion en modalidad POST
            dataType: "json",
            data: JSON.stringify(huespedModificado) //Dato enviado al server
        }).success(function(result){//si la aplicacion con el server se llevo acabo
            $("#mensaje").removeClass('alert-danger alert-info');

            claseMensaje = '';
            if (result.codigo == 0)
                claseMensaje = 'alert-danger';
            else
                claseMensaje = 'alert-info';

            $("#mensaje").addClass(claseMensaje);
            $("#mensaje").text(result.mensaje);
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);

        }).error(function (xhr, status) {
            /*Notificar al usuario de un error de comunicacion
            con el server*/
            $("#mensaje").removeClass('alert-danger alert-info');
            $("#mensaje").addClass('alert-danger');
            $("#mensaje").text("Ha ocurrido un error: " + status + '.'+xhr);
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
        });
        //Se cierra el modal
        $("#modalEditar").modal('toggle');
    })
})