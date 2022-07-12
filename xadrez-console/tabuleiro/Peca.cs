

namespace tabuleiro
{
    abstract class Peca//classe genéria, peça qualquer(rainha, rei, bispo cavelo, etc.)
    {
        public Posicao posicao { get; set; }// associação com a classe posicao
        public Cor cor { get; protected set; }// protected: alterada apenas pelas classes e subclasses
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }
        public Peca(Tabuleiro tab, Cor cor)
        {
            this.posicao = null;// quando crio uma peça, ela ainda nao tem posição
            this.tab = tab;
            this.cor = cor;
            this.QuantidadeDeMovimentos = 0;// quando a peça é criada, tem 0 movimentos

        }

        public void incrementarQuantidadeDeMovimentos()
        {
            QuantidadeDeMovimentos++;
        }

        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;//se retorna falso é pq passou pelo for e não tem nenhum movimento possivel
        }

        //testar se posso mover para determinada posição
       public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }
        //essa clase vai ser implementada na classe de cada peça(rei, torre, etc.) pra dar o moviemto especifico de cada uma
        public abstract bool[,] movimentosPossiveis();

    }
}
