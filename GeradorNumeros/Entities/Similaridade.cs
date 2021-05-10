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
    }
}
