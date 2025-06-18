using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using Tarea2.Models;

namespace Tarea2.Helpers
{
    public static class DataManager
    {
        private static readonly string _filePath;

        static DataManager()
        {
            _filePath = HttpContext.Current.Server.MapPath("~/App_Data/DestinosEncuestas.json");
        }

        public static List<Destino> CargarDestinosDesdeArchivo()
        {
            if (!File.Exists(_filePath))
            {
                List<Destino> initialDestinos = new List<Destino>
                {
                    new Destino { Nombre = "París", Puntuacion = 0 },
                    new Destino { Nombre = "Tokio", Puntuacion = 0 },
                    new Destino { Nombre = "Nueva York", Puntuacion = 0 },
                    new Destino { Nombre = "Roma", Puntuacion = 0 },
                    new Destino { Nombre = "Londres", Puntuacion = 0 },
                    new Destino { Nombre = "Dubái", Puntuacion = 0 },
                    new Destino { Nombre = "Sidney", Puntuacion = 0 },
                    new Destino { Nombre = "Barcelona", Puntuacion = 0 },
                    new Destino { Nombre = "Ámsterdam", Puntuacion = 0 },
                    new Destino { Nombre = "Pekín", Puntuacion = 0 }
                };
                GuardarDestinosEnArchivo(initialDestinos); 
                return initialDestinos;
            }

            string json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<Destino>>(json);
        }

        public static void GuardarDestinosEnArchivo(List<Destino> destinos)
        {
            string json = JsonConvert.SerializeObject(destinos, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
    }
}