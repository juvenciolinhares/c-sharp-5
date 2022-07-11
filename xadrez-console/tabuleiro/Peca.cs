

namespace tabuleiro
{
    internal class Peca//classe genéria, peça qualquer(rainha, rei, bispo cavelo, etc.)
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
    }
}
