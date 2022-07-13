using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca
    {
        //para que o rei tenha acesso à partida:
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)//mesmo construtor da classe pai(peça)
        {
           this.partida = partida;
        }

        public override string ToString()
        {
            return "R";// retorna o nome rei.(R)
        }


        //testar se o rei pode mover pra determinada posição:
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor; //significa que a casa ta livre pro rei se mover ou a peça é adversária(cor diferente)

        }

        //testar se uma torre pode fazer roque
        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.QuantidadeDeMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis()
        {
            //instanciar uma matriz
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            //crio uma posição
            Posicao pos = new Posicao(0, 0);

            //verificando as posições:

            //acima: uma linha a menos, mesma coluna
            pos.definirValores(posicao.linha - 1, posicao.coluna);

            //testar posição:
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;


            }

            //nordeste: 
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //direita: 
            pos.definirValores(posicao.linha, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //sudeste: 
            pos.definirValores(posicao.linha = + 1, posicao.coluna + 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //abaixo: 
            pos.definirValores(posicao.linha + 1, posicao.coluna);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //sudoeste: 
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //esquerda: 
            pos.definirValores(posicao.linha, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //noroeste: 
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);

            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //#Jogadaespecial roque pequeno

            if(QuantidadeDeMovimentos == 0 && !partida.xeque)//testar se o rei não mexeu e não está em xeque
            {
                
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);//verificando a posição da torre(tem que ser 3 colunas p direita)
                if (testeTorreParaRoque(posT1))
                {
                    //essas duas posições têm que estar vazias:
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if(tab.peca(p1) == null && tab.peca(p2) == null)
                    {
                        mat[posicao.linha, posicao.coluna + 2] = true; 
                    }
                }
                //#JOGADA ESPECIAL ROQUE GRANDE:
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4 );//verificando a posição da torre(tem que ser 3 colunas p direita)
                if (testeTorreParaRoque(posT2))
                {
                    //essas duas posições têm que estar vazias:
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);

                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null)
                    {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }

            }
            return mat;

        }
    }
}
