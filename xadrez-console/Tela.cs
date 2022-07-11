using tabuleiro;
using System;

namespace xadrez_console
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            //método para imprimir o tabuleiro na tela(criar uma matriz)
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    //imprimir a peça na posição i,j. caso nao tenha peça imprime um ifem -
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {

                        Console.Write(tab.peca(i, j) + " ");// acessa o obj tab, o metodo peca passa i,j como arguemento
                    }

                }
                Console.WriteLine();//para quebra a linha e printar a debaixo
            }
        }
    }
}
