using tabuleiro;

namespace xadrez {

    class Pawn : Piece {

        private PartidaDeXadrez partida;

        public Pawn(ChessBoard tab, Collor cor, PartidaDeXadrez partida) : base(tab, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "P";
        }

        private bool existeInimigo(Position pos) {
            Piece p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Position pos) {
            return tab.peca(pos) == null;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Position pos = new Position(0, 0);

            if (cor == Collor.Branca) {
                pos.SetValue(posicao.Line - 1, posicao.Column);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(posicao.Line - 2, posicao.Column);
                Position p2 = new Position(posicao.Line - 1, posicao.Column);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(posicao.Line - 1, posicao.Column - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(posicao.Line - 1, posicao.Column + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (posicao.Line == 3) {
                    Position esquerda = new Position(posicao.Line, posicao.Column - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.Line - 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(posicao.Line, posicao.Column + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.Line - 1, direita.Column] = true;
                    }
                }
            }
            else {
                pos.SetValue(posicao.Line + 1, posicao.Column);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(posicao.Line + 2, posicao.Column);
                Position p2 = new Position(posicao.Line + 1, posicao.Column);
                if (tab.posicaoValida(p2) && livre(p2) && tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(posicao.Line + 1, posicao.Column - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(posicao.Line + 1, posicao.Column + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (posicao.Line == 4) {
                    Position esquerda = new Position(posicao.Line, posicao.Column - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.Line + 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(posicao.Line, posicao.Column + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.Line + 1, direita.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
