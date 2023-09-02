namespace board 
{
    class ChessBoard {

        public int Lines { get; set; }
        public int Columns { get; set; }
        private readonly Piece[,] Piece;

        public ChessBoard(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            Piece = new Piece[lines, columns]; 
        }

        public Piece ReturnPart(int line, int column)
        {
            return Piece[line, column];
        }

        public Piece ChessPiece(Position pos)
        {
            return Piece[pos.Line, pos.Column];
        }

        public bool ExistsPiece(Position pos)
        {
            ValidatePosition(pos);
            return ChessPiece(pos) != null;
        }

        public void InsertPart(Piece p, Position pos)
        {
            if (ExistsPiece(pos))
            {
                throw new BoardException("Já existe uma peça nessa posição!");
            }
            Piece[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePiece(Position pos) 
        {
            if (ChessPiece(pos) == null) 
            {
                return null;
            }
            Piece aux = ChessPiece(pos);
            aux.Position = null;
            Piece[pos.Line, pos.Column] = null;
            return aux;
        }

        public bool PositionValid(Position pos)
        {
            if (pos.Line<0 || pos.Line>=Lines || pos.Column<0 || pos.Column>=Columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!PositionValid(pos))
            {
                throw new BoardException("Posição inválida!");
            }
        }
    }
}
