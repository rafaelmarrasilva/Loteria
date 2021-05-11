using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public class Parametros
    {
        public List<int> Matriz { get; set; } = new List<int>();
        public List<int> Pares { get; set; } = new List<int>();
        public List<string> QuadLinha { get; set; } = new List<string>();
        public List<string> QuadColuna { get; set; } = new List<string>();
        public List<int> SomaDezenas { get; set; } = new List<int>();

        public void addMatriz(int numMatriz)
        {
            Matriz.Add(numMatriz);
        }

        public void addPares(int numPares)
        {
            Pares.Add(numPares);
        }

        public void addQuadLinha(string stgQuadLinha)
        {
            QuadLinha.Add(stgQuadLinha);
        }

        public void addQuadColuna(string stgQuadColuna)
        {
            QuadColuna.Add(stgQuadColuna);
        }

        public void addSomaDezenas(int numSoma)
        {
            SomaDezenas.Add(numSoma);
        }

    }
}
