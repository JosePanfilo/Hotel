using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Habitacion
    {
        [Key]
        public int habitacionID { get; set; } //Llave primaria
        [DisplayName("Tamaño de la Habiación")]
        public string tamañoHabitacion { get; set; }
        [DisplayName("Estado de la Habiación")]
        public Boolean estado { get; set; }
        [DisplayName("Costo por Noche")]
        public Double costoNoche { get; set; }


        //una Habitación puede tener muchas Recervaciones
        public ICollection<Reservacion> reservaciones;
    }
}