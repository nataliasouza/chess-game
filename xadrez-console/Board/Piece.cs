namespace board
{
    abstract class Piece {

        public Position Position { get; set; }
        public Collor Collor { get; protected set; }
        public int AmountOfMoves { get; protected set; }
        public ChessBoard ChessBoard { get; protected set; }
         
        public Piece(ChessBoard chessBoard, Collor cor)
        {
            Position = null;
            ChessBoard = chessBoard;
            Collor = cor;
            AmountOfMoves = 0;
        }

        public void IncreaseNumberOfMoves()
        {
            AmountOfMoves++;
        }

        public void DecreaseNumberOfMoves()
        {
            AmountOfMoves--;
        }

        public bool ThereArePossibleMoves()
        {
            bool[,] mat = PossibleMoves();
            for (int i=0; i< ChessBoard.Lines; i++)
            {
                for (int j=0; j< ChessBoard.Columns; j++)
                {
                    if (mat[i, j]) {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMoves(Position position)
        {
            return PossibleMoves()[position.Line, position.Column];
        }

        public abstract bool[,] PossibleMoves();
    }
}
