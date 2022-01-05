using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loteria.Entities
{
    public static class GerarJogo
    {
        public static List<Jogo> GerarJogos(int numJogos, Parametros parametros)
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
                    Regras.QuadranteLinha(lista, parametros.QuadLinha) &&
                    Regras.QuadranteColuna(lista, parametros.QuadColuna) &&
                    Regras.SomaDezenas(lista, parametros.SomaDezenas) &&
                    Regras.MaiorSequencia(lista,parametros.MaiorSequencia) &&
                    Regras.MenorSequencia(lista,parametros.MenorSequencia) &&
                    Similaridade.Similar(lista, jogos)
                    )
                {
                    lista.Sort();
                    jogos.Add(new Jogo(i, DateTime.Now, lista));
                }
            }
            return jogos;
        }
    }
}
