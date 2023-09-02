using board;

namespace chessPiece {

    class Lady : Piece {

        public Lady(ChessBoard tab, Collor cor) : base(tab, cor) {
        }

        public override string ToString() {
            return "D";
        }      

        public override bool[,] PossibleMoves() {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            // esquerda
            pos.SetValue(Position.Line, Position.Column - 1);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line, pos.Column - 1);
            }

            // direita
            pos.SetValue(Position.Line, Position.Column + 1);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line, pos.Column + 1);
            }

            // acima
            pos.SetValue(Position.Line - 1, Position.Column);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column);
            }

            // abaixo
            pos.SetValue(Position.Line + 1, Position.Column);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column);
            }

            // NO
            pos.SetValue(Position.Line - 1, Position.Column - 1);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column - 1);
            }

            // NE
            pos.SetValue(Position.Line - 1, Position.Column + 1);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column + 1);
            }

            // SE
            pos.SetValue(Position.Line + 1, Position.Column + 1);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column + 1);
            }

            // SO
            pos.SetValue(Position.Line + 1, Position.Column - 1);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}
