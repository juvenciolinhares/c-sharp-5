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
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            //construtor inicia o tabuleiro, primeiro turno(jogada) e jogador com peças brancas joga primeiro
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        //metodo para executar movimento
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQuantidadeDeMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                //se eu capturar alguma peça, ela vai ser inserida no conjunto de pecas capturadas
                capturadas.Add(pecaCapturada);
            }

            //#jogada especia Roque pequeno:
            if(p is Rei && destino.coluna == origem.coluna + 2)//movi o rei 2 casas, tbm tenho que mexer a torre
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);//posicao origem da torre
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);//posicao destino da torre
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, destinoT);
            }
            //#jogada especia Roque GRANDE:
            if (p is Rei && destino.coluna == origem.coluna - 2)//movi o rei 2 casas, tbm tenho que mexer a torre
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);//posicao origem da torre
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);//posicao destino da torre
                Peca T = tab.retirarPeca(origemT);
                T.incrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            //retirar a peça da posição de destino:
            Peca p = tab.retirarPeca(destino);

            //decrementar qtd de movimentos:
            p.decrementarQuantidadeDeMovimentos();

            //se houve uma peça capturada, tenho que desfazer isso(colocar ela de volta)
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);//remover peça do conjunto das peças capturadas
            }
            tab.colocarPeca(p, origem);
            //#jogada especia Roque pequeno:
            if (p is Rei && destino.coluna == origem.coluna + 2)//movi o rei 2 casas, tbm tenho que mexer a torre
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);//posicao origem da torre
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);//posicao destino da torre
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, origemT);
            }
            //#jogada especia Roque GRANDE:
            if (p is Rei && destino.coluna == origem.coluna - 2)//movi o rei 2 casas, tbm tenho que mexer a torre
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);//posicao origem da torre
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);//posicao destino da torre
                Peca T = tab.retirarPeca(destinoT);
                T.incrementarQuantidadeDeMovimentos();
                tab.colocarPeca(T, origemT);
            }

        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executaMovimento(origem, destino);

            //verificar se com o movimento acima eu fiquei em xeque
            if (estaEmXeque(jogadorAtual))//se euntrei em xeque, tenho que desfazer a jogada
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");

            }

            if (estaEmXeque(adversaria(jogadorAtual)))//adversario em xeque com a minha jogada, isso pode
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                //passa o turno
                turno++;

                //inverter o jogador 
                mudaJogador();
            }
           
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
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");

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
            foreach (Peca x in capturadas)//percorrer tds peças capturadas
            {
                if (x.cor == cor)// se a cor da peça for igual a cor do parametro
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

        //peça adversária:
        private Cor adversaria(Cor cor)//cor adversária de uma cor entre parenteses
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;//se a peça dada for branca a preta é a adversaria
            }
            else
            {
                return Cor.Branca;//caso contrario a adversária é a branda
            }

        }

        //quem é o rei de determinada cor:
        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)//se x é uma instancia da classe rei
                {
                    return x;
                }
            }
            return null;//isso diz que não tem rei
        }

        //testa se o rei está em cheque levando em consideração os movimentos de todas as pecas adversárias
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);//rei da cor informada
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in pecasEmJogo(adversaria(cor)))//percorro as peças adversárias
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;//significa que x pode mover para o rei
                }
            }
            return false;//rei não está em cheque.
        }

        public bool testeXequemate(Cor cor)
        {
            if (!estaEmXeque(cor))//se o rei não está em xeque
            {
                return false;
            }

            foreach(Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])//posicao possivel pra peça x.
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);//faz o motimento
                            bool testeXeque = estaEmXeque(cor);//esta se ainda está em xeque
                            desfazMovimento(origem, destino, pecaCapturada);//desfaz o movimento
                            if (!testeXeque)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }

        //dado uma coluna, linha e peça, eu coloco isso no tabuleiro da partida
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);//adiciono essa peça colocada no conjunto de peças
        }

        private void colocarPecas()
        {
            //Instanciando todas as peças:
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('b', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('f', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('g', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeca('h', 2, new Peao(tab, Cor.Branca));

            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('b', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('f', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('g', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeca('h', 7, new Peao(tab, Cor.Preta));

        }

    }
}
