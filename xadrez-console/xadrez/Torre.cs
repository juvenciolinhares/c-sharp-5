using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor)
        {
            //objeto Torre repassa para o construtor da superclasse o tabuleiro(linhas,colunas) e a cor
        }

        public override string ToString()
        {
            return "T";// retorna o nome Torre(T)
        }
    }
}
