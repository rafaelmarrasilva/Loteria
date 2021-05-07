using System;
using System.Collections.Generic;
using System.Linq;
using Loteria.Entities;

namespace Loteria
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Informe a qtde de jogos desejados: ");
            int qtdJogos = int.Parse(Console.ReadLine());
            Console.WriteLine("-----------------------------------");

            List<JogoGerado> listaJG = new List<JogoGerado>();

            for (int i = 1; i <= qtdJogos; i++)
            {
                listaJG.Add(new JogoGerado(i, DateTime.Now));
            }

            Similaridade.Similar(listaJG,1);
            Console.WriteLine("-----------------------------------");

            /*impressão dos jogos gerados*/
            foreach (var jogo in listaJG)
            {
                Console.Write(jogo.Sequencial + " - " + jogo.Data + " - ");
                foreach (var num in jogo.ListaNumeros)
                {
                    if (jogo.ListaNumeros[jogo.ListaNumeros.Count - 1] != num)
                    {
                        Console.Write(num.ToString() + ", ");
                    }
                    else
                    {
                        Console.Write(num.ToString());
                    }
                }
                Console.WriteLine();
                Console.WriteLine("-----------------------------------");
            }
        }
    }
}
