using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using Logica;


namespace RESTMantenimiento.Controllers
{
    public class PedidoServicio
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del solicitante")]
        public string Solicitante { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del area")]
        public string Area { get; set; }

        [Required(ErrorMessage = "Debe ingresar la descripcion del paquete")]
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
    public static class ConversorsPedidos
    {
        public static PedidoServicio ConvertirAServicio(this Pedido pedido)
        {
            PedidoServicio pedidoServicio = new PedidoServicio()
            {
                ID = pedido.ID,
                Solicitante = pedido.Solicitante,
                Area = pedido.Area,
                Descripcion = pedido.Descripcion,
                FechaCreacion=pedido.FechaCreacion

            };
            return pedidoServicio;
        }
        public static Pedido ConvertirALogica(this PedidoServicio pedidoServicio)
        {
            Pedido pedido = new Pedido()
            {
                ID = pedidoServicio.ID,
                Solicitante = pedidoServicio.Solicitante,
                Area = pedidoServicio.Area,
                Descripcion = pedidoServicio.Descripcion,
                FechaCreacion = pedidoServicio.FechaCreacion

            };
            return pedido;
        }
    }
    public class PedidosController : ApiController
    {
        [Route("NuevoPedido")]
        public IHttpActionResult Post([FromBody] PedidoServicio pedidoServicio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Created("", Logica.Logica.Instancia.CargarPedido(pedidoServicio.ConvertirALogica()).ConvertirAServicio());
        }
        //Para probar-----------------------
        [Route("ListaPedidos")]
        public IHttpActionResult Get()
        {
            List<PedidoServicio> listaServicio = new List<PedidoServicio>();
            List<Pedido> lista = Logica.Logica.Instancia.ObtenerListaPedidos();

            foreach (Pedido item in lista)
            {
                listaServicio.Add(item.ConvertirAServicio());
            }

            return Ok(listaServicio);
        }
    }
}
