

namespace tabuleiro
{
    internal class Peca
    {
        public Posicao posicao { get; set; }// associação com a classe posicao
        public Cor cor { get; protected set; }// protected: alterada apenas pelas classes e subclasses
        public int QuantidadeDeMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }
        public Peca(Posicao posicao, Tabuleiro tab, Cor cor)
        {
            this.posicao = posicao;
            this.tab = tab;
            this.cor = cor;
            this.QuantidadeDeMovimentos = 0;// quando a peça é criada, tem 0 movimentos

        }
    }
}
