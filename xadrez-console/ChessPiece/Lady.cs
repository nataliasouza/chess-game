using tabuleiro;

namespace xadrez {

    class Lady : Piece {

        public Lady(ChessBoard tab, Collor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "D";
        }

        private bool podeMover(Position pos) {
            Piece p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Position pos = new Position(0, 0);

            // esquerda
            pos.SetValue(posicao.Line, posicao.Column - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line, pos.Column - 1);
            }

            // direita
            pos.SetValue(posicao.Line, posicao.Column + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line, pos.Column + 1);
            }

            // acima
            pos.SetValue(posicao.Line - 1, posicao.Column);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column);
            }

            // abaixo
            pos.SetValue(posicao.Line + 1, posicao.Column);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column);
            }

            // NO
            pos.SetValue(posicao.Line - 1, posicao.Column - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column - 1);
            }

            // NE
            pos.SetValue(posicao.Line - 1, posicao.Column + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column + 1);
            }

            // SE
            pos.SetValue(posicao.Line + 1, posicao.Column + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column + 1);
            }

            // SO
            pos.SetValue(posicao.Line + 1, posicao.Column - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}
