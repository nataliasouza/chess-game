using board;

namespace chessPiece {
    class King : Piece {

        private ChessGame partida;

        public King(ChessBoard tab, Collor cor, ChessGame partida) : base(tab, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "R";
        }

        private bool podeMover(Position pos) {
            Piece p = ChessBoard.ChessPiece(pos);
            return p == null || p.Collor != Collor;
        }

        private bool testeTorreParaRoque(Position pos) {
            Piece p = ChessBoard.ChessPiece(pos);
            return p != null && p is Tower && p.Collor == Collor && p.AmountOfMoves == 0;
        }

        public override bool[,] PossibleMoves() {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            // acima
            pos.SetValue(Position.Line - 1, Position.Column);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // ne
            pos.SetValue(Position.Line - 1, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // direita
            pos.SetValue(Position.Line, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // se
            pos.SetValue(Position.Line + 1, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // abaixo
            pos.SetValue(Position.Line + 1, Position.Column);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // so
            pos.SetValue(Position.Line + 1, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // esquerda
            pos.SetValue(Position.Line, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }
            // no
            pos.SetValue(Position.Line - 1, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
            }

            // #jogadaespecial roque
            if (AmountOfMoves==0 && !partida.CheckMate) {
                // #jogadaespecial roque pequeno
                Position posT1 = new Position(Position.Line, Position.Column + 3);
                if (testeTorreParaRoque(posT1)) {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);
                    if (ChessBoard.ChessPiece(p1)==null && ChessBoard.ChessPiece(p2)==null) {
                        mat[Position.Line, Position.Column + 2] = true;
                    }
                }
                // #jogadaespecial roque grande
                Position posT2 = new Position(Position.Line, Position.Column - 4);
                if (testeTorreParaRoque(posT2)) {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);
                    if (ChessBoard.ChessPiece(p1) == null && ChessBoard.ChessPiece(p2) == null && ChessBoard.ChessPiece(p3) == null) {
                        mat[Position.Line, Position.Column - 2] = true;
                    }
                }
            } 


            return mat;
        }
    }
}
