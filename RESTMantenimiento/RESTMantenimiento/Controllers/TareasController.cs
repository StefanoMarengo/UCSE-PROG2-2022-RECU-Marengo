using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using Logica;

namespace RESTMantenimiento.Controllers
{
    public class TareasServicio
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Debe ingresar una ID del Pedido")]
        public int IDPedido { get; set; }

        [Required(ErrorMessage = "Debe ingresar un costo de materiales")]
        public int CostoMateriales { get; set; }

        [Required(ErrorMessage = "Debe ingresar un costo de mano de obra")]
        public int CostoManodeObra { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CostoTotal { get { return CostoMateriales + CostoManodeObra; } }
    }
    public static class Conversors
    {
        public static TareasServicio ConvertirAServicio(this Tarea tarea)
        {
            TareasServicio tareaservicio = new TareasServicio()
            {
                ID = tarea.ID,
                IDPedido = tarea.IDPedido,
                CostoMateriales = tarea.CostoMateriales,
                CostoManodeObra = tarea.CostoManodeObra,
                Estado = tarea.Estado,
                FechaCreacion = tarea.FechaCreacion

            };
            return tareaservicio;
        }
        public static Tarea ConvertirALogica(this TareasServicio tareaServicio)
        {
            Tarea tarea = new Tarea()
            {
                ID=tareaServicio.ID,
                IDPedido = tareaServicio.IDPedido,
                CostoMateriales = tareaServicio.CostoMateriales,
                CostoManodeObra = tareaServicio.CostoManodeObra,
                Estado = tareaServicio.Estado,
                FechaCreacion = tareaServicio.FechaCreacion

            };
            return tarea;
        }
    }
    public class TareasController : ApiController
    {
        [Route("Tarea/{id}")]
        public IHttpActionResult Get(int id)
        {
            Tarea tarea = Logica.Logica.Instancia.ObtenerTareaId(id);
            if (tarea == null)
                return NotFound();

            return Ok(tarea.ConvertirAServicio());
        }
        [Route("ListaTareas")]
        public IHttpActionResult Get()
        {
            List<TareasServicio> listaServicio = new List<TareasServicio>();
            List<Tarea> lista = Logica.Logica.Instancia.ObtenerLista();

            foreach (Tarea item in lista)
            {
                listaServicio.Add(item.ConvertirAServicio());
            }

            return Ok(listaServicio);
        }

        [Route("ListaTareas/{estado}")]
        public IHttpActionResult GetFiltrado(string estado)
        {
            List<TareasServicio> listaServicio = new List<TareasServicio>();
            List<Tarea> lista = Logica.Logica.Instancia.ObtenerLista();
            lista = lista.Where(x => x.Estado.ToLower().Contains(estado.ToLower())).ToList();

            foreach (Tarea item in lista)
            {
                listaServicio.Add(item.ConvertirAServicio());
            }

            return Ok(listaServicio);
        }

        [Route("NuevaTarea")]
        public IHttpActionResult Post([FromBody] TareasServicio tareaServicio)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (Logica.Logica.Instancia.ObtenerPedidoId(tareaServicio.IDPedido) == null)
                return BadRequest("No existe un pedido con esa ID");

            return Created("", Logica.Logica.Instancia.CargarTarea(tareaServicio.ConvertirALogica()).ConvertirAServicio());
        }

        [Route("ModificarTarea/{id}")]
        public IHttpActionResult Put(int id)
        {
            Tarea tarea = Logica.Logica.Instancia.ObtenerTareaId(id);
            if (tarea == null)
                return NotFound();

            return Ok(Logica.Logica.Instancia.ActualizarTarea(id).ConvertirAServicio());
        }
    }
}
