using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)//mesmo construtor da classe pai(peça)
        {
            //objeto Torre repassa para o construtor da superclasse o tabuleiro(linhas,colunas) e a cor
        }

        public override string ToString()
        {
            return "T";// retorna o nome Torre(T)
        }
        //testar se o rei pode mover pra determinada posição:
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor; //significa que a casa ta livre pro rei se mover ou a peça é adversária(cor diferente)

        }

        public override bool[,] movimentosPossiveis()
        {
            //instanciar uma matriz
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            //crio uma posição
            Posicao pos = new Posicao(0, 0);

            //acima: enquanto tiver casa livre ou peça advsersária posso mover a torre
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;

                //obs se eu bato numa peça adversária, tenho que parar:
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    break;//paro de mover
                }
                //caso não haja peça adversária:
                pos.linha = pos.linha - 1;//estou indo para a proxima posição acima
            }

            //abaixo: 
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;

                //obs se eu bato numa peça adversária, tenho que parar:
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    break;//paro de mover
                }
                //caso não haja peça adversária:
                pos.linha = pos.linha + 1;//estou indo para a proxima posição abaixo
            }

            //a direita: 
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;

                //obs se eu bato numa peça adversária, tenho que parar:
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    break;//paro de mover
                }
                //caso não haja peça adversária:
                pos.coluna = pos.coluna + 1;//estou indo para a direita
            }

            //a esquerda: 
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;

                //obs se eu bato numa peça adversária, tenho que parar:
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor)
                {
                    break;//paro de mover
                }
                //caso não haja peça adversária:
                pos.coluna = pos.coluna - 1;//estou indo para a esquerda
            }

            return mat;

        }
    }
}
