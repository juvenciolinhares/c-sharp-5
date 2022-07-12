using tabuleiro;
using System;
using xadrez;

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
                    imprimirPeca(tab.peca(i, j));
                    
                }
                Console.WriteLine();//para quebra a linha e printar a debaixo
            }

            //colunas abcdefgh:
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            //mudar o fundo do caminho possivel que a peça pode ir:
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;


            //método para imprimir o tabuleiro na tela(criar uma matriz)
            for (int i = 0; i < tab.linhas; i++)
            {
                // linhas 87654321
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++)

                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    //imprimir a peça na posição i,j. caso nao tenha peça imprime um ifem -
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;

                }
                Console.WriteLine();//para quebra a linha e printar a debaixo
            }

            //colunas abcdefgh:
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;//garantir que vai voltar pro fundo original
        }

        //esse método vai ler do teclado o que o usuario digitar(exemplo: c,2)
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);


        }

        //cor da peça preta => AMARELA
        public static void imprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                //se a peça for branca eu imprimo a peça:
                if (peca.cor == Cor.Branca)
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
                Console.Write(" ");// para quebrar a linha e printar a debaixo
            }
        }
    }
}
