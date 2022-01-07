using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class Similaridade
    {
        public static bool Similar(List<int> lista, List<Jogo> pLista, int qtdSimilar)
        {
            bool result = true;
            foreach (var p in pLista)
            {
                var listaTemp = new HashSet<int>(lista);
                var listaJogo = new HashSet<int>(p.ListaNumeros);

                listaTemp.IntersectWith(listaJogo);

                if (listaTemp.Count >= 14)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public static bool ResultadosAnteriores(List<int> lista, List<Resultado> resultados, int qtdSimilar)
        {
            if (resultados.Count() == 0)
                return true;

            //Se encontrar similaridade com um resultado sai com false.
            bool result = true;
            foreach(var p in resultados)
            {
                var listJogoGerado = new HashSet<int>(lista);
                var listResultConcuros = new HashSet<int>(p.Numeros);

                listJogoGerado.IntersectWith(listResultConcuros);

                if (listJogoGerado.Count() >= qtdSimilar)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
