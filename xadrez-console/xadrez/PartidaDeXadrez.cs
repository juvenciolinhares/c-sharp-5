using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    internal class PartidaDeXadrez
    {
        //atributos:
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }//cada jogada 
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            //construtor inicia o tabuleiro, primeiro turno(jogada) e jogador com peças brancas joga primeiro
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        //metodo para executar movimento
        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQuantidadeDeMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                //se eu capturar alguma peça, ela vai ser inserida no conjunto de pecas capturadas
                capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executaMovimento(origem, destino);
            turno++;

            //inverter o jogador 
            mudaJogador();
        }

        //testar se a posição de origem é válida:
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)//se não existe peça naquela posição
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)// só posso escolher a peça correrpondente a jogador atual
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())//se a peça esta bloqueada(não existe movimente(jogada) possivel)
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem esolhida");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            //se a peça de origem nao pode mover para a posicao de destino
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posião de destino inválida");

            }
        }

        //inverter o jogador se for branco passa pra preto, vice-versa
        private void mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        //metodo p informar as peças capturadas de determinada cor
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();//conjunto temporario.
            foreach(Peca x in capturadas)//percorrer tds peças capturadas
            {
                if(x.cor == cor)// se a cor da peça for igual a cor do parametro
                {
                    aux.Add(x);//add essa peça no conjunto aux
                }
            }
            return aux;// retorna o conjunto 

        }

        //metodo pra deixar em jogo apenas peças que nao foram capturadas( aux.ExceptWith(pecasCapturadas(cor));)
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();//conjunto temporario.
            foreach (Peca x in pecas)//percorrer tds peças 
            {
                if (x.cor == cor)// se a cor da peça for igual a cor do parametro
                {
                    aux.Add(x);//add essa peça no conjunto aux
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));//retirar todas as peças capturadas dessa mesma cor
            return aux;
        }

        //dado uma coluna, linha e peça, eu coloco isso no tabuleiro da partida
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);//adiciono essa peça colocada no conjunto de peças
        }

        private void colocarPecas()
        {
            //refatorado método colocar peças:
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }

    }
}
