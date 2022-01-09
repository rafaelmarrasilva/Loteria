using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public class ResultadosConferidos
    {
        public int Acertos { get; set; }
        public List<int> Jogo { get; set; } = new List<int>();

        public ResultadosConferidos(int acertos, List<int> jogo)
        {
            Acertos = acertos;
            Jogo = jogo;
        }
    }
}
