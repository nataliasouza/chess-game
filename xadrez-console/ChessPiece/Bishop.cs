using board;

namespace chessPiece
{

    class Bishop : Piece {

        public Bishop(ChessBoard board, Collor collor) : base(board, collor)
        { }

        public override string ToString() 
        {
            return "B";
        }            
        
        public override bool[,] PossibleMoves() 
        {
            bool[,] mat = new bool[ChessBoard.Lines, ChessBoard.Columns];

            Position pos = new Position(0, 0);

            // NO
            pos.SetValue(Position.Line - 1, Position.Column - 1);
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) 
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
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) 
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
            while (ChessBoard.PositionValid(pos) && CanMove(pos)) 
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
            while (ChessBoard.PositionValid(pos) && CanMove(pos))
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
