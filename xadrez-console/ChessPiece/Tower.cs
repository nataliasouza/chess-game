using tabuleiro;

namespace xadrez {
    class Tower : Piece {

        public Tower(ChessBoard tab, Collor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "T";
        }

        private bool podeMover(Position pos) {
            Piece p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Position pos = new Position(0, 0);

            // acima
            pos.SetValue(posicao.Line - 1, posicao.Column);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.Line = pos.Line - 1;
            }

            // abaixo
            pos.SetValue(posicao.Line + 1, posicao.Column);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            // direita
            pos.SetValue(posicao.Line, posicao.Column + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            // esquerda
            pos.SetValue(posicao.Line, posicao.Column - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
