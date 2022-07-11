using tabuleiro;
namespace xadrez
{
    internal class PosicaoXadrez
    {
        //atributos
        public char coluna { get; set; }
        public int linha { get; set; }

        //construtor
        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }

        //converter a posição do xadrez para uma posicao interna da matriz:
        public Posicao toPosicao()
        {
            return new Posicao(8 - linha, coluna - 'a');
        }

        //to string:
        public override string ToString()
        {
            return "" + coluna + linha;
        }
    }
}
/*
 * no xadrez, a posição das peças é dispota da seguinte forma:
 * 
 * 8
 * 7
 * 6
 * 5
 * 4
 * 3
 * 2
 * 1
 *  a b c d e f g h
 *  
 *  (ou seja, uma letra e um inteiro)
 * 
 * ja a matriz instancia as linhas e colunas da seguinte forma:
 * 0 1 2 3 4 5 6 7 
 * 1
 * 2
 * 3
 * 4
 * 5
 * 6
 * 7
 * 
 * essa classe tem o objetivo de determinar a posição das peças igual ao xadrez
 */
