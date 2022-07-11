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
                // linhas 87654321
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    //imprimir a peça na posição i,j. caso nao tenha peça imprime um ifem -
                    if (tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i, j));
                        Console.Write(" ");// acessa o obj tab, o metodo peca passa i,j como arguemento
                    }

                }
                Console.WriteLine();//para quebra a linha e printar a debaixo
            }

            //colunas abcdefgh:
            Console.WriteLine("  a b c d e f g h");
        }

        //cor da peça preta => AMARELA
        public static void imprimirPeca(Peca peca)
        {
            //se a peça for branca eu imprimo a peça:
            if(peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                //se a peça for preta, imprime como amarelo
                ConsoleColor aux = Console.ForegroundColor;//salvando foreground color em aux
                Console.ForegroundColor = ConsoleColor.Yellow;//mudo a cor
                Console.Write(peca);//imprimo em amarelo
                Console.ForegroundColor = aux;//volto pra cor original
            }
        }
    }
}
