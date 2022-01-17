using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class Regras
    {
        public static (List<Resultado>, string) CarregarResultados(string nomeArquivo, string nomeLoteria)
        {
            int posLinha = 0;
            switch (nomeLoteria)
            {
                case "LotoFacil":
                    posLinha = 17;
                    break;
                case "MegaSena":
                    posLinha = 8;
                    break;
            }

            List<Resultado> result = new List<Resultado>();
            int resultNaoImpor = 0;
            foreach (var linhas in File.ReadAllLines(nomeArquivo))
            {
                Resultado resultado = new Resultado();
                string[] vs = linhas.ToString().Split(',');
                if (vs.Length == posLinha)
                {
                    resultado.Concurso = int.Parse(vs[0]);
                    resultado.Data = DateTime.Parse(vs[1]);
                    List<int> lista = new List<int>();

                    for (int i = 2; i < vs.Length; i++)
                    {
                        lista.Add(int.Parse(vs[i]));
                    }
                    lista.Sort();
                    resultado.Numeros = lista;
                }
                else
                    resultNaoImpor++;

                result.Add(resultado);
            }
            string qtdResultImport = result.Count() + "," + resultNaoImpor;

            return (result, qtdResultImport);
        }

        public static List<ResultadosConferidos> Conferencia(List<int> resultadoConcuro, string caminho)
        {
            List<ResultadosConferidos> listaConferida = new List<ResultadosConferidos>();

            foreach (var linha in File.ReadAllLines(caminho))
            {
                List<int> l = new List<int>();
                foreach (var num in linha.Split(','))
                {
                    if (int.TryParse(num, out int value))
                        l.Add(value);
                    else
                        throw new InvalidProgramException("Só é permitido digitar números e ou ',' como separador.");
                }
                if (l.Count() < 15)
                    throw new InvalidProgramException("O resultado deve ter 15 números e não " + l.Count());

                var hashResultadoConcurso = new HashSet<int>(resultadoConcuro);
                var hashJogo = new HashSet<int>(l);
                hashJogo.IntersectWith(hashResultadoConcurso);

                listaConferida.Add(new ResultadosConferidos(hashJogo.Count(), l));
            }
            return listaConferida;
        }

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

        public static bool QuadranteLinha(List<int> lista, List<string> parametros, string nomeLoteria)
        {
            int startNumIni = 0;
            int startNumFim = 0;
            int maxNumLinhas = 0;
            int numSoma = 0;

            switch (nomeLoteria)
            {
                case "LotoFacil":
                    startNumIni = 1;
                    startNumFim = 5;
                    maxNumLinhas = 5;
                    numSoma = 5;
                    break;

                case "MegaSena":
                    startNumIni = 1;
                    startNumFim = 10;
                    maxNumLinhas = 6;
                    numSoma = 10;
                    break;
            }

            if (parametros.Count() == 0)
            {
                return true;
            }
            List<int> listaResult = new List<int>();
            int numIni = startNumIni;
            int numFim = startNumFim;

            for (int i = 0; i < maxNumLinhas; i++)
            {
                IEnumerable<int> Query = from num in lista where num >= numIni && num <= numFim select num;
                listaResult.Add(Query.Count());
                numIni += numSoma;
                numFim += numSoma;
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
            return contaQL == maxNumLinhas ? true : false;
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
            parametros.Sort();
            if (!(parametros[0] >= 120 && parametros[1] <= 270))
            {
                throw new Exception("Soma das Dezenas não está no intervalo aceito [120 a 270];");
            }
            return lista.Sum() >= parametros[0] && lista.Sum() <= parametros[1] ? true : false;
        }

        public static bool MaiorSequencia(List<int> lista, int parametro)
        {
            if (parametro == 0)
            {
                return true;
            }

            lista.Sort();
            int nSequencia = 0;

            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista.Contains((lista[i]) + 1))
                    nSequencia++;
                else
                    nSequencia = 1;

                if (nSequencia > parametro)
                    return false;
            }
            return true;
        }

        public static bool MenorSequencia(List<int> lista, int parametro)
        {
            if (parametro == 0)
            {
                return true;
            }

            lista.Sort();
            int nSequencia = 0;

            for (int i = 0; i < lista.Count(); i++)
            {
                if (lista.Contains((lista[i]) + 1))
                    nSequencia++;
                else
                {
                    if (nSequencia < parametro)
                        return false;

                    nSequencia = 1;
                }
            }
            return true;
        }
    }
}
