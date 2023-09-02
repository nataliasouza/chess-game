using board;

namespace chessPiece 
{

    class Horse : Piece 
    {

        public Horse(ChessBoard tab, Collor cor) : base(tab, cor) 
        { }

        public override string ToString() 
        {
            return "C";
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

            pos.SetValue(Position.Line - 1, Position.Column - 2);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(Position.Line - 2, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(Position.Line - 2, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(Position.Line - 1, Position.Column + 2);
            if (ChessBoard.PositionValid(pos) && podeMover(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(Position.Line + 1, Position.Column + 2);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(Position.Line + 2, Position.Column + 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(Position.Line + 2, Position.Column - 1);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }
            pos.SetValue(Position.Line + 1, Position.Column - 2);
            if (ChessBoard.PositionValid(pos) && podeMover(pos)) 
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }
    }
}
