using board;

namespace chessPiece
{

    class Bishop : Piece {

        public Bishop(ChessBoard tab, Collor cor) : base(tab, cor)
        { }

        public override string ToString() 
        {
            return "B";
        }

        private bool podeMover(Position pos)
        {
            Piece p = ChessBoard.ChessPiece(pos);
            return p == null || p.Collor != Collor;
        }
        
        public override bool[,] PossibleMoves() 
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            // NO
            pos.SetValue(Position.Line - 1, Position.Column - 1);
            while (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) 
                {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column - 1);
            }

            // NE
            pos.SetValue(Position.Line - 1, Position.Column + 1);
            while (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) 
                {
                    break;
                }
                pos.SetValue(pos.Line - 1, pos.Column + 1);
            }

            // SE
            pos.SetValue(Position.Line + 1, Position.Column + 1);
            while (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) 
                {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column + 1);
            }

            // SO
            pos.SetValue(Position.Line + 1, Position.Column - 1);
            while (ChessBoard.PositionValid(pos) && podeMover(pos))
            {
                mat[pos.Line, pos.Column] = true;
                if (ChessBoard.ChessPiece(pos) != null && ChessBoard.ChessPiece(pos).Collor != Collor) 
                {
                    break;
                }
                pos.SetValue(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }
    }
}
