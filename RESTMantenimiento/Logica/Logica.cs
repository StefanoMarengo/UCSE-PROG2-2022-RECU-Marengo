using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{

    public sealed class Logica
    {
        // SINGLETON

        private static Logica instance = null;
        private Logica()
        {

        }
        public static Logica Instancia
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logica();
                }
                return instance;
            }
        }
        //
        private static List<Pedido> ListaPedidos = new List<Pedido>()
        {
            new Pedido() {ID=554, Solicitante="Juan", Area="Informatica", Descripcion="Juego"},
            new Pedido() {ID=477, Solicitante="Jorge", Area="Informatica", Descripcion="Juego"},
            new Pedido() {ID=114, Solicitante="Vanesa", Area="Informatica", Descripcion="Juego"}
        };
        private static List<Tarea> ListaTareas = new List<Tarea>()
        {
            new Tarea() {ID=111, IDPedido=1312, CostoManodeObra=333, CostoMateriales=444, Estado="PENDIENTE"},
            new Tarea() {ID=222, IDPedido=1144, CostoManodeObra=774, CostoMateriales=444, Estado="COMPLETADO"},
            new Tarea() {ID=333, IDPedido=5522, CostoManodeObra=333, CostoMateriales=444, Estado="PENDIENTE"},
        };
        public Pedido CargarPedido(Pedido pedido)
        {
            Random rnd = new Random();

            Pedido pedidoNuevo = new Pedido()
            {
                ID = rnd.Next(0, 999),
                Solicitante = pedido.Solicitante,
                Area = pedido.Area,
                Descripcion = pedido.Descripcion,
                FechaCreacion = DateTime.Now
            };
            ListaPedidos.Add(pedidoNuevo);
            return pedidoNuevo;

        }
        public Tarea CargarTarea(Tarea tarea)
        {
            Tarea tareaNueva = new Tarea();
            tareaNueva.ID = new Random().Next(0, 999);
            tareaNueva.IDPedido = 1;
            tareaNueva.CostoMateriales = tarea.CostoMateriales;
            tareaNueva.CostoManodeObra = tarea.CostoManodeObra;
            tareaNueva.FechaCreacion = DateTime.Now;
            tareaNueva.Estado = "PENDIENTE";
            ListaTareas.Add(tareaNueva);
            return tareaNueva;

        }
        public Tarea ObtenerTareaId(int id)
        {
            var resultado = ListaTareas.Where(x=>x.ID==id).FirstOrDefault();
            return resultado;
        }
        public Pedido ObtenerPedidoId(int id)
        {
            var resultado = ListaPedidos.Where(x => x.ID == id).FirstOrDefault();
            return resultado;
        }
        public List<Tarea> ObtenerLista()
        {
            return ListaTareas;
        }

        public Tarea ActualizarTarea(int id)
        {
            Tarea tareaModific = ListaTareas.Where(x => x.ID == id).FirstOrDefault();
            tareaModific.Estado = "COMPLETADA";
            return tareaModific;
        }

        //Testeos----
        public List<Pedido> ObtenerListaPedidos()
        {
            return ListaPedidos;
        }
    }
}
