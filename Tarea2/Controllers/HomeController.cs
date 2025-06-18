using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tarea2.Helpers;
using Tarea2.Models;

namespace Tarea2.Controllers
{
    public class HomeController : Controller
    {

        private const string SessionKeyDestinos = "DestinosViaje";

        public ActionResult Index()
        {
            List<Destino> destinos = ObtenerDestinos();
            List<Resumen> resumen = CalcularResumenIndice(destinos);
            return View(resumen);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private List<Destino> ObtenerDestinos()
        {
            List<Destino> destinos = Session[SessionKeyDestinos] as List<Destino>;
            if (destinos == null)
            {
                destinos = DataManager.CargarDestinosDesdeArchivo();
                Session[SessionKeyDestinos] = destinos;
            }
            return destinos;
        }

        private void GuardarDestinos(List<Destino> destinos)
        {
            Session[SessionKeyDestinos] = destinos;
            DataManager.GuardarDestinosEnArchivo(destinos);
        }

        private List<Resumen> CalcularResumenIndice(List<Destino> destinos)
        {
            double totalPuntuacion = destinos.Sum(d => d.Puntuacion);

            List<Destino> destinosOrdenados = destinos
                .OrderByDescending(d => d.Puntuacion)
                .ToList();

            List<Resumen> resumen = new List<Resumen>();
            for (int i = 0; i < Math.Min(20, destinosOrdenados.Count); i++)
            {
                Destino actual = destinosOrdenados[i];
                double porcentajeActual = (totalPuntuacion > 0) ? (actual.Puntuacion / totalPuntuacion) * 100 : 0;

                double diferenciaAnterior = 0;
                if (i > 0)
                {
                    Destino anterior = destinosOrdenados[i - 1];
                    double porcentajeAnterior = (totalPuntuacion > 0) ? (anterior.Puntuacion / totalPuntuacion) * 100 : 0;
                    diferenciaAnterior = porcentajeActual - porcentajeAnterior;
                }

                resumen.Add(new Resumen
                {
                    Posicion = i + 1,
                    NombreDestino = actual.Nombre,
                    ClasificacionPorcentual = Math.Round(porcentajeActual, 2),
                    DiferenciaPorcentualAnterior = Math.Round(diferenciaAnterior, 2)
                });
            }
            return resumen;
        }

        private List<SelectListItem> ObtenerListaRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Turista", Text = "Turista" },
                new SelectListItem { Value = "Viajero de Negocios", Text = "Viajero de Negocios" },
                new SelectListItem { Value = "Estudiante", Text = "Estudiante" },
                new SelectListItem { Value = "Investigador", Text = "Investigador" },
                new SelectListItem { Value = "Otro", Text = "Otro" }
            };
        }

        private List<SelectListItem> ObtenerListaPaises()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "CR", Text = "Costa Rica" },
                new SelectListItem { Value = "MX", Text = "México" },
            };
        }

        public ActionResult AgregarEncuesta()
        {
            ViewBag.Paises = ObtenerListaPaises();
            ViewBag.Roles = ObtenerListaRoles();
            ViewBag.Destinos = ObtenerDestinos().Select(d => d.Nombre).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult GuardarEncuesta(Encuesta model)
        {
            if (ModelState.IsValid)
            {
                List<Destino> destinos = ObtenerDestinos();

                // Sumar puntuaciones
                Destino destinoPrimario = destinos.FirstOrDefault(d => d.Nombre == model.DestinoPrimario);
                if (destinoPrimario != null)
                {
                    destinoPrimario.Puntuacion += 1;
                }

                Destino destinoSecundario = destinos.FirstOrDefault(d => d.Nombre == model.DestinoSecundario);
                if (destinoSecundario != null)
                {
                    destinoSecundario.Puntuacion += 0.5;
                }

                GuardarDestinos(destinos);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Por favor, corrija los errores en el formulario.");
                ViewBag.Roles = ObtenerListaRoles();
                ViewBag.Paises = ObtenerListaPaises();
                return View("Encuesta", model);
            }
        }

        public ActionResult ActualizarGrid()
        {
            List<Destino> destinos = ObtenerDestinos();
            List<Resumen> resumen = CalcularResumenIndice(destinos);
            return PartialView("_TablaDestinos", resumen); 
        }
    }
}