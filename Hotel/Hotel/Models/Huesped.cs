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

            //// Huesped tiene una coleccion de reservaciones
            //public ICollection<Reservacion> reservaciones { get; set; }

            /*Representa la definicion de un modelo para vista (ViewModel),
     * por lo que no contiene información de relaciones con otras entidades*/
            public class VMHuesped
            {
                public int huespedID { get; set; }
                public String nombre { set; get; }

                public String apellidoP { get; set; }
                public String apellidoM { get; set; }
                public String telefono { get; set; }

                //public int grupoID { get; set; }
                //public String nombreGrupo { get; set; }

                public VMHuesped(Huesped huesped)
                {
                    this.huespedID = huesped.huespedID;
                    this.nombre = huesped.nombre;
                    this.apellidoM = huesped.apellidoM;
                    this.apellidoP = huesped.apellidoP;
                    this.telefono = huesped.telefono;
                    //this.grupoID = huesped.grupoID;

                    //if (huesped.grupo != null)
                    //    this.nombreGrupo = huesped.grupo.nombre;

                }
            }
        }
    }
