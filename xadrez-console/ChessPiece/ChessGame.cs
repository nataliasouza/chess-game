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
            colocarPecas();
        }

        public Piece ExecuteMovement(Position origem, Position destino) {
            Piece p = Board.RemovePiece(origem);
            p.IncreaseNumberOfMoves();
            Piece pecaCapturada = Board.RemovePiece(destino);
            Board.InsertPart(p, destino);
            if (pecaCapturada != null) {
                _captured.Add(pecaCapturada);
            }

            // #jogadaespecial roque pequeno
            if (p is King && destino.Column == origem.Column + 2) {
                Position origemT = new Position(origem.Line, origem.Column + 3);
                Position destinoT = new Position(origem.Line, origem.Column + 1);
                Piece T = Board.RemovePiece(origemT);
                T.IncreaseNumberOfMoves();
                Board.InsertPart(T, destinoT);
            }

            // #jogadaespecial roque grande
            if (p is King && destino.Column == origem.Column - 2) {
                Position origemT = new Position(origem.Line, origem.Column - 4);
                Position destinoT = new Position(origem.Line, origem.Column - 1);
                Piece T = Board.RemovePiece(origemT);
                T.IncreaseNumberOfMoves();
                Board.InsertPart(T, destinoT);
            }

            // #jogadaespecial en passant
            if (p is Pawn) {
                if (origem.Column != destino.Column && pecaCapturada == null) {
                    Position posP;
                    if (p.Collor == Collor.White) {
                        posP = new Position(destino.Line + 1, destino.Column);
                    }
                    else {
                        posP = new Position(destino.Line - 1, destino.Column);
                    }
                    pecaCapturada = Board.RemovePiece(posP);
                    _captured.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        public void desfazMovimento(Position origem, Position destino, Piece pecaCapturada) {
            Piece p = Board.RemovePiece(destino);
            p.DecreaseNumberOfMoves();
            if (pecaCapturada != null) {
                Board.InsertPart(pecaCapturada, destino);
                _captured.Remove(pecaCapturada);
            }
            Board.InsertPart(p, origem);

            // #jogadaespecial roque pequeno
            if (p is King && destino.Column == origem.Column + 2) {
                Position origemT = new Position(origem.Line, origem.Column + 3);
                Position destinoT = new Position(origem.Line, origem.Column + 1);
                Piece T = Board.RemovePiece(destinoT);
                T.DecreaseNumberOfMoves();
                Board.InsertPart(T, origemT);
            }

            // #jogadaespecial roque grande
            if (p is King && destino.Column == origem.Column - 2) {
                Position origemT = new Position(origem.Line, origem.Column - 4);
                Position destinoT = new Position(origem.Line, origem.Column - 1);
                Piece T = Board.RemovePiece(destinoT);
                T.DecreaseNumberOfMoves();
                Board.InsertPart(T, origemT);
            }

            // #jogadaespecial en passant
            if (p is Pawn) {
                if (origem.Column != destino.Column && pecaCapturada == vulneravelEnPassant) {
                    Piece peao = Board.RemovePiece(destino);
                    Position posP;
                    if (p.Collor == Collor.White) {
                        posP = new Position(3, destino.Column);
                    }
                    else {
                        posP = new Position(4, destino.Column);
                    }
                    Board.InsertPart(peao, posP);
                }
            }
        }

        public void realizaJogada(Position origem, Position destino) {
            Piece pecaCapturada = ExecuteMovement(origem, destino);

            if (estaEmXeque(CurrentPlayer)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new BoardException("Você não pode se colocar em CheckMate!");
            }

            Piece p = Board.ChessPiece(destino);

            // #jogadaespecial promocao
            if (p is Pawn) {
                if ((p.Collor == Collor.White && destino.Line == 0) || (p.Collor == Collor.Black && destino.Line == 7)) {
                    p = Board.RemovePiece(destino);
                    _pieces.Remove(p);
                    Piece dama = new Lady(Board, p.Collor);
                    Board.InsertPart(dama, destino);
                    _pieces.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(CurrentPlayer))) {
                CheckMate = true;
            }
            else {
                CheckMate = false;
            }

            if (testeXequemate(adversaria(CurrentPlayer))) {
                Finished = true;
            }
            else {
                Shift++;
                mudaJogador();
            }

            // #jogadaespecial en passant
            if (p is Pawn && (destino.Line == origem.Line - 2 || destino.Line == origem.Line + 2)) {
                vulneravelEnPassant = p;
            }
            else {
                vulneravelEnPassant = null;
            }
        }

        public void validarPosicaoDeOrigem(Position pos) {
            if (Board.ChessPiece(pos) == null) {
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != Board.ChessPiece(pos).Collor) {
                throw new BoardException("A peça de origem escolhida não é sua!");
            }
            if (!Board.ChessPiece(pos).ThereArePossibleMoves()) {
                throw new BoardException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Position origem, Position destino) {
            if (!Board.ChessPiece(origem).PossibleMoves(destino)) {
                throw new BoardException("Posição de destino inválida!");
            }
        }

        private void mudaJogador() {
            if (CurrentPlayer == Collor.White) {
                CurrentPlayer = Collor.Black;
            }
            else {
                CurrentPlayer = Collor.White;
            }
        }

        public HashSet<Piece> pecasCapturadas(Collor cor) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _captured) {
                if (x.Collor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> pecasEmJogo(Collor cor) {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in _pieces) {
                if (x.Collor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Collor adversaria(Collor cor) {
            if (cor == Collor.White) {
                return Collor.Black;
            }
            else {
                return Collor.White;
            }
        }

        private Piece rei(Collor cor) {
            foreach (Piece x in pecasEmJogo(cor)) {
                if (x is King) {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Collor cor) {
            Piece R = rei(cor);
            if (R == null) {
                throw new BoardException("Não tem rei da cor " + cor + " no board!");
            }
            foreach (Piece x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.PossibleMoves();
                if (mat[R.Position.Line, R.Position.Column]) {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequemate(Collor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Piece x in pecasEmJogo(cor)) {
                bool[,] mat = x.PossibleMoves();
                for (int i=0; i<Board.Lines; i++) {
                    for (int j=0; j<Board.Columns; j++) {
                        if (mat[i, j]) {
                            Position origem = x.Position;
                            Position destino = new Position(i, j);
                            Piece pecaCapturada = ExecuteMovement(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Piece peca) {
            Board.InsertPart(peca, new PositionChess(coluna, linha).ToPosition());
            _pieces.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeca('a', 1, new Tower(Board, Collor.White));
            colocarNovaPeca('b', 1, new Horse(Board, Collor.White));
            colocarNovaPeca('c', 1, new Bishop(Board, Collor.White));
            colocarNovaPeca('d', 1, new Lady(Board, Collor.White));
            colocarNovaPeca('e', 1, new King(Board, Collor.White, this));
            colocarNovaPeca('f', 1, new Bishop(Board, Collor.White));
            colocarNovaPeca('g', 1, new Horse(Board, Collor.White));
            colocarNovaPeca('h', 1, new Tower(Board, Collor.White));
            colocarNovaPeca('a', 2, new Pawn(Board, Collor.White, this));
            colocarNovaPeca('b', 2, new Pawn(Board, Collor.White, this));
            colocarNovaPeca('c', 2, new Pawn(Board, Collor.White, this));
            colocarNovaPeca('d', 2, new Pawn(Board, Collor.White, this));
            colocarNovaPeca('e', 2, new Pawn(Board, Collor.White, this));
            colocarNovaPeca('f', 2, new Pawn(Board, Collor.White, this));
            colocarNovaPeca('g', 2, new Pawn(Board, Collor.White, this));
            colocarNovaPeca('h', 2, new Pawn(Board, Collor.White, this));

            colocarNovaPeca('a', 8, new Tower(Board, Collor.Black));
            colocarNovaPeca('b', 8, new Horse(Board, Collor.Black));
            colocarNovaPeca('c', 8, new Bishop(Board, Collor.Black));
            colocarNovaPeca('d', 8, new Lady(Board, Collor.Black));
            colocarNovaPeca('e', 8, new King(Board, Collor.Black, this));
            colocarNovaPeca('f', 8, new Bishop(Board, Collor.Black));
            colocarNovaPeca('g', 8, new Horse(Board, Collor.Black));
            colocarNovaPeca('h', 8, new Tower(Board, Collor.Black));
            colocarNovaPeca('a', 7, new Pawn(Board, Collor.Black, this));
            colocarNovaPeca('b', 7, new Pawn(Board, Collor.Black, this));
            colocarNovaPeca('c', 7, new Pawn(Board, Collor.Black, this));
            colocarNovaPeca('d', 7, new Pawn(Board, Collor.Black, this));
            colocarNovaPeca('e', 7, new Pawn(Board, Collor.Black, this));
            colocarNovaPeca('f', 7, new Pawn(Board, Collor.Black, this));
            colocarNovaPeca('g', 7, new Pawn(Board, Collor.Black, this));
            colocarNovaPeca('h', 7, new Pawn(Board, Collor.Black, this));
        }
    }
}
