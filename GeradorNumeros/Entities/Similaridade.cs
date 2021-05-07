using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    class Similaridade
    {
        public static void Similar(List<JogoGerado> pLista, int qtdSimilar)
        {
            //int tamLista = pLista.Count();
            int i = 0;
            int j = 0;
            foreach (var p in pLista)
            {
                var listaCom = new HashSet<int>(p.ListaNumeros);

                foreach (var s in pLista)
                {
                    if (i != j)
                    {
                        var listaSec = new HashSet<int>(s.ListaNumeros);

                        listaSec.IntersectWith(listaCom);

                        Console.WriteLine("Sequencial " + s.Sequencial + " é similar (" + listaSec.Count + ") ao Sequencial " + p.Sequencial);

                        j++;
                        listaSec.Clear();
                    }
                }
                j = 0;
                i++;
                listaCom.Clear();
            }
        }
    }
}
