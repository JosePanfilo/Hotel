using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Reservacion
    {
        [Key]
        public int reservacionID { get; set; }//llave primaria

        [DisplayName("Fecha de Ingreso")]
        [DataType(DataType.Date)]
        public DateTime fechaDeIngreso { get; set; }

        [DisplayName("Fecha de Salida")]
        [DataType(DataType.Date)]
        public DateTime fechaDeSalida { get; set; }

        [DisplayName("Numero de Habitacion")]
        public int numeroDeHabitacion { get; set; }

        //llave foranea con Huesped
        public int habitacionID { get; set; }
        public Habitacion habitacion { get; set; }

        //Lave foranea con Habitacion.
        [DisplayName("Huesped")]
        public int huespedID { get; set; }
        public Huesped huesped { get; set; }
    }
    /*Representa la definicion de un modelo para vista (ViewModel),
     * por lo que no contiene información de relaciones con otras entidades*/
    public class VMReservacion
    {
        public int reservacionID { get; set; }
        public DateTime fechaDeIngreso { get; set; }
        public DateTime fechaDeSalida { get; set; }
        public int numeroDeHabitacion { get; set; }

        public int habitacionID { get; set; }
        public int huespedID { get; set; }

        public VMReservacion(Reservacion reservacion)
        {
            this.reservacionID = reservacion.reservacionID;
            this.fechaDeIngreso = reservacion.fechaDeIngreso;
            this.fechaDeSalida = reservacion.fechaDeSalida;
            this.numeroDeHabitacion = reservacion.numeroDeHabitacion;

            this.habitacionID = reservacion.habitacionID;
            if (reservacion.habitacion != null)
                this.habitacionID = reservacion.habitacion.habitacionID;

            this.huespedID = reservacion.huespedID;
            if (reservacion.huesped != null)
                this.huespedID = reservacion.huesped.huespedID;

        }
    }
}