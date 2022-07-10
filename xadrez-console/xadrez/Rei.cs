using tabuleiro;

namespace xadrez
{
    internal class Rei  : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor)//mesmo construtor da classe pai(peça)
        {
            //objeto rei repassa para o construtor da superclasse o tabuleiro(linhas,colunas) e a cor
        }

        public override string ToString()
        {
            return "R";// retorna o nome rei.(R)
        }
    }
}
