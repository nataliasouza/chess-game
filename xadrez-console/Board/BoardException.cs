using System;

namespace tabuleiro {
    class BoardException : Exception {

        public BoardException(string msg) : base(msg) {
        }
    }
}
