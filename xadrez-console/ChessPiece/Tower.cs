using board;

namespace chessPiece {
    class Tower : Piece {

        public Tower(ChessBoard tab, Collor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "T";
        }

        private bool podeMover(Position pos) {
            Piece p = ChessBoard.ChessPiece(pos);
            return p == null || p.Collor != Collor;
        }

        public override bool[,] PossibleMoves() {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            // acima
            pos.SetValue(Position.Line - 1, Position.Column);
            while (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.Line = pos.Line - 1;
            }

            // abaixo
            pos.SetValue(Position.Line + 1, Position.Column);
            while (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.Line = pos.Line + 1;
            }

            // direita
            pos.SetValue(Position.Line, Position.Column + 1);
            while (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.Column = pos.Column + 1;
            }

            // esquerda
            pos.SetValue(Position.Line, Position.Column - 1);
            while (ChessBoard.PositionValid(pos) && podeMover(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
