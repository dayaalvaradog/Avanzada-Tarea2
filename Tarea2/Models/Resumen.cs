using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tarea2.Models
{
    public class Resumen
    {
        public int Posicion { get; set; }
        public string NombreDestino { get; set; }
        public double ClasificacionPorcentual { get; set; }
        public double DiferenciaPorcentualAnterior { get; set; }
    }
}