using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Pedido
    {
        public int ID { get; set; }
        public string Solicitante { get; set; }
        public string Area { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
