using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarea2.Models
{
    public class Destino
    {
        public string Nombre { get; set; }
        public double Puntuacion { get; set; } 
        public double PorcentajePopularidad { get; set; }
        public double DiferenciaPorcentualAnterior { get; set; } 
    }
}