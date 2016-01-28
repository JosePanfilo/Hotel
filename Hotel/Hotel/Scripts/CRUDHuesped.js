$().ready(function () {
    var huespedID = 0
    //rellenar la tabla index
    function rellenarIndexHuesped() {
        //agrega el texto que desea buscar
        var strBuscado = $("input[name='strBuscado']").val();
        //se ejecuta una conexion con el servidor en la accion del controlador ajaxIndex de la entidad del libro
        $.ajax({
            //Se ejecuta la ruta en la cual se ejecutara la acción
            url: "/Huesped/AjaxIndex", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",//tipo de contenido que se enviara
            type: "GET",//tipo de transaccion
            dataType: "html",//tipo de archivo
            data: { strBuscado: strBuscado } //Dato enviado al server

    //Si todo salio bien en la transaccion ajax entra en .success
        }).success(function (result) {
            //se crea un a nueva variable de tipo tbody en la tabla de la vista de index Huesped
            var tablaHuesped = $("tablaHuesped tbody");
            //se limpia la tabla
            tablaHuesped.html("");
            //se transforma el archivo Json que biene en formato json de la base de datos de cadena de string a formato json puro
            var conjuntoHuespedes = JSON.parse(result);

            //Se rellena la tabla de huespedes con todos los datos qe la integra
            for (var indice in conjuntoHuespedes) {
                var huesped = conjuntoHuespedes[indice];
                tablaHuesped.append("<tr>" +
                     "<td>" + huesped.nombre + "</td>" +
                     "<td>" + huesped.apellidoP + "</td>" +
                     "<td>" + huesped.apellidoM + "</td>" +
                     "<td>" + huesped.telefono + "</td>" +
                     "<td>" + "<a id='enlaceDetalles' data-toggle='modal' data-target='#modalDetalles' huespedID='" + huesped.huespedID + ">Detalles</a> |" +
                            "<a id='enlaceBorrar' data-toggle='modal' data-target='#modalBorrar' huespedID='" + huesped.huespedID + ">Borrar</a> |'" +
                            "<a id='enlaceEditar' data-toggle='modal' data-target='#modalEditar' huespedID=" + huesped.huespedID + ">Editar</a>" +
                     "</td>"+"</tr>")
            }
            //Si sale algun error en la transaccion Ajax entra en .error
        }).error(function (xhr, status) {

        })
    }


    //<-<-<-<-<-<-<-<-<-<-<-<--<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->//


    $("a#enlaceEditar").click(function () {
        var enlaceClickeado = $(this)
        //Selecciona el huespedID para rellenar los los campos del modal modificar
        var huespedID = enlaceClickeado.attr("huespedID")
        $.ajax({
            //Accion a ejecutar en el server
            url: "/Huesped/AjaxEdit", 
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            //Dato enviado al server
            data: { huespedID: huespedID }
        }).success(function (result) {
            //se ontiene la respuesta del server en forma de objetos
            var huesped = JSON.parse(result);

            //con la informacion que se obtivo se rellena el formulario
            $("#modalEditar #huespedID").val(huesped.huespedID);
            $("#modalEditar #nombre").val(huesped.nombre);
            $("#modalEditar #apellidoP").val(huesped.apellidoP);
            $("#modalEditar #apellidoM").val(huesped.apellidoM);
            $("#modalEditar #telefono").val(huesped.telefono);
        })
    })//--------------------------------------------------------------//
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
            alert("Lo sentimos no se pudo conectar con el servidor, verifique su conexion a internet. XD");
        })
        $("#modalEditar").modal("toggle");
    })


    //<-<-<-<-<-<-<-<-<-<-<-<--<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->//


    $("a#enlaceBorrar").click(function () {
        huespedID = $(this).attr("huespedID")
    })
    //Elimina el registro seleccionado
    $("#btnBorrar").click(function () {
        $.ajax({
            url: '/Huesped/DeleteConfirmed',
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            //Dato enviado al server
            data: { huespedID: huespedID }, 
            type: 'get',
        }).success(function (result) {
            rellenarIndexHuesped();
        }).error(function (result) {
            alert("Lo sentimos no se pudo conectar con el servidor, verifique su conexion a internet. XD");
        })
    })


    //<-<-<-<-<-<-<-<-<-<-<-<--<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->//


    $("#btnCrear").click(function () {
        nuevoHuesped = {
            nombre: $("#modalAlta #nombre").val(),
            apellidoP: $("#modalAlta #apellidoP").val(),
            apellidoM: $("#modalAlta #apellidoM").val(),
            telefono: $("#modalAlta #telefono").val(),
        };
        $.ajax({
            url: '/Huesped/Create',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(nuevoHuesped),
            type: 'post',
        }).success(function (result) {
            rellenarIndexHuesped();
        }).error(function (xhr, status) {

        })
        $("#modalAlta").modal("toggle");
    })


    //<-<-<-<-<-<-<-<-<-<-<-<--<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->->//


    $("a#enlaceDetalles").click(function () {
        var enlaceClickeado = $(this)
        var huespedID = enlaceClickeado.attr("huespedID")
        $.ajax({
            url: "/Huesped/Details", //Accion a ejecutar en el server
            contentType: "application/html; charset=utf-8",
            type: "GET",
            dataType: "html",
            //Dato enviado al server
            data: { id: huespedID } 
        }).success(function (result) {
            var huesped = JSON.parse(result);
            var detalles = $("#detalleHuesped");
            detalles.html("");
            //Con la inofrmacion obtenida se rellena el formulario
            detalles.append(
                "<div>" + "<b>" + "Nombre: "+"</b>" + huesped.nombre + "</div>" +
                "<div>" + "<b>" + "Apellido Paterno: " + "</b>" + huesped.apellidoP + "</div>" +
                "<div>" + "<b>" + "Apellido Materno: " + "</b>" + huesped.apellidoM + "</div>" +
                "<div>" + "<b>" + "Telefono: " + "</b>" + huesped.telefono + "</div>"
            )
        }).error(function (xhr, status) {
            /*Notificar al usuario de un error de comunicacion
                        con el server*/
            $("#mensaje").removeClass('alert-danger alert-info');
            $("#mensaje").html("Ha ocurrido un error: " + status).addClass('alert-danger');
            $("#mensaje").fadeIn(500).delay(2000).fadeOut(500);
        })
    })
})