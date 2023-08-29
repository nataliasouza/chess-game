using System.Collections.Generic;
using tabuleiro;

namespace xadrez {
    class PartidaDeXadrez {

        public ChessBoard tab { get; private set; }
        public int turno { get; private set; }
        public Collor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Piece> pecas;
        private HashSet<Piece> capturadas;
        public bool xeque { get; private set; }
        public Piece vulneravelEnPassant { get; private set; }

        public PartidaDeXadrez() {
            tab = new ChessBoard(8, 8);
            turno = 1;
            jogadorAtual = Collor.Branca;
            terminada = false;
            xeque = false;
            vulneravelEnPassant = null;
            pecas = new HashSet<Piece>();
            capturadas = new HashSet<Piece>();
            colocarPecas();
        }

        public Piece executaMovimento(Position origem, Position destino) {
            Piece p = tab.retirarPeca(origem);
            p.incrementarQteMovimentos();
            Piece pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is King && destino.coluna == origem.coluna + 2) {
                Position origemT = new Position(origem.linha, origem.coluna + 3);
                Position destinoT = new Position(origem.linha, origem.coluna + 1);
                Piece T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is King && destino.coluna == origem.coluna - 2) {
                Position origemT = new Position(origem.linha, origem.coluna - 4);
                Position destinoT = new Position(origem.linha, origem.coluna - 1);
                Piece T = tab.retirarPeca(origemT);
                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Pawn) {
                if (origem.coluna != destino.coluna && pecaCapturada == null) {
                    Position posP;
                    if (p.cor == Collor.Branca) {
                        posP = new Position(destino.linha + 1, destino.coluna);
                    }
                    else {
                        posP = new Position(destino.linha - 1, destino.coluna);
                    }
                    pecaCapturada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Position origem, Position destino, Piece pecaCapturada) {
            Piece p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null) {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);

            // #jogadaespecial roque pequeno
            if (p is King && destino.coluna == origem.coluna + 2) {
                Position origemT = new Position(origem.linha, origem.coluna + 3);
                Position destinoT = new Position(origem.linha, origem.coluna + 1);
                Piece T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is King && destino.coluna == origem.coluna - 2) {
                Position origemT = new Position(origem.linha, origem.coluna - 4);
                Position destinoT = new Position(origem.linha, origem.coluna - 1);
                Piece T = tab.retirarPeca(destinoT);
                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Pawn) {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant) {
                    Piece peao = tab.retirarPeca(destino);
                    Position posP;
                    if (p.cor == Collor.Branca) {
                        posP = new Position(3, destino.coluna);
                    }
                    else {
                        posP = new Position(4, destino.coluna);
                    }
                    tab.colocarPeca(peao, posP);
                }
            }
        }

        public void realizaJogada(Position origem, Position destino) {
            Piece pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new BoardException("Você não pode se colocar em xeque!");
            }

            Piece p = tab.peca(destino);

            // #jogadaespecial promocao
            if (p is Pawn) {
                if ((p.cor == Collor.Branca && destino.linha == 0) || (p.cor == Collor.Preta && destino.linha == 7)) {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Piece dama = new Lady(tab, p.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            }
            else {
                xeque = false;
            }

            if (testeXequemate(adversaria(jogadorAtual))) {
                terminada = true;
            }
            else {
                turno++;
                mudaJogador();
            }

            // #jogadaespecial en passant
            if (p is Pawn && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) {
                vulneravelEnPassant = p;
            }
            else {
                vulneravelEnPassant = null;
            }

        }

        public void validarPosicaoDeOrigem(Position pos) {
            if (tab.peca(pos) == null) {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor) {
                throw new BoardException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Position origem, Position destino) {
            if (!tab.peca(origem).movimentoPossivel(destino)) {
                throw new BoardException("Posição de destino inválida!");
            }
        }

        private void mudaJogador() {
            if (jogadorAtual == Collor.Branca) {
                jogadorAtual = Collor.Preta;
            }
            else {
                jogadorAtual = Collor.Branca;
            }
        }

        public HashSet<Piece> pecasCapturadas(Collor cor) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> pecasEmJogo(Collor cor) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Collor adversaria(Collor cor) {
            if (cor == Collor.Branca) {
                return Collor.Preta;
            }
            else {
                return Collor.Branca;
            }
        }

        private Piece rei(Collor cor) {
            foreach (Piece x in pecasEmJogo(cor)) {
                if (x is King) {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Collor cor) {
            Piece R = rei(cor);
            if (R == null) {
                throw new BoardException("Não tem rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Piece x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Collor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Piece x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i=0; i<tab.linhas; i++) {
                    for (int j=0; j<tab.colunas; j++) {
                        if (mat[i, j]) {
                            Position origem = x.posicao;
                            Position destino = new Position(i, j);
                            Piece pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Piece peca) {
            tab.colocarPeca(peca, new PositionChess(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeca('a', 1, new Tower(tab, Collor.Branca));
            colocarNovaPeca('b', 1, new Horse(tab, Collor.Branca));
            colocarNovaPeca('c', 1, new Bishop(tab, Collor.Branca));
            colocarNovaPeca('d', 1, new Lady(tab, Collor.Branca));
            colocarNovaPeca('e', 1, new King(tab, Collor.Branca, this));
            colocarNovaPeca('f', 1, new Bishop(tab, Collor.Branca));
            colocarNovaPeca('g', 1, new Horse(tab, Collor.Branca));
            colocarNovaPeca('h', 1, new Tower(tab, Collor.Branca));
            colocarNovaPeca('a', 2, new Pawn(tab, Collor.Branca, this));
            colocarNovaPeca('b', 2, new Pawn(tab, Collor.Branca, this));
            colocarNovaPeca('c', 2, new Pawn(tab, Collor.Branca, this));
            colocarNovaPeca('d', 2, new Pawn(tab, Collor.Branca, this));
            colocarNovaPeca('e', 2, new Pawn(tab, Collor.Branca, this));
            colocarNovaPeca('f', 2, new Pawn(tab, Collor.Branca, this));
            colocarNovaPeca('g', 2, new Pawn(tab, Collor.Branca, this));
            colocarNovaPeca('h', 2, new Pawn(tab, Collor.Branca, this));

            colocarNovaPeca('a', 8, new Tower(tab, Collor.Preta));
            colocarNovaPeca('b', 8, new Horse(tab, Collor.Preta));
            colocarNovaPeca('c', 8, new Bishop(tab, Collor.Preta));
            colocarNovaPeca('d', 8, new Lady(tab, Collor.Preta));
            colocarNovaPeca('e', 8, new King(tab, Collor.Preta, this));
            colocarNovaPeca('f', 8, new Bishop(tab, Collor.Preta));
            colocarNovaPeca('g', 8, new Horse(tab, Collor.Preta));
            colocarNovaPeca('h', 8, new Tower(tab, Collor.Preta));
            colocarNovaPeca('a', 7, new Pawn(tab, Collor.Preta, this));
            colocarNovaPeca('b', 7, new Pawn(tab, Collor.Preta, this));
            colocarNovaPeca('c', 7, new Pawn(tab, Collor.Preta, this));
            colocarNovaPeca('d', 7, new Pawn(tab, Collor.Preta, this));
            colocarNovaPeca('e', 7, new Pawn(tab, Collor.Preta, this));
            colocarNovaPeca('f', 7, new Pawn(tab, Collor.Preta, this));
            colocarNovaPeca('g', 7, new Pawn(tab, Collor.Preta, this));
            colocarNovaPeca('h', 7, new Pawn(tab, Collor.Preta, this));
        }
    }
}
