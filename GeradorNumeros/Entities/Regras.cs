using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class Regras
    {
        public static bool Pares(List<int> lista, List<int> parametros)
        {
            if (parametros.Count() == 0)
            {
                return true;
            }
            int i = 0;
            foreach (var item in lista)
            {
                int resto = item % 2;
                if (resto == 0)
                {
                    i++;
                }
            }
            return parametros.Contains(i);
        }

        public static bool QuadranteLinha(List<int> lista, List<string> parametros)
        {
            if (parametros.Count() == 0)
            {
                return true;
            }
            List<int> listaResult = new List<int>();
            int numIni = 1;
            int numFim = 5;

            for (int i = 0; i < 5; i++)
            {
                IEnumerable<int> Query = from num in lista where num >= numIni && num <= numFim select num;
                listaResult.Add(Query.Count());
                numIni += 5;
                numFim += 5;
            }
            int k = 0;
            int contaQL = 0;
            foreach (var item in parametros)
            {
                foreach (var s in item.Split(','))
                {
                    if (listaResult[k] == int.Parse(s))
                    {
                        contaQL++;
                    }
                }
                k++;
            }
            return contaQL == 5 ? true : false;
            //01, 02, 03, 04, 05
            //06, 07, 08, 09, 10
            //11, 12, 13, 14, 15
            //16, 17, 18, 19, 20
            //21, 22, 23, 24, 25
        }

        public static bool QuadranteColuna(List<int> lista, List<string> parametros)
        {
            if (parametros.Count() == 0)
            {
                return true;
            }
            List<int> listaResult = new List<int>();
            int numStart = 1;
            int num = numStart;
            for (int i = 0; i < 5; i++)
            {
                int contador = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (lista.Contains(num))
                    {
                        contador++;
                    }
                    num += 5;
                }
                listaResult.Add(contador);
                numStart++;
                num = numStart;
            }
            int k = 0;
            int contaQC = 0;
            foreach (var item in parametros)
            {
                foreach (var s in item.Split(','))
                {
                    if (listaResult[k] == int.Parse(s))
                    {
                        contaQC++;
                    }
                }
                k++;
            }
            return contaQC == 5 ? true : false;
            //01, 06, 11, 16, 21
            //02, 07, 12, 17, 22
            //03, 08, 13, 18, 23
            //04, 09, 14, 19, 24
            //05, 10, 15, 20, 25
        }

        public static bool SomaDezenas(List<int> lista, List<int> parametros)
        {
            if (parametros.Count() == 0)
            {
                return true;
            }
            return lista.Sum() >= parametros[0] && lista.Sum() <= parametros[1] ? true : false;
        }
    }
}
