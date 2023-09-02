namespace tabuleiro {
    abstract class Piece {

        public Position posicao { get; set; }
        public Collor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public ChessBoard tab { get; protected set; }
         
        public Piece(ChessBoard tab, Collor cor) {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0;
        }

        public void incrementarQteMovimentos() {
            qteMovimentos++;
        }

        public void decrementarQteMovimentos() {
            qteMovimentos--;
        }

        public bool existeMovimentosPossiveis() {
            bool[,] mat = movimentosPossiveis();
            for (int i=0; i<tab.linhas; i++) {
                for (int j=0; j<tab.colunas; j++) {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool movimentoPossivel(Position pos) {
            return movimentosPossiveis()[pos.Line, pos.Column];
        }

        public abstract bool[,] movimentosPossiveis();
    }
}
