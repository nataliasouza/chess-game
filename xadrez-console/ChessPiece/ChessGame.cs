using System.Collections.Generic;
using board;

namespace chessPiece {
    class ChessGame {

        public ChessBoard Board { get; private set; }
        public int Shift { get; private set; }
        public Collor CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private readonly HashSet<Piece> _pieces;
        private readonly HashSet<Piece> _captured;
        public bool CheckMate { get; private set; }
        public Piece vulneravelEnPassant { get; private set; }

        public ChessGame() {
            Board = new ChessBoard(8, 8);
            Shift = 1;
            CurrentPlayer = Collor.White;
            Finished = false;
            CheckMate = false;
            vulneravelEnPassant = null;
            _pieces = new HashSet<Piece>();
            _captured = new HashSet<Piece>();
            PlacePart();
        }

        public Piece ExecuteMovement(Position origin, Position destiny) {
            Piece p = Board.RemovePiece(origin);
            p.IncreaseNumberOfMoves();
            Piece capturedPart = Board.RemovePiece(destiny);
            Board.InsertPart(p, destiny);
            if (capturedPart != null) {
                _captured.Add(capturedPart);
            }

            // #jogadaespecial roque pequeno
            if (p is King && destiny.Column == origin.Column + 2) {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(originT);
                T.IncreaseNumberOfMoves();
                Board.InsertPart(T, destinyT);
            }

            // #jogadaespecial roque grande
            if (p is King && destiny.Column == origin.Column - 2) {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(originT);
                T.IncreaseNumberOfMoves();
                Board.InsertPart(T, destinyT);
            }

            // #jogadaespecial en passant
            if (p is Pawn) {
                if (origin.Column != destiny.Column && capturedPart == null) {
                    Position posP;
                    if (p.Collor == Collor.White) {
                        posP = new Position(destiny.Line + 1, destiny.Column);
                    }
                    else {
                        posP = new Position(destiny.Line - 1, destiny.Column);
                    }
                    capturedPart = Board.RemovePiece(posP);
                    _captured.Add(capturedPart);
                }
            }

            return capturedPart;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPart) {
            Piece p = Board.RemovePiece(destiny);
            p.DecreaseNumberOfMoves();
            if (capturedPart != null) {
                Board.InsertPart(capturedPart, destiny);
                _captured.Remove(capturedPart);
            }
            Board.InsertPart(p, origin);

            // #jogadaespecial roque pequeno
            if (p is King && destiny.Column == origin.Column + 2) {
                Position originT = new Position(origin.Line, origin.Column + 3);
                Position destinyT = new Position(origin.Line, origin.Column + 1);
                Piece T = Board.RemovePiece(destinyT);
                T.DecreaseNumberOfMoves();
                Board.InsertPart(T, originT);
            }

            // #jogadaespecial roque grande
            if (p is King && destiny.Column == origin.Column - 2) {
                Position originT = new Position(origin.Line, origin.Column - 4);
                Position destinyT = new Position(origin.Line, origin.Column - 1);
                Piece T = Board.RemovePiece(destinyT);
                T.DecreaseNumberOfMoves();
                Board.InsertPart(T, originT);
            }

            // #jogadaespecial en passant
            if (p is Pawn) {
                if (origin.Column != destiny.Column && capturedPart == vulneravelEnPassant) {
                    Piece pawn = Board.RemovePiece(destiny);
                    Position posP;
                    if (p.Collor == Collor.White) {
                        posP = new Position(3, destiny.Column);
                    }
                    else {
                        posP = new Position(4, destiny.Column);
                    }
                    Board.InsertPart(pawn, posP);
                }
            }
        }

        public void PerformsMove(Position origin, Position destiny) {
            Piece capturePart = ExecuteMovement(origin, destiny);

            if (IsInCheckmate(CurrentPlayer)) {
                UndoMove(origin, destiny, capturePart);
                throw new BoardException("Você não pode se colocar em CheckMate!");
            }

            Piece p = Board.ChessPiece(destiny);

            // #jogadaespecial promocao
            if (p is Pawn) {
                if ((p.Collor == Collor.White && destiny.Line == 0) || (p.Collor == Collor.Black && destiny.Line == 7)) {
                    p = Board.RemovePiece(destiny);
                    _pieces.Remove(p);
                    Piece lady = new Lady(Board, p.Collor);
                    Board.InsertPart(lady, destiny);
                    _pieces.Add(lady);
                }
            }

            if (IsInCheckmate(Adversary(CurrentPlayer))) {
                CheckMate = true;
            }
            else {
                CheckMate = false;
            }

            if (TestCheckmate(Adversary(CurrentPlayer))) {
                Finished = true;
            }
            else {
                Shift++;
                ChangePlayer();
            }

            // #jogadaespecial en passant
            if (p is Pawn && (destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2)) {
                vulneravelEnPassant = p;
            }
            else {
                vulneravelEnPassant = null;
            }
        }

