using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    class JogoGerado
    {
        public int Sequencial { get; set; }
        public DateTime Data { get; set; }
        public List<int> ListaNumeros { get; set; } = new List<int>(Gerador.GerarJogo());

        public JogoGerado(int sequencial, DateTime data)
        {
            Sequencial = sequencial;
            Data = data;
        }
    }
}
