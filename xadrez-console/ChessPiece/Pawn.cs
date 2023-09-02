using board;

namespace chessPiece {

    class Pawn : Piece {

        private readonly ChessGame _match;

        public Pawn(ChessBoard board, Collor collor, ChessGame match) : base(board, collor) {
            _match = match;
        }

        public override string ToString() {
            return "P";
        }

        private bool IsThereAnEnemy(Position pos) {
            Piece p = ChessBoard.ChessPiece(pos);
            return p != null && p.Collor != Collor;
        }

        private bool Free(Position pos) {
            return ChessBoard.ChessPiece(pos) == null;
        }

        public override bool[,] PossibleMoves() {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            if (Collor == Collor.White) {
                pos.SetValue(Position.Line - 1, Position.Column);
                if (ChessBoard.PositionValid(pos) && Free(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line - 2, Position.Column);
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if (ChessBoard.PositionValid(p2) && Free(p2) && ChessBoard.PositionValid(pos) && Free(pos) && AmountOfMoves == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line - 1, Position.Column - 1);
                if (ChessBoard.PositionValid(pos) && IsThereAnEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line - 1, Position.Column + 1);
                if (ChessBoard.PositionValid(pos) && IsThereAnEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 3) {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (ChessBoard.PositionValid(left) && IsThereAnEnemy(left) && ChessBoard.ChessPiece(left) == _match.vulneravelEnPassant) {
                        mat[left.Line - 1, left.Column] = true;
                    }
                    Position rigth = new Position(Position.Line, Position.Column + 1);
                    if (ChessBoard.PositionValid(rigth) && IsThereAnEnemy(rigth) && ChessBoard.ChessPiece(rigth) == _match.vulneravelEnPassant) {
                        mat[rigth.Line - 1, rigth.Column] = true;
                    }
                }
            }
            else {
                pos.SetValue(Position.Line + 1, Position.Column);
                if (ChessBoard.PositionValid(pos) && Free(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line + 2, Position.Column);
                Position p2 = new Position(Position.Line + 1, Position.Column);
                if (ChessBoard.PositionValid(p2) && Free(p2) && ChessBoard.PositionValid(pos) && Free(pos) && AmountOfMoves == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line + 1, Position.Column - 1);
                if (ChessBoard.PositionValid(pos) && IsThereAnEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line + 1, Position.Column + 1);
                if (ChessBoard.PositionValid(pos) && IsThereAnEnemy(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 4) {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (ChessBoard.PositionValid(left) && IsThereAnEnemy(left) && ChessBoard.ChessPiece(left) == _match.vulneravelEnPassant) {
                        mat[left.Line + 1, left.Column] = true;
                    }
                    Position rigth = new Position(Position.Line, Position.Column + 1);
                    if (ChessBoard.PositionValid(rigth) && IsThereAnEnemy(rigth) && ChessBoard.ChessPiece(rigth) == _match.vulneravelEnPassant) {
                        mat[rigth.Line + 1, rigth.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