        public void ValidateOriginPosition(Position pos) {
            if (Board.ChessPiece(pos) == null) {
                throw new BoardException("Não existe peça na posição de origin escolhida!");
            }
            if (CurrentPlayer != Board.ChessPiece(pos).Collor) {
                throw new BoardException("A peça de origin escolhida não é sua!");
            }
            if (!Board.ChessPiece(pos).ThereArePossibleMoves()) {
                throw new BoardException("Não há movimentos possíveis para a peça de origin escolhida!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destiny) {
            if (!Board.ChessPiece(origin).PossibleMoves(destiny)) {
                throw new BoardException("Posição de destiny inválida!");
            }
        }

        private void ChangePlayer() {
            if (CurrentPlayer == Collor.White) {
                CurrentPlayer = Collor.Black;
            }
            else {
                CurrentPlayer = Collor.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Collor collor) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _captured) {
                if (x.Collor == collor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PartsInPlay(Collor collor) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _pieces) {
                if (x.Collor == collor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(collor));
            return aux;
        }

        private Collor Adversary(Collor collor) {
            if (collor == Collor.White) {
                return Collor.Black;
            }
            else {
                return Collor.White;
            }
        }

        private Piece KingPiece(Collor collor) {
            foreach (Piece x in PartsInPlay(collor)) {
                if (x is King) {
                    return x;
                }
            }
            return null;
        }

        public bool IsInCheckmate(Collor collor) {
            Piece R = KingPiece(collor);
            if (R == null) {
                throw new BoardException("Não tem KingPiece da collor " + collor + " no board!");
            }
            foreach (Piece x in PartsInPlay(Adversary(collor))) {
                bool[,] mat = x.PossibleMoves();
                if (mat[R.Position.Line, R.Position.Column]) {
                    return true;
                }
            }
            return false;
        }

        public bool TestCheckmate(Collor collor) {
            if (!IsInCheckmate(collor)) {
                return false;
            }
            foreach (Piece x in PartsInPlay(collor)) {
                bool[,] mat = x.PossibleMoves();
                for (int i=0; i<Board.Lines; i++) {
                    for (int j=0; j<Board.Columns; j++) {
                        if (mat[i, j]) {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ExecuteMovement(origin, destiny);
                            bool testCheckmate = IsInCheckmate(collor);
                            UndoMove(origin, destiny, capturedPiece);
                            if (!testCheckmate) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPart(char coluna, int linha, Piece peca) {
            Board.InsertPart(peca, new PositionChess(coluna, linha).ToPosition());
            _pieces.Add(peca);
        }

        private void PlacePart() {
            PutNewPart('a', 1, new Tower(Board, Collor.White));
            PutNewPart('b', 1, new Horse(Board, Collor.White));
            PutNewPart('c', 1, new Bishop(Board, Collor.White));
            PutNewPart('d', 1, new Lady(Board, Collor.White));
            PutNewPart('e', 1, new King(Board, Collor.White, this));
            PutNewPart('f', 1, new Bishop(Board, Collor.White));
            PutNewPart('g', 1, new Horse(Board, Collor.White));
            PutNewPart('h', 1, new Tower(Board, Collor.White));
            PutNewPart('a', 2, new Pawn(Board, Collor.White, this));
            PutNewPart('b', 2, new Pawn(Board, Collor.White, this));
            PutNewPart('c', 2, new Pawn(Board, Collor.White, this));
            PutNewPart('d', 2, new Pawn(Board, Collor.White, this));
            PutNewPart('e', 2, new Pawn(Board, Collor.White, this));
            PutNewPart('f', 2, new Pawn(Board, Collor.White, this));
            PutNewPart('g', 2, new Pawn(Board, Collor.White, this));
            PutNewPart('h', 2, new Pawn(Board, Collor.White, this));

            PutNewPart('a', 8, new Tower(Board, Collor.Black));
            PutNewPart('b', 8, new Horse(Board, Collor.Black));
            PutNewPart('c', 8, new Bishop(Board, Collor.Black));
            PutNewPart('d', 8, new Lady(Board, Collor.Black));
            PutNewPart('e', 8, new King(Board, Collor.Black, this));
            PutNewPart('f', 8, new Bishop(Board, Collor.Black));
            PutNewPart('g', 8, new Horse(Board, Collor.Black));
            PutNewPart('h', 8, new Tower(Board, Collor.Black));
            PutNewPart('a', 7, new Pawn(Board, Collor.Black, this));
            PutNewPart('b', 7, new Pawn(Board, Collor.Black, this));
            PutNewPart('c', 7, new Pawn(Board, Collor.Black, this));
            PutNewPart('d', 7, new Pawn(Board, Collor.Black, this));
            PutNewPart('e', 7, new Pawn(Board, Collor.Black, this));
            PutNewPart('f', 7, new Pawn(Board, Collor.Black, this));
            PutNewPart('g', 7, new Pawn(Board, Collor.Black, this));
            PutNewPart('h', 7, new Pawn(Board, Collor.Black, this));
        }
    }
}
