using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class Estatisticas
    {
        public static Dictionary<int, int> NumMaisSorteados(List<Resultado> resultados, int qtConcursos)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            var nCount = resultados.Where(p => p.Concurso > resultados.Max(m => m.Concurso) - 1).Select(p => p.Numeros).ToList();

            for (int i = 1; i <= nCount[0].Count(); i++)
            {
                result.Add(i, 0);
            }

            foreach (var resultado in resultados.Where(p => p.Concurso > resultados.Max(m => m.Concurso) - qtConcursos))
            {
                resultado.Numeros.Sort();
                foreach (var numero in resultado.Numeros)
                {
                    if (result.TryGetValue(numero, out int value))
                        result[numero] += 1;
                    else
                        result.Add(numero, 1);
                }
            }
            var resultSort = result.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
            return resultSort;
        }

        public static Dictionary<int, int> QuantPares(List<Resultado> resultados, int qtConcursos)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            for (int i = 2; i <= 12; i++)
            {
                result.Add(i, 0);
            }

            foreach (Resultado resultado in resultados.Where(c => c.Concurso > resultados.Max(m => m.Concurso) - qtConcursos))
            {
                int numPar = 0;
                foreach (var numeros in resultado.Numeros)
                {
                    if (numeros % 2 == 0)
                    {
                        numPar++;
                    }
                }
                if (result.TryGetValue(numPar, out int valor))
                {
                    result[numPar] += 1;
                }
                else
                {
                    result.Add(numPar, 1);
                }
            }
            var resultSort = result.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
            return resultSort;
        }

        public static Dictionary<string, int> SomaDezenas(List<Resultado> resultados, int qtConcursos)
        {
            Dictionary<string, int> somaDezenas = new Dictionary<string, int>();
            somaDezenas.Add("120 à 165", 0);
            somaDezenas.Add("166 à 180", 0);
            somaDezenas.Add("181 à 195", 0);
            somaDezenas.Add("196 à 210", 0);
            somaDezenas.Add("211 à 225", 0);
            somaDezenas.Add("226 à 270", 0);

            string key = null;
            foreach (Resultado resultado in resultados.Where(c => c.Concurso > resultados.Max(m => m.Concurso) - qtConcursos))
            {
                int soma = resultado.Numeros.Sum();

                if (soma < 166)
                    key = "120 à 165";
                else if (soma > 165 && soma < 181)
                    key = "166 à 180";
                else if (soma > 180 && soma < 196)
                    key = "181 à 195";
                else if (soma > 195 && soma < 211)
                    key = "196 à 210";
                else if (soma > 210 && soma < 226)
                    key = "211 à 225";
                else
                    key = "226 à 270";

                if (somaDezenas.TryGetValue(key, out int value))
                    somaDezenas[key] += 1;
                else
                    somaDezenas.Add(key, 1);
            }
            var resultSort = somaDezenas.OrderByDescending(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
            return resultSort;
        }

        public static Dictionary<string, int> QuadrantesLinha(List<Resultado> resultados, int qtConcursos)
        {
            List<string> result = new List<string>();
            Dictionary<string, int> dicQL = new Dictionary<string, int>();

            foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtConcursos))
            {
                int numIni = 1;
                int numFim = 5;
                string resultTemp = null;

                while (numFim <= 25)
                {
                    IEnumerable<int> Query = from num in resultado.Numeros where num >= numIni && num <= numFim select num;

                    if (numFim == 25)
                    {
                        int a = Query.Count();
                        resultTemp = resultTemp + a;
                    }
                    else
                    {
                        int a = Query.Count();
                        resultTemp = resultTemp + a + ",";
                    }
                    numIni += 5;
                    numFim += 5;
                }
                result.Add(resultTemp);
            }

            foreach (var item in result)
            {
                int i = 1;
                foreach (var itemSplit in item.Split(','))
                {
                    string key = "QL" + i + "-" + itemSplit;
                    if (dicQL.TryGetValue(key, out int valor))
                    {
                        dicQL[key] += 1;
                    }
                    else
                    {
                        dicQL.Add(key, 1);
                    }
                    i++;
                }
            }
            return dicQL;
        }

        public static Dictionary<string, int> QuadrantesColuna(List<Resultado> resultados, int qtConcursos)
        {
            List<string> result = new List<string>();
            Dictionary<string, int> dicQC = new Dictionary<string, int>();

            foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtConcursos))
            {
                HashSet<int> listNumResult = new HashSet<int>(resultado.Numeros);
                HashSet<int> listQC = new HashSet<int>();
                string resultTemp = null;
                int numStart = 1;
                int numIncre = numStart;

                for (int i = 0; i < 5; i++)
                {
                    //montar lista para comparar
                    for (int j = 0; j < 5; j++)
                    {
                        listQC.Add(numIncre);
                        numIncre += 5;
                    }
                    numStart++;
                    numIncre = numStart;

                    listQC.IntersectWith(listNumResult);

                    if (i == 4)
                    {
                        int a = listQC.Count();
                        resultTemp = resultTemp + a.ToString();
                    }
                    else
                    {
                        int a = listQC.Count();
                        resultTemp = resultTemp + a.ToString() + ",";
                    }
                    listQC.Clear();
                }
                result.Add(resultTemp);
            }

            foreach (var item in result)
            {
                int i = 1;
                foreach (var itemSplit in item.Split(','))
                {
                    string key = "QC" + i + "-" + itemSplit;
                    if (dicQC.TryGetValue(key, out int valor))
                    {
                        dicQC[key] += 1;
                    }
                    else
                    {
                        dicQC.Add(key, 1);
                    }
                    i++;
                }
            }
            return dicQC;
        }
    }
}
