using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esteira.Dto
{
    public class EtiquetaDto
    {
        public string Produto { get; set; }
        public decimal Peso { get; set; }
        public int Quantidade { get; set; }
        public string CodBarras { get; set; }
    }
}
