using board;

namespace chessPiece {

    class Pawn : Piece {

        private ChessGame partida;

        public Pawn(ChessBoard tab, Collor cor, ChessGame partida) : base(tab, cor) {
            this.partida = partida;
        }

        public override string ToString() {
            return "P";
        }

        private bool existeInimigo(Position pos) {
            Piece p = ChessBoard.ChessPiece(pos);
            return p != null && p.Collor != Collor;
        }

        private bool livre(Position pos) {
            return ChessBoard.ChessPiece(pos) == null;
        }

        public override bool[,] PossibleMoves() {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            if (Collor == Collor.White) {
                pos.SetValue(Position.Line - 1, Position.Column);
                if (ChessBoard.PositionValid(pos) && livre(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line - 2, Position.Column);
                Position p2 = new Position(Position.Line - 1, Position.Column);
                if (ChessBoard.PositionValid(p2) && livre(p2) && ChessBoard.PositionValid(pos) && livre(pos) && AmountOfMoves == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line - 1, Position.Column - 1);
                if (ChessBoard.PositionValid(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line - 1, Position.Column + 1);
                if (ChessBoard.PositionValid(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 3) {
                    Position esquerda = new Position(Position.Line, Position.Column - 1);
                    if (ChessBoard.PositionValid(esquerda) && existeInimigo(esquerda) && ChessBoard.ChessPiece(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.Line - 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(Position.Line, Position.Column + 1);
                    if (ChessBoard.PositionValid(direita) && existeInimigo(direita) && ChessBoard.ChessPiece(direita) == partida.vulneravelEnPassant) {
                        mat[direita.Line - 1, direita.Column] = true;
                    }
                }
            }
            else {
                pos.SetValue(Position.Line + 1, Position.Column);
                if (ChessBoard.PositionValid(pos) && livre(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line + 2, Position.Column);
                Position p2 = new Position(Position.Line + 1, Position.Column);
                if (ChessBoard.PositionValid(p2) && livre(p2) && ChessBoard.PositionValid(pos) && livre(pos) && AmountOfMoves == 0) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line + 1, Position.Column - 1);
                if (ChessBoard.PositionValid(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }
                pos.SetValue(Position.Line + 1, Position.Column + 1);
                if (ChessBoard.PositionValid(pos) && existeInimigo(pos)) {
                    mat[pos.Line, pos.Column] = true;
                }

                // #jogadaespecial en passant
                if (Position.Line == 4) {
                    Position esquerda = new Position(Position.Line, Position.Column - 1);
                    if (ChessBoard.PositionValid(esquerda) && existeInimigo(esquerda) && ChessBoard.ChessPiece(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.Line + 1, esquerda.Column] = true;
                    }
                    Position direita = new Position(Position.Line, Position.Column + 1);
                    if (ChessBoard.PositionValid(direita) && existeInimigo(direita) && ChessBoard.ChessPiece(direita) == partida.vulneravelEnPassant) {
                        mat[direita.Line + 1, direita.Column] = true;
                    }
                }
            }

            return mat;
        }
    }
}
