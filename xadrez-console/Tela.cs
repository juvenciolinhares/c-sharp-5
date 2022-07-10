using tabuleiro;
using System;
namespace xadrez_console
{
    internal class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            //método para imprimir o tabuleiro na tela
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        //imprimir a peça na posição i,j. caso nao tenha peça imprime um ifem -
                        Console.Write(tab.peca(i, j) + " ");// acessa o obj tab, o metodo peca passa i,j como arguemento
                    }

                }
                Console.WriteLine();//para quebra a linha e printar a debaixo
            }
        }
    }
}
