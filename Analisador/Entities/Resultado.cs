using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Entities
{
    class Resultado
    {
        public int Concurso { get; set; }
        public DateTime Data { get; set; }
        public List<int> Numeros { get; set; }

        public Resultado(int concurso, DateTime data, List<int> numeros)
        {
            Concurso = concurso;
            Data = data;
            Numeros = numeros;
        }

        public Resultado()
        {

        }
    }
}
