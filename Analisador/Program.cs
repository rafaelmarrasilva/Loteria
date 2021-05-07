﻿using System;
using System.IO;
using System.Collections.Generic;
using Analisador.Entities;
using System.Linq;

namespace Analisador
{
    class Program
    {
        static void Main(string[] args)
        {
            //Carregar resultados do arquivo texto para analise.
            Console.Write("Informe o caminho do arquivo para a importação dos resultados: ");
            string path = Console.ReadLine();
            var resultados = Preencher.CarregarResultados(path);

            Console.Write("Informe quantos concursos deseja analisar: ");
            int nConcAnalise = int.Parse(Console.ReadLine());

            List<Combinacao> listaCombinacoes = new List<Combinacao>();
            int pFrequencia = -1;

            Console.WriteLine();
            Console.WriteLine("Analise Combinatório Simples");
            Console.Write("Deseja gerar um único conjuto de elementos Exp.: 1,5,7,15,24 ? [S/N]: ");
            string vSNConjunto = Console.ReadLine();

            if (vSNConjunto.ToUpper() == "S")
            {
                Console.Write("Informe os elementos do conjunto (separados por vírgula): ");
                string pConjunto = Console.ReadLine();

                List<int> listaConjunto = new List<int>();
                foreach (var item in pConjunto.Split(","))
                {
                    listaConjunto.Add(int.Parse(item));
                }

                listaCombinacoes.Add(new Combinacao(pConjunto, listaConjunto));

                Console.WriteLine("Será exibida a quantidade de vezes que essa combinação saiu nos últimos " + nConcAnalise + " resultados.");
                Console.Write("Tecle Enter para continuar");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Qual será o tamanho do conjunto (Analise Combinatório Simples). ");
                Console.WriteLine("Exemplo: 5 gera 53130 combinações, 2 gera 300 combinações.");
                Console.WriteLine();
                Console.Write("Infome o tamanho do conjunto desejado: ");
                int nElementos = int.Parse(Console.ReadLine());
                
                Console.Write("Informe qual é o valor minimo de repetições deseja visualizar para cada conjuto: ");
                pFrequencia = int.Parse(Console.ReadLine());

                listaCombinacoes = Preencher.GerarCombinacoes(nElementos);
            }
            Console.WriteLine();

            //Comprar as combinações geradas com os resultados importados
            Preencher.ComparaCombinacoesResultados(listaCombinacoes, pFrequencia, resultados, nConcAnalise);

            //Analise dos numeros Pares
            Preencher.QtdPares(resultados, nConcAnalise);

            //Analise dos Quadrantes por Linha
            Preencher.QuadrantesLinha(resultados, nConcAnalise);

            //Analise dos Quadrantes por Coluna
            Preencher.QuadrantesColuna(resultados, nConcAnalise);

            //Analise da Soma das Dezenas
            Preencher.SomaDezenas(resultados, nConcAnalise);

            //Analise dos numeros que mais foram sorteados
            Preencher.CincoMaisDosUltDoze(resultados);

            //Analise dos numeros que mais foram sorteados
            Preencher.CincoMenosDosUltDoze(resultados);
        }
    }
}