using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class Similaridade
    {
        public static bool Similar(List<int> lista, List<Jogo> pLista)
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

        public static bool ResultadosAnteriores(List<int> lista, List<Resultado> resultados)
        {
            if (resultados.Count() == 0)
                return true;

            //Se encontrar similaridade com um resultado sai com false.

            return true;
        }
    }
}
