using tabuleiro;

namespace xadrez {

    class Horse : Piece {

        public Horse(ChessBoard tab, Collor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "C";
        }

        private bool podeMover(Position pos) {
            Piece p = tab.peca(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] movimentosPossiveis() {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Position pos = new Position(0, 0);

            pos.SetValue(posicao.Line - 1, posicao.Column - 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(posicao.Line - 2, posicao.Column - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(posicao.Line - 2, posicao.Column + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(posicao.Line - 1, posicao.Column + 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(posicao.Line + 1, posicao.Column + 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(posicao.Line + 2, posicao.Column + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(posicao.Line + 2, posicao.Column - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(posicao.Line + 1, posicao.Column - 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}
