
namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca [,] pecas;// matriz de pecas.só pode ser acessada pela própria classe

        public Tabuleiro(int linhas, int colunas)
        {
            //dizer a qtd de linhas e colunas do tabuleiro
            this.linhas = linhas;
            this.colunas = colunas;

            //criar a matriz de peças que vai ter esse numero de linhas por esse número de colunas informados no argumento.
            pecas = new Peca[linhas, colunas];
        }

        //criar um método pra dar acesso a uma peça individual  do tabuleiro
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
    }
}
