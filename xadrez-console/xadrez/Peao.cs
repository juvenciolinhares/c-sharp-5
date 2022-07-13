using tabuleiro;

namespace xadrez
{
    internal class Peao : Peca
    {
        private PartidaDeXadrez partida;
        public Peao ( Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
             this.partida = partida;
        }
        public override string ToString()
        {
            return "P";
        }
        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }
        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];
            Posicao pos = new Posicao(0, 0);

            if(cor == Cor.Branca)
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && QuantidadeDeMovimentos == 0)//primeiro movimento
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna -1);//captura se existir um inimigo
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);//captura se existir um inimigo
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                
                //#Jogadaespecial En Passant(PEÕES BRANCOS):
                if(posicao.linha == 3)//o en passant da branca só acontece na linha 3
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);//posicao da peça vulnerável
                    //se a casa da esquedar é valida, se tem um inimigo,se a peça da esquerda é o peao que esta vulneravel
                    if(tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.linha -1 , esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    //se a casa da esquedar é valida, se tem um inimigo,se a peça da esquerda é o peao que esta vulneravel
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.linha -1, direita.coluna] = true;
                    }
                }

            }
            else
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && QuantidadeDeMovimentos == 0)//primeiro movimento
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);//captura se existir um inimigo
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);//captura se existir um inimigo
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                //#Jogadaespecial En Passant(PEÕES PRETOS):
                if (posicao.linha == 4)//o en passant da preta só acontece na linha 4
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    //se a casa da esquedar é valida, se tem um inimigo,se a peça da esquerda é o peao que esta vulneravel
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant)
                    {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    //se a casa da esquedar é valida, se tem um inimigo,se a peça da esquerda é o peao que esta vulneravel
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant)
                    {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }

            }
            return mat;
        }
    }
}
