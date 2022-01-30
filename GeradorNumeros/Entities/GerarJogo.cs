using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class GerarJogo
    {
        public static List<Jogo> GerarJogos(int numJogos, Parametros parametros, List<Resultado> listResultados)
        {
            List<Jogo> jogos = new List<Jogo>();
            int numero;

            for (int i = 1; numJogos > jogos.Count(); i++)
            {
                List<int> lista = new List<int>();
                if (parametros.Matriz.Count() > 0)
                {
                    foreach (var item in parametros.Matriz)
                    {
                        lista.Add(item);
                    }
                }
                while (lista.Count() < 15)
                {
                    numero = new Random().Next(26);
                    if (!lista.Contains(numero) && numero != 0)
                    {
                        lista.Add(numero);
                    }
                }
                //Validar regra dos numeros pares
                //Validar regra dos quadrante linha
                //Só gerar o jogo se todos os parametros forem verdadeiros.
                if (Regras.Pares(lista, parametros.Pares) &&
                    Regras.QuadranteLinha(lista, parametros.QuadLinha, "LotoFacil") &&
                    Regras.QuadranteColuna(lista, parametros.QuadColuna) &&
                    Regras.SomaDezenas(lista, parametros.SomaDezenas) &&
                    Regras.MaiorSequencia(lista, parametros.MaiorSequencia) &&
                    Regras.MenorSequencia(lista, parametros.MenorSequencia) &&
                    Similaridade.ResultadosAnteriores(lista, listResultados, 14) &&
                    Similaridade.Similar(lista, jogos, 14)
                    )
                {
                    lista.Sort();
                    jogos.Add(new Jogo(i, DateTime.Now, lista));
                }
            }
            return jogos;
        }

        public static List<Jogo> GerarJogosPote(int numJogos, Parametros parametros, List<Resultado> listResultados)
        {
            List<Jogo> jogos = new List<Jogo>();

            int numMais = 7;
            bool vSort = false;

            for (int i = 1; numJogos > jogos.Count(); i++)
            {
                List<int> listaPote1 = new List<int>();
                List<int> listaPote2 = new List<int>();
                List<int> l = new List<int>();

                foreach (KeyValuePair<int, int> item in Estatisticas.NumMaisSorteados(listResultados, numMais).Take(15))
                {
                    listaPote1.Add(item.Key);
                }

                foreach (KeyValuePair<int, int> item in Estatisticas.NumMaisSorteados(listResultados, numMais).Skip(15))
                {
                    listaPote2.Add(item.Key);
                }

                if (vSort)
                {
                    listaPote1.Sort();
                    listaPote2.Sort();
                }

                while (l.Count() < 10)
                {
                    int num = new Random().Next(15);
                    if (!l.Contains(listaPote1[num]))
                        l.Add(listaPote1[num]);
                }

                while (l.Count() < 15)
                {
                    int num = new Random().Next(10);
                    if (!l.Contains(listaPote2[num]))
                        l.Add(listaPote2[num]);
                }

                if (Similaridade.ResultadosAnteriores(l, listResultados, 14) &&
                    Similaridade.Similar(l, jogos, 14) &&
                    Regras.MaiorSequencia(l, parametros.MaiorSequencia))
                {
                    l.Sort();
                    jogos.Add(new Jogo(i, DateTime.Now, l));
                    numMais += 7;
                    vSort = vSort == true ? false : true;
                }
            }
            return jogos;
        }

        public static List<Jogo> GerarJogosMegaSena(int numJogos, int qtdDezenas, Parametros parametros, List<Resultado> listResultados)
        {
            List<Jogo> jogos = new List<Jogo>();

            for (int i = 1; numJogos > jogos.Count(); i++)
            {
                List<int> l = new List<int>();

                while (l.Count() < qtdDezenas)
                {
                    int num = new Random().Next(0, 61);
                    if (!l.Contains(num))
                        l.Add(num);
                }

                if (Similaridade.ResultadosAnteriores(l, listResultados, 4) &&
                    Similaridade.Similar(l, jogos, 4) && 
                    Regras.QuadranteLinha(l, parametros.QuadLinha, "MegaSena"))
                {
                    l.Sort();
                    jogos.Add(new Jogo(i, DateTime.Now, l));
                }
            }
            return jogos;
        }

        public static List<Jogo> GerarJogosPoteMegaSena(int numJogos, List<int> listaPote1, int qtdNumP1, List<int> listaPote2, int qtdNumP2, List<Resultado> listResultados)
        {
            bool vSortDesc = false;

            if (listaPote2.Count() == 0)
            {
                for (int i = 1; i <= 60; i++)
                {
                    if (!listaPote1.Contains(i))
                        listaPote2.Add(i);
                }
            }

            List<Jogo> jogos = new List<Jogo>();

            listaPote1.Sort();
            listaPote2.Sort();

            if (vSortDesc)
            {
                listaPote1 = listaPote1.OrderByDescending(i => i).ToList();
                listaPote2 = listaPote1.OrderByDescending(i => i).ToList();
            }

            for (int i = 1; numJogos > jogos.Count(); i++)
            {
                List<int> l = new List<int>();

                while (l.Count() < qtdNumP1)
                {
                    Thread.Sleep(3000);
                    int num = new Random().Next(listaPote1.Count());
                    if (!l.Contains(listaPote1[num]))
                        l.Add(listaPote1[num]);
                }

                while (l.Count() < qtdNumP2)
                {
                    int num = new Random().Next(listaPote2.Count());
                    if (!l.Contains(listaPote2[num]))
                        l.Add(listaPote2[num]);
                }

                if (Similaridade.ResultadosAnteriores(l, listResultados, 4) && Similaridade.Similar(l, jogos, 4))
                {
                    l.Sort();
                    jogos.Add(new Jogo(i, DateTime.Now, l));
                    vSortDesc = vSortDesc == true ? false : true;
                }
            }
            return jogos;
        }

    }
}
