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

        //llave foranea con Huesped.
        public int habitacionID { get; set; }
        public Habitacion habitacion { get; set; }

        //Lave foranea con Habitacion.
        [DisplayName("Huesped")]
        public int huespedID { get; set; }
        public Huesped huesped { get; set; }
    }
}