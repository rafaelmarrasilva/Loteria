using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Analisador.Entities
{
    class Combinacao
    {
        public string IdCombinacao { get; set; }
        public List<int> SeqCombinada { get; set; }
        public int QtdRepeticoes { get; private set; }

        public Combinacao()
        {

        }

        public Combinacao(string idCombinacao, List<int> seqCombinada)
        {
            IdCombinacao = idCombinacao;
            SeqCombinada = seqCombinada;
        }

        public void addRepeticao(int qtdRepeticoes)
        {
            QtdRepeticoes = QtdRepeticoes + qtdRepeticoes;
        }
    }
}
