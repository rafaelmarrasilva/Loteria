using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public class Resultado
    {
        public int Concurso { get; set; }
        public DateTime Data { get; set; }
        public List<int> Numeros { get; set; }

    }
}
