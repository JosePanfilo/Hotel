using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
        //Se crea modelo de huesped
        public class Huesped
        {
            [Key]
            public int huespedID { get; set; } //Llave primaria
            [DisplayName("Nombre")]
            public String nombre { get; set; }
            [DisplayName("Apellido Paterno")]
            public String apellidoP { get; set; }
            [DisplayName("Apellido Materno")]
            public String apellidoM { get; set; }
            [DisplayName("Telefono")]
            public String telefono { get; set; }

            // Huesped tiene una coleccion de reservaciones
            public ICollection<Reservacion> reservaciones { get; set; }
        }
    }
