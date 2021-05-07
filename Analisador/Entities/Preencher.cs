﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Analisador.Entities
{
    class Preencher
    {
        public static List<Resultado> CarregarResultados(string path)
        {
            int totalResNaoImportado = 0;
            int totalResImportado = 0;
            List<Resultado> resultados = new List<Resultado>();

            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");

            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    Console.WriteLine("Resultados não importados:");
                    while (!sr.EndOfStream)
                    {
                        string[] s = sr.ReadLine().Split(',');
                        if (s.Length == 17)
                        {
                            List<int> l = new List<int>();

                            int i = 0;
                            foreach (var item in s)
                            {
                                if (i > 1)
                                {
                                    l.Add(int.Parse(item));
                                }
                                i++;
                            }
                            resultados.Add(new Resultado(int.Parse(s[0]), DateTime.Parse(s[1]), l));
                            totalResImportado++;
                        }
                        else
                        {
                            totalResNaoImportado++;
                            foreach (var item in s)
                            {
                                Console.Write(item + ",");
                            }
                            Console.WriteLine();
                        }
                    }
                }
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Total de Erros na importação: " + totalResNaoImportado);
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Total de Sucessos na importção: " + totalResImportado);
                Console.WriteLine("------------------------------------");

                //Listar os resultados importados.
                Console.WriteLine("Últimos três resultados importados.");
                foreach (var item in resultados.Where(p => p.Concurso > resultados.Max(x => x.Concurso) - 3))
                {
                    Console.WriteLine(item.Concurso + " - " + item.Data.ToString("dd/MM/yyyy"));
                }

                Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
                Console.WriteLine();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            return resultados;
        }

        public static List<Combinacao> GerarCombinacoes(int k)
        {
            List<Combinacao> resultLista = new List<Combinacao>();

            List<string> lista = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25" };

            var result = Combinacoes(lista, k);

            foreach (var item in result)
            {
                int i = 0;
                string arrayItem = null;
                List<int> listaTemp = new List<int>();
                foreach (var s in item.Split(','))
                {
                    listaTemp.Add(int.Parse(s));
                    if (i == k - 1)
                    {
                        arrayItem = arrayItem + s.ToString();
                    }
                    else
                    {
                        arrayItem = arrayItem + s.ToString() + ",";
                    }
                    i++;
                }
                resultLista.Add(new Combinacao(arrayItem, listaTemp));
            }
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Total de Combinações geradas " + resultLista.Count);
            Console.WriteLine("-----------------------------------------------");
            return resultLista;
        }

        static IEnumerable<string> Combinacoes(List<string> characters, int length)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                // only want 1 character, just return this one
                if (length == 1)
                    yield return characters[i];

                // want more than one character, return this one plus all combinations one shorter
                // only use characters after the current one for the rest of the combinations
                else
                    foreach (string next in Combinacoes(characters.GetRange(i + 1, characters.Count - (i + 1)), length - 1))
                        yield return characters[i] + "," + next;
            }
        }

        public static void ComparaCombinacoesResultados(List<Combinacao> listComb, int pFrequencia, List<Resultado> resultados, int qtdConcurso)
        {
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
            Console.WriteLine("Combinações e frequência dos números analisando os últimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados");
            foreach (Combinacao combinacao in listComb)
            {
                var lCombina = new HashSet<int>(combinacao.SeqCombinada);
                foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso))
                {
                    var lResultado = new HashSet<int>(resultado.Numeros);
                    var count = lCombina.Intersect(lResultado);

                    if (count.Count() == lCombina.Count())
                    {
                        combinacao.addRepeticao(1);
                    }
                }
            }
            foreach (Combinacao item in listComb.Where(p => p.QtdRepeticoes >= pFrequencia).OrderBy(p => p.QtdRepeticoes))
            {
                Console.WriteLine(item.IdCombinacao + " - " + item.QtdRepeticoes);
            }
            Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=");
        }


        public static void QtdPares(List<Resultado> resultados, int qtdConcurso)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso))
            {
                int key = 0;
                foreach (var numeros in resultado.Numeros)
                {
                    if (numeros % 2 == 0)
                    {
                        key++;
                    }
                }
                if (result.TryGetValue(key, out int valor))
                {
                    result[key] += 1;
                }
                else
                {
                    result.Add(key, 1);
                }
            }
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine("Qtde de números pares apresentados nos resultados");
            Console.WriteLine("Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");
            foreach (KeyValuePair<int, int> item in result.OrderByDescending(s => s.Value))
            {
                Console.WriteLine("Número Par: {0} com Resultados: {1}", item.Key, item.Value);
            }
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine();
        }


        public static void QuadrantesLinha(List<Resultado> resultados, int qtdConcurso)
        {
            List<string> result = new List<string>();
            Dictionary<string, int> dicQL = new Dictionary<string, int>();

            foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso))
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

            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine("Qtde de números sorteados em cada linha do volante (Qtde vezes)");
            Console.WriteLine("Exibe os três mais dos cinco possiveis. Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");
            int j = 1;
            while (j <= 5)
            {
                int x = 1;
                foreach (KeyValuePair<string, int> item in dicQL.Where(p => p.Key.Contains("QL" + j)).OrderByDescending(s => s.Value))
                {
                    Console.WriteLine(item.Key + " - " + item.Value);
                    x++;
                    if (x > 3)
                    {
                        break;
                    }
                }
                Console.WriteLine("------------");
                j++;
            }
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine();
        }

        public static void QuadrantesColuna(List<Resultado> resultados, int qtdConcurso)
        {
            List<string> result = new List<string>();
            Dictionary<string, int> dicQC = new Dictionary<string, int>();

            foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso))
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

            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine("Qtde de números sorteados em cada coluna do volante (Qtde vezes)");
            Console.WriteLine("Exibe os três mais dos cinco possiveis. Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");
            int y = 1;
            while (y <= 5)
            {
                int x = 1;
                foreach (KeyValuePair<string, int> item in dicQC.Where(p => p.Key.Contains("QC" + y)).OrderByDescending(s => s.Value))
                {
                    Console.WriteLine(item.Key + " - " + item.Value);
                    x++;
                    if (x > 3)
                    {
                        break;
                    }
                }
                Console.WriteLine("------------");
                y++;
            }
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine();
        }

        public static void CincoMaisDosUltDoze(List<Resultado> resultados)
        {
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Dictionary<int, int> dicCincoMais = new Dictionary<int, int>();
            int contador = 5;

            for (int i = 0; i < 3; i++)
            {
                foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador))
                {
                    foreach (var numero in resultado.Numeros)
                    {
                        if (dicCincoMais.TryGetValue(numero, out int value))
                        {
                            dicCincoMais[numero] += 1;
                        }
                        else
                        {
                            dicCincoMais.Add(numero, 1);
                        }
                    }
                }
                //Impressão
                Console.WriteLine("Números que mais foram sorteados nos ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador).Count() + " concursos.");
                int j = 1;
                foreach (KeyValuePair<int, int> item in dicCincoMais.OrderByDescending(s => s.Value))
                {
                    Console.WriteLine(item.Key.ToString().PadLeft(2, '0') + " - " + item.Value);
                    j++;
                    if (j > 5)
                    {
                        break;
                    }
                }
                dicCincoMais.Clear();
                contador += 3;
                if (i != 2)
                {
                    Console.WriteLine("-----------------------------------------------------------");
                }
            }
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine();
        }

        public static void CincoMenosDosUltDoze(List<Resultado> resultados)
        {
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");

            Dictionary<int, int> dicCincoMenos = new Dictionary<int, int>();
            int contador = 5;
            int recValue = 0;

            for (int i = 0; i < 3; i++)
            {
                foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador))
                {
                    for (int y = 1; y <= 25; y++)
                    {
                        recValue = resultado.Numeros.Contains(y) ? 1 : 0;

                        if (dicCincoMenos.TryGetValue(y, out int value))
                        {
                            dicCincoMenos[y] += recValue;
                        }
                        else
                        {
                            dicCincoMenos.Add(y, recValue);
                        }
                    }
                }
                //Impressão
                Console.WriteLine("Números que menos foram sorteados nos ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador).Count() + " concursos.");
                int j = 1;
                foreach (KeyValuePair<int, int> item in dicCincoMenos.OrderBy(s => s.Value))
                {
                    Console.WriteLine(item.Key.ToString().PadLeft(2, '0') + " - " + item.Value);
                    j++;
                    if (j > 5)
                    {
                        break;
                    }
                }
                dicCincoMenos.Clear();
                contador += 3;
                if (i != 2)
                {
                    Console.WriteLine("-----------------------------------------------------------");
                }
            }
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine();
        }

        public static void SomaDezenas(List<Resultado> resultados, int qtdConcurso)
        {
            Dictionary<string, int> somaDezenas = new Dictionary<string, int>();
            string key = null;

            foreach (Resultado resultado in resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso))
            {
                int soma = 0;
                foreach (var item in resultado.Numeros)
                {
                    soma += item;
                }

                if (soma < 166)
                {
                    key = "120 à 165";
                }
                else if (soma > 165 && soma < 181)
                {
                    key = "166 à 180";
                }
                else if (soma > 180 && soma < 196)
                {
                    key = "181 à 195";
                }
                else if (soma > 195 && soma < 211)
                {
                    key = "196 à 210";
                }
                else if (soma > 210 && soma < 226)
                {
                    key = "211 à 225";
                }
                else
                {
                    key = "226 à 270";
                }

                if (somaDezenas.TryGetValue(key, out int value))
                {
                    somaDezenas[key] += 1;
                }
                else
                {
                    somaDezenas.Add(key, 1);
                }
            }

            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine("Soma das dezenas (Qtde vezes)");
            Console.WriteLine("Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");

            foreach (KeyValuePair<string, int> item in somaDezenas.OrderByDescending(s => s.Value))
            {
                Console.WriteLine("{0} - {1}", item.Key, item.Value);
            }
            Console.WriteLine("=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Console.WriteLine();
        }


    }
}