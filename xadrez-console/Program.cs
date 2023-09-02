using System;
using tabuleiro;
using xadrez;

namespace xadrez_console {
    class Program {
        static void Main(string[] args) {

            //    try {
            //        PartidaDeXadrez match = new PartidaDeXadrez();

            //        while (!match.terminada) {

            //            try {
            //                Console.Clear();
            //                Screen.imprimirPartida(match);

            //                Console.WriteLine();
            //                Console.Write("Origem: ");
            //                Position origin = Screen.lerPosicaoXadrez().toPosicao();
            //                match.validarPosicaoDeOrigem(origin);

            //                bool[,] posicoesPossiveis = match.tab.peca(origin).movimentosPossiveis();

            //                Console.Clear();
            //                Screen.imprimirTabuleiro(match.tab, posicoesPossiveis);

            //                Console.WriteLine();
            //                Console.Write("Destino: ");
            //                Position destino = Screen.lerPosicaoXadrez().toPosicao();
            //                match.validarPosicaoDeDestino(origin, destino);

            //                match.realizaJogada(origin, destino);
            //            }
            //            catch (BoardException e) {
            //                Console.WriteLine(e.Message);
            //                Console.ReadLine();
            //            }
            //        }
            //        Console.Clear();
            //        Screen.imprimirPartida(match);
            //    }
            //    catch (BoardException e) {
            //        Console.WriteLine(e.Message);
            //    }

            //    Console.ReadLine();

            Position p;
            p = new Position(3, 4);

            Console.WriteLine("Position: " + p);
            Console.ReadLine();
        }
    }
}
