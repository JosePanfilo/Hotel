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
                    //"<td>" + alumno.grupo + "</td>" + //Nombre grupo
                    "<td>" + huesped.nombre + "</td>" + //nombre
                    "<td>" + huesped.apellidoP + "</td>" + //apellidoP
                    "<td>" + huesped.apellidoM + "</td>" + //apellidoM
                    "<td>" + huesped.telefono + "</td>" + //telefono
                    "<td>" +
                    "<a id='enlaceDetalles' data-toggle='modal' data-target='#modalDetalles' huespedID='" + huesped.huespedID + "'>Detalles</a> |" +
                    "<a id='enlaceBorrar' data-toggle='modal' data-target='#modalBorrar' huespedID='" + huesped.huespedID + "'>Borrar</a> |" +
                    "<a id='enlaceEditar' data-toggle='modal' data-target='#modalEditar' huespedID='" + huesped.huespedID + "'>Editar</a> |" +
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
        var id = enlaceClickeado.attr("huespedID");

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
            $("#modalEditar #noMatricula").val(huesped.huespedID);
            $("#modalEditar #nombre").val(huesped.nombre);
            $("#modalEditar #apellidoP").val(huesped.apellidoP);
            $("#modalEditar #apellidoM").val(huesped.apellidoM);
            //fechaRecibida = new Date(huesped.fechaNac);
            //$("#modalEditar #fechaNac").val(fechaRecibida);
            //document.getElementById("fechaNac").valueAsDate = fechaRecibida;
            //$("#modalEditar #fechaNac")[0].valueAsDate = fechaRecibida;
            //$("#modalEditar #grupoID").val(huesped.grupoID);

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
        alumnoModificado = {
            noMatricula: $("#modalEditar #noMatricula").val(),
            nombre: $("#modalEditar #nombre").val(),
            apellidoP: $("#modalEditar #apellidoP").val(),
            apellidoM: $("#modalEditar #apellidoM").val(),
            //fechaNac: $("#modalEditar #fechaNac").val(),
            //grupoID: $("#modalEditar #grupoID").val(),
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
            alert("No se encontro el servidor," +
                " verifique si se encuentra conectado a internet.");

        })
        $("#modalEditar").modal("toggle");
    })


})







//$().ready(function () {
//    //Abrir pantalla de editar y motar datos de alumno
//    $("a#enlaceEditar").click(function () {
//        //Se obtiene  el numero de huesped a consultar
//        var enlaceClickeado = $(this);
//        var huespedId = enlaceClickeado.attr("huespedID");

//        //Definir la transaccion AJAX al server
//        $.ajax({
//            url: "/Huesped/AjaxEedit", //Accion a ejecutar en el server
//            contentType: "application/html; charset=utf-8",
//            type: "GET",
//            dataType: "html",
//            data: { huespedId: huespedId } //Dato enviado al server
//        }).success(function (result) {
//            //Senobtiene la respuesta del servidor en forma de objeto
//            var huesped = JSON.parse(result);

//            //Con la informacion recibida, se rellena el formulario
//            $("#modalEditar #huespedID").val(huesped.huespedId);
//            $("#modalEditar #nombre").val(huesped.nombre);
//            $("#modalEditar #apellidoP").val(huesped.apellidoP);
//            $("#modalEditar #apellidoM").val(huesped.apellidoM);
//            $("#modalEditar #telefono").val(huesped.telefono);
//        }).error(function (xhr, status) {
//            /*Notificar al usuario de un error de comunicacion
//            con el server*/
//            $("#mensaje").removeClass('alert-danger alert-info');
//            $("#mensaje").html("Ha ocurrido un error: " + status).addClass('alert-danger');
//            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
//        })
//    })

//    /*Confirmar edicion de cambios en registro de alumnos*/
//    $("#btnEditar").click(function () {
//        //Se construye un objeto JSON que representa los valores nuevos para los atributos del huesped
//        var huespedModificado = {
//            noMatricula: $("#huespedID").val(),
//            nombre: $("#nombre").val(),
//            apellidoP: $("#apellidoP").val(),
//            apellidoM: $("#apellidoM").val(),
//            telefono:$("#telefono").val(),
//        }
        
//        $.ajax({
//            url: "/Huesped/AjaxEdit", //Accion a ejecutar en el server
//            contentType: "application/json; charset=utf-8",
//            type: "POST", //Accion en modalidad POST
//            dataType: "json",
//            data: JSON.stringify(huespedModificado) //Dato enviado al server
//        }).success(function(result){//si la aplicacion con el server se llevo acabo
//            $("#mensaje").removeClass('alert-danger alert-info');

//            claseMensaje = '';
//            if (result.codigo == 0)
//                claseMensaje = 'alert-danger';
//            else
//                claseMensaje = 'alert-info';

//            $("#mensaje").addClass(claseMensaje);
//            $("#mensaje").text(result.mensaje);
//            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);

//        }).error(function (xhr, status) {
//            /*Notificar al usuario de un error de comunicacion
//            con el server*/
//            $("#mensaje").removeClass('alert-danger alert-info');
//            $("#mensaje").addClass('alert-danger');
//            $("#mensaje").text("Ha ocurrido un error: " + status + '.'+xhr);
//            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
//        });
//        //Se cierra el modal
//        $("#modalEditar").modal('toggle');
//    })
//})