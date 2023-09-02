using tabuleiro;

namespace xadrez {
    class King : Piece {

        private PartidaDeXadrez partida;

        public King(ChessBoard tab, Collor cor, PartidaDeXadrez partida) : base(tab, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "R";
        }

        private bool podeMover(Position pos) {
            Piece p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        private bool testeTorreParaRoque(Position pos) {
            Piece p = tab.peca(pos);
            return p != null && p is Tower && p.cor == cor && p.qteMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Position pos = new Position(0, 0);

            // acima
            pos.SetValue(posicao.Line - 1, posicao.Column);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // ne
            pos.SetValue(posicao.Line - 1, posicao.Column + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // direita
            pos.SetValue(posicao.Line, posicao.Column + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // se
            pos.SetValue(posicao.Line + 1, posicao.Column + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // abaixo
            pos.SetValue(posicao.Line + 1, posicao.Column);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // so
            pos.SetValue(posicao.Line + 1, posicao.Column - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // esquerda
            pos.SetValue(posicao.Line, posicao.Column - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // no
            pos.SetValue(posicao.Line - 1, posicao.Column - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // #jogadaespecial roque
            if (qteMovimentos==0 && !partida.xeque) {
                // #jogadaespecial roque pequeno
                Position posT1 = new Position(posicao.Line, posicao.Column + 3);
                if (testeTorreParaRoque(posT1)) {
                    Position p1 = new Position(posicao.Line, posicao.Column + 1);
                    Position p2 = new Position(posicao.Line, posicao.Column + 2);
                    if (tab.peca(p1)==null && tab.peca(p2)==null) {
                        mat[posicao.Line, posicao.Column + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Position posT2 = new Position(posicao.Line, posicao.Column - 4);
                if (testeTorreParaRoque(posT2)) {
                    Position p1 = new Position(posicao.Line, posicao.Column - 1);
                    Position p2 = new Position(posicao.Line, posicao.Column - 2);
                    Position p3 = new Position(posicao.Line, posicao.Column - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) {
                        mat[posicao.Line, posicao.Column - 2] = true;
                    }
                }
            } 


            return mat;
        }
    }
}
