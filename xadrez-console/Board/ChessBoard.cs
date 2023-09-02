namespace tabuleiro {
    class ChessBoard {

        public int linhas { get; set; }
        public int colunas { get; set; }
        private Piece[,] pecas;

        public ChessBoard(int linhas, int colunas) {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Piece[linhas, colunas]; 
        }

        public Piece peca(int linha, int coluna) {
            return pecas[linha, coluna];
        }

        public Piece peca(Position pos) {
            return pecas[pos.Line, pos.Column];
        }

        public bool existePeca(Position pos) {
            validarPosicao(pos);
            return peca(pos) != null;
        }

        public void colocarPeca(Piece p, Position pos) {
            if (existePeca(pos)) {
                throw new BoardException("Já existe uma peça nessa posição!");
            }
            pecas[pos.Line, pos.Column] = p;
            p.posicao = pos;
        }

        public Piece retirarPeca(Position pos) {
            if (peca(pos) == null) {
                return null;
            }
            Piece aux = peca(pos);
            aux.posicao = null;
            pecas[pos.Line, pos.Column] = null;
            return aux;
        }

        public bool posicaoValida(Position pos) {
            if (pos.Line<0 || pos.Line>=linhas || pos.Column<0 || pos.Column>=colunas) {
                return false;
            }
            return true;
        }

        public void validarPosicao(Position pos) {
            if (!posicaoValida(pos)) {
                throw new BoardException("Posição inválida!");
            }
        }
    }
}
