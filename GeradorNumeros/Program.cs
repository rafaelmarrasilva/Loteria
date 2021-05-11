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
            Parametros parametros = new Parametros();

            Console.Write("Informe a qtde de jogos desejados: ");
            int qtdJogos = int.Parse(Console.ReadLine());
            Console.WriteLine();
            
            Console.Write("Será utilizada alguma matriz [S/N]: ");
            string utilizaMatriz = Console.ReadLine();

            if (utilizaMatriz.ToUpper() == "S")
            {
                Console.Write("Informe os números (sep. virgula): ");
                string numMatriz = Console.ReadLine();

                foreach (var item in numMatriz.Split(','))
                {
                    parametros.addMatriz(int.Parse(item));
                }
            }
            Console.WriteLine();

            Console.Write("Informe quantos números pares o jogo deve ter (sep. virgula, caso sejá mais de um): ");
            string numPares = Console.ReadLine();
            foreach (var item in numPares.Split(','))
            {
                parametros.addPares(int.Parse(item));
            }
            Console.WriteLine();

            List<string> listQL = new List<string>();
            for (int i = 1; i < 6; i++)
            {
                Console.Write("Informe quantos números o jogo deve haver no quadrante linha "+ i + " (sep. virgula, caso sejá mais de um): ");
                string preencherQL = Console.ReadLine();
                parametros.addQuadLinha(preencherQL);
            }
            Console.WriteLine();

            List<string> listQC = new List<string>();
            for (int i = 1; i < 6; i++)
            {
                Console.Write("Informe quantos números o jogo deve haver no quadrante coluna " + i + " (sep. virgula, caso sejá mais de um): ");
                string preencherQC = Console.ReadLine();
                parametros.addQuadColuna(preencherQC);
            }
            Console.WriteLine();

            Console.Write("Informe o valor da menor e maior soma que o jogo pode conter (sep. virgula): ");
            string sumDezenas = Console.ReadLine();
            foreach (var item in sumDezenas.Split(','))
            {
                parametros.addSomaDezenas(int.Parse(item));
            }
            Console.WriteLine();

            var jogosGerados = GerarJogo.GerarJogos(qtdJogos, parametros);

            //inicio impressão dos jogos gerados
            foreach (Jogo jogos in jogosGerados)
            {
                Console.Write(jogos.Sequencial + " - " + jogos.Data + " - ");
                foreach (int num in jogos.ListaNumeros)
                {
                    if (jogos.ListaNumeros[jogos.ListaNumeros.Count - 1] != num)
                    {
                        Console.Write(num.ToString().PadLeft(2,'0') + ", ");
                    }
                    else
                    {
                        Console.Write(num.ToString().PadLeft(2,'0'));
                    }
                }
                Console.WriteLine();
            }
            //fim da impressão dos jogos gerados
        }
    }
}
