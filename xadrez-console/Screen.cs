using System;
using System.Collections.Generic;
using board;
using chessPiece;

namespace xadrez_console {
    class Screen {

        public static void PrintMatch(ChessGame match) 
        {
            PrintBoard(match.Board);
            Console.WriteLine();
            PrintCapturedParts(match);
            Console.WriteLine();
            Console.WriteLine("Shift: " + match.Shift);

            if (!match.Finished) 
            {
                Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);
               
                if (match.CheckMate) 
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + match.CurrentPlayer);
            }
        }

        public static void PrintCapturedParts(ChessGame partida) 
        {
            Console.WriteLine("Peças _captured:");
            Console.Write("Brancas: ");
            PrintSet(partida.pecasCapturadas(Collor.White));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintSet(partida.pecasCapturadas(Collor.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void PrintSet(HashSet<Piece> conjunto)
        {
            Console.Write("[");
            foreach (Piece x in conjunto) 
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void PrintBoard(ChessBoard chessBoard) 
        {
            for (int i = 0; i < chessBoard.Lines; i++) 
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < chessBoard.Columns; j++)
                {
                   PrintPart(chessBoard.ReturnPart(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintBoard(ChessBoard tab, bool[,] posicoePossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Lines; i++) 
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Columns; j++) 
                {
                    if (posicoePossiveis[i, j]) 
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else 
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    PrintPart(tab.ReturnPart(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PositionChess ReadChessPosition() 
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PositionChess(coluna, linha);
        }

        public static void PrintPart(Piece piece) 
        {
            if (piece == null) 
            {
                Console.Write("- ");
            }
            else {
                if (piece.Collor == Collor.White) 
                {
                    Console.Write(piece);
                }
                else 
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
