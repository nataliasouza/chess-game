using board;

namespace chessPiece {
    class PositionChess {

        public char Column { get; set; }
        public int Line { get; set; }

        public PositionChess(char coluna, int linha)
        {
            Column = coluna;
            Line = linha;
        }

        public Position ToPosition()
        {
            return new Position(8 - Line, Column - 'a');
        }

        public override string ToString()
        {
            return "" + Column + Line;
        }
    }
}
