using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Analisador.Entities
{
    class Preencher
    {
        public static string targetPath { get; internal set; }

        public static List<Resultado> CarregarResultados(string path)
        {
            int totalResNaoImportado = 0;
            int totalResImportado = 0;
            List<Resultado> resultados = new List<Resultado>();

            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            try
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    GravarArquivo(targetPath, "Resultados não importados:");
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
                            string naoImp = null;
                            foreach (var item in s)
                            {
                                naoImp += item + ","; //ajustar isso para gravar no arquivo GravarArquivo(targetPath, 
                            }
                            GravarArquivo(targetPath, naoImp);
                        }
                    }
                }
                GravarArquivo(targetPath, "------------------------------------");
                GravarArquivo(targetPath, "Total de Erros na importação: " + totalResNaoImportado);
                GravarArquivo(targetPath, "------------------------------------");
                GravarArquivo(targetPath, "Total de Sucessos na importção: " + totalResImportado);
                GravarArquivo(targetPath, "------------------------------------");

                //Listar os resultados importados.
                GravarArquivo(targetPath, "Últimos três resultados importados.");
                foreach (var item in resultados.Where(p => p.Concurso > resultados.Max(x => x.Concurso) - 3))
                {
                    GravarArquivo(targetPath, item.Concurso + " - " + item.Data.ToString("dd/MM/yyyy"));
                }

                GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
                GravarArquivo(targetPath, " ");
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                File.Delete(targetPath);
                Environment.Exit(0);
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
                        arrayItem = arrayItem + s.ToString().PadLeft(2, '0');
                    }
                    else
                    {
                        arrayItem = arrayItem + s.ToString().PadLeft(2, '0') + ",";
                    }
                    i++;
                }
                resultLista.Add(new Combinacao(arrayItem, listaTemp));
            }
            GravarArquivo(targetPath, "-----------------------------------------------");
            GravarArquivo(targetPath, "Total de Combinações geradas: " + resultLista.Count);
            GravarArquivo(targetPath, "-----------------------------------------------");
            GravarArquivo(targetPath, " ");
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
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");

            //Impressao do resumo ComparaCombinacoesResultados
            GravarArquivo(targetPath, "Resumo das Combinações e a frequência que elas se repetem");
            GravarArquivo(targetPath, "Maior frequência: " + listComb.Max(m => m.QtdRepeticoes));
            GravarArquivo(targetPath, "Menor frequência: " + listComb.Min(m => m.QtdRepeticoes));
            GravarArquivo(targetPath, "------------------------------------------");
            GravarArquivo(targetPath, "Total de frequência vs Qtde de Combinações");
            Dictionary<string, int> listSort = new Dictionary<string, int>();
            for (int i = 0; i < listComb.Max(m => m.QtdRepeticoes) + 1; i++)
            {
                if (listComb.Where(w => w.QtdRepeticoes == i).Count() > 0)
                {
                    listSort.Add(i.ToString().PadLeft(listComb.Max(m => m.QtdRepeticoes).ToString().Length, '0'), listComb.Where(w => w.QtdRepeticoes == i).Count());
                }
            }
            foreach (KeyValuePair<string, int> item in listSort.OrderByDescending(s => s.Key))
            {
                GravarArquivo(targetPath, item.Key + " - " + item.Value);
            }

            //Impressao do analitico de acordo com os parametros selecionados para comparar as combinações.
            GravarArquivo(targetPath, "--------------------------------------------------------------------------------------------------------------");
            GravarArquivo(targetPath, "Combinações e frequência dos números analisando os últimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados com frequência maior ou igua a " + pFrequencia);
            foreach (Combinacao item in listComb.Where(p => p.QtdRepeticoes >= pFrequencia).OrderBy(p => p.QtdRepeticoes))
            {
                GravarArquivo(targetPath, item.IdCombinacao + " - " + item.QtdRepeticoes);
            }
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=");
            GravarArquivo(targetPath, " ");
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
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, "Qtde de números pares apresentados nos resultados");
            GravarArquivo(targetPath, "Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");

            foreach (KeyValuePair<int, int> item in result.OrderByDescending(s => s.Value))
            {
                GravarArquivo(targetPath, "Número Par: " + item.Key + " com Resultados: " + item.Value);
            }

            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, " ");
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

            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, "Qtde de números sorteados em cada linha do volante (Qtde vezes)");
            GravarArquivo(targetPath, "Exibe os três mais dos cinco possiveis. Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");
            int j = 1;
            while (j <= 5)
            {
                int x = 1;
                foreach (KeyValuePair<string, int> item in dicQL.Where(p => p.Key.Contains("QL" + j)).OrderByDescending(s => s.Value))
                {
                    GravarArquivo(targetPath, item.Key + " - " + item.Value);
                    x++;
                    if (x > 3)
                    {
                        break;
                    }
                }
                GravarArquivo(targetPath, "------------");
                j++;
            }
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, " ");
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

            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, "Qtde de números sorteados em cada coluna do volante (Qtde vezes)");
            GravarArquivo(targetPath, "Exibe os três mais dos cinco possiveis. Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");
            int y = 1;
            while (y <= 5)
            {
                int x = 1;
                foreach (KeyValuePair<string, int> item in dicQC.Where(p => p.Key.Contains("QC" + y)).OrderByDescending(s => s.Value))
                {
                    GravarArquivo(targetPath, item.Key + " - " + item.Value);
                    x++;
                    if (x > 3)
                    {
                        break;
                    }
                }
                GravarArquivo(targetPath, "------------");
                y++;
            }
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, " ");
        }

        public static void CincoMaisDosUltDoze(List<Resultado> resultados)
        {
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            Dictionary<int, int> dicCincoMais = new Dictionary<int, int>();
            int contador = 5;

            for (int i = 0; i < 3; i++)
            {
                if (contador < resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador).Count())
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
                    GravarArquivo(targetPath, "Números que mais foram sorteados nos ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador).Count() + " concursos.");
                    int j = 1;
                    foreach (KeyValuePair<int, int> item in dicCincoMais.OrderByDescending(s => s.Value))
                    {
                        GravarArquivo(targetPath, item.Key.ToString().PadLeft(2, '0') + " - " + item.Value);
                        j++;
                        if (j > 5)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    GravarArquivo(targetPath, "Não há dados suficientes para a analise de resultados (Últimos " + (contador + 1) + ").");
                }

                dicCincoMais.Clear();
                contador += 3;
                if (i != 2)
                {
                    GravarArquivo(targetPath, "-----------------------------------------------------------");
                }
            }
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, " ");
        }

        public static void CincoMenosDosUltDoze(List<Resultado> resultados)
        {
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");

            Dictionary<int, int> dicCincoMenos = new Dictionary<int, int>();
            int contador = 5;
            int recValue = 0;

            for (int i = 0; i < 3; i++)
            {
                if (contador < resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador).Count())
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
                    GravarArquivo(targetPath, "Números que menos foram sorteados nos ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - contador).Count() + " concursos.");
                    int j = 1;
                    foreach (KeyValuePair<int, int> item in dicCincoMenos.OrderBy(s => s.Value))
                    {
                        GravarArquivo(targetPath, item.Key.ToString().PadLeft(2, '0') + " - " + item.Value);
                        j++;
                        if (j > 5)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    GravarArquivo(targetPath, "Não há dados suficientes para a analise de resultados (Últimos " + (contador + 1) + ").");
                }
                dicCincoMenos.Clear();
                contador += 3;
                if (i != 2)
                {
                    GravarArquivo(targetPath, "-----------------------------------------------------------");
                }
            }
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, " ");
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

            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, "Soma das dezenas (Qtde vezes)");
            GravarArquivo(targetPath, "Analisados os ultimos " + resultados.Where(c => c.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");

            foreach (KeyValuePair<string, int> item in somaDezenas.OrderByDescending(s => s.Value))
            {
                GravarArquivo(targetPath, item.Key + " - " + item.Value);
            }
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, " ");
        }

        public static void MapaResultado(List<Resultado> resultados, int qtdConcurso)
        {
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, "Mapa de resultados - Analisados os últimos " + resultados.Where(w => w.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso).Count() + " resultados.");
            foreach (Resultado resultado in resultados.Where(w => w.Concurso >= resultados.Max(m => m.Concurso) - qtdConcurso))
            {
                string result = resultado.Concurso.ToString().PadLeft(4,'0') + " - ";
                int k = 1;
                while (k < 26)
                {
                    if (k <= resultado.Numeros.Count)
                    {
                        foreach (var item in resultado.Numeros)
                        {
                            if (item == k)
                            {
                                result += item.ToString().PadLeft(2, '0') + " - ";
                                k++;
                            }
                            else
                            {
                                while (k < item)
                                {
                                    result += "XX - ";
                                    k++;
                                }
                                result += item.ToString().PadLeft(2, '0') + " - ";
                                k++;
                            }
                        }
                    }
                    else
                    {
                        while (k < 26)
                        {
                            result += "XX - ";
                            k++;
                        }
                    }
                }
                GravarArquivo(targetPath, result);
            }
            GravarArquivo(targetPath, "=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+=+");
            GravarArquivo(targetPath, " ");
        }

        public static void GravarArquivo(string caminho, string msg)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(caminho))
                {
                    sw.WriteLine(msg);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

    }
}