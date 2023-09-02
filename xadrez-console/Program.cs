using System;
using board;
using chessPiece;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {

            try
            {
                ChessGame match = new ChessGame();

                while (!match.Finished)
                {

                    try
                    {
                        Console.Clear();
                        Screen.PrintMatch(match);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Screen.ReadChessPosition().ToPosition();
                        match.validarPosicaoDeOrigem(origin);

                        bool[,] posicoesPossiveis = match.Board.ChessPiece(origin).PossibleMoves();

                        Console.Clear();
                        Screen.PrintBoard(match.Board, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Position destino = Screen.ReadChessPosition().ToPosition();
                        match.validarPosicaoDeDestino(origin, destino);

                        match.realizaJogada(origin, destino);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.PrintMatch(match);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

            ChessBoard chessBoard = new ChessBoard(8,8);

            Console.WriteLine(chessBoard);
            Console.ReadLine();
        }
    }
}
