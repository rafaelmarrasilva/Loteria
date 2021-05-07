using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class Regras
    {
        public static int Pares(List<int> vs)
        {
            int i = 0;
            foreach (var item in vs)
            {
                int resto = item % 2;
                if (resto == 0)
                {
                    i++;
                }
            }
            return i;
            //Retornos bons 6, 7 ou 8
        }

        public static bool ValidaQuadrantesLinha(List<int> vs)
        {
            bool continua = true;
            bool result = true;
            int numIni = 1;
            int numFim = 5;

            while (continua)
            {
                IEnumerable<int> Query = from num in vs where num >= numIni && num <= numFim select num;
                if (Query.Count() > 1)
                {
                    numIni = numIni + 5;
                    numFim = numFim + 5;
                }
                else
                {
                    continua = false;
                    result = false;
                }

                if (numFim > 25)
                {
                    continua = false;
                }
            }
            return result;
            //01, 02, 03, 04, 05
            //06, 07, 08, 09, 10
            //11, 12, 13, 14, 15
            //16, 17, 18, 19, 20
            //21, 22, 23, 24, 25
        }

        public static bool ValidaQuadrantesColuna(List<int> vs)
        {
            bool continua = true;
            bool result = true;
            int pStart = 1;
            int j = 1;

            var listaJogo = new HashSet<int>(vs);

            HashSet<int> lst = new HashSet<int>();

            while (continua)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 0)
                    {
                        lst.Add(pStart);
                    }
                    else
                    {
                        lst.Add(pStart);
                    }
                    pStart = pStart + 5;
                }

                lst.IntersectWith(listaJogo);

                if (lst.Count() <= 1)
                {
                    continua = false;
                    result = false;
                }

                j++;
                pStart = j;
                lst.Clear();

                if (pStart > 5)
                {
                    continua = false;
                }
            }
            return result;
            //01, 06, 11, 16, 21
            //02, 07, 12, 17, 22
            //03, 08, 13, 18, 23
            //04, 09, 14, 19, 24
            //05, 10, 15, 20, 25
        }
    }
}
