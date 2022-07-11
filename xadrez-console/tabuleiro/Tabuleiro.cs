
namespace tabuleiro
{
    internal class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        private Peca[,] pecas;// matriz de pecas.só pode ser acessada pela própria classe

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

        //sobrecarga do método peça recebendo uma posição pós
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        //metodo pra testar se existe uma peça em determinada posição:
        public bool existepeca(Posicao pos)
        {
            validarPosicao(pos);//antes de ver se tem a peça, preciso validar a posição da possivel peça. 
            return peca(pos) != null;
        }

        //metodo que coloca peças no tabuleiro
        public void colocarPeca(Peca p, Posicao pos)
        {
            //teste pra ver se ja existe uma peça:
            if (existepeca(pos))
            {
                throw new TabuleiroException("Ja existe uma peça nessa posição!");
            }
            pecas[pos.linha, pos.coluna] = p;// jogando a peça p na matriz
            p.posicao = pos;//posição da peça p agora é pos. 

        }

        //retirar uma peça:
        public Peca retirarPeca(Posicao pos)
        {
            //se não tiver peça(==null) retorna null;
            if (peca(pos) == null)
            {
                return null;
            }

            // se tiver peça, cria uma var auxiliar recebendo a peça informada
            Peca aux = peca(pos);
            //aux nula = foi retirada do tabuleiro
            aux.posicao = null; 

            // a posição que estava essa peça vai ser nula
            pecas[pos.linha, pos.coluna] = null;
            return aux;

        }

        //metodo pra testar se a posição pos é válida ou não:
        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }

        //validar posição(lança uma exceção):
        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))//se a posição pos não for válida, lançar uma exceção
            {
                throw new TabuleiroException("Posição inválida");
            }
        }
    }
}
