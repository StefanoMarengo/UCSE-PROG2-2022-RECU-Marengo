using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Tarea
    {
        public int ID { get; set; }
        public int IDPedido { get; set; }
        public int CostoMateriales { get; set; }
        public int CostoManodeObra { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int CostoTotal { get { return CostoMateriales + CostoManodeObra; } }
    }
}
