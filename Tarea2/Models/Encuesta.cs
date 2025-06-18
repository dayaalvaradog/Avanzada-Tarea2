using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tarea2.Models
{
    public class Encuesta
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        public string NombreEncuestado { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [Display(Name = "Apellido")]
        public string ApellidoEncuestado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un país.")]
        [Display(Name = "País de Pertenencia")]
        public string PaisPertenencia { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un rol.")]
        [Display(Name = "Rol en la Encuesta")]
        public string RolEncuesta { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un destino de viaje primario.")]
        [Display(Name = "Destino de Viaje Primario")]
        public string DestinoPrimario { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un destino de viaje secundario.")]
        [Display(Name = "Destino de Viaje Secundario")]
        public string DestinoSecundario { get; set; }
    }
}