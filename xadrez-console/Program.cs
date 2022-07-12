using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
               PartidaDeXadrez partida = new PartidaDeXadrez();
                while (!partida.terminada)
                {
                    try
                    {
                        Console.Clear();//limpar tela
                        Tela.imprimirPartida(partida);


                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                        //validar posição:
                        partida.validarPosicaoDeOrigem(origem);

                        //destacar possições possiveis da peça:
                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis();

                        Console.Clear();//limpar tela
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);//passoa a matriz como argumento

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();
                        partida.validarPosicaoDeDestino(origem, destino);

                        partida.realizaJogada(origem, destino);
                    }
                    catch(TabuleiroException e)
                    {
                        //se der erro, mostra a msg e o cw tabtab é pra esperar ele apertar enter e terminar a jogada
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
