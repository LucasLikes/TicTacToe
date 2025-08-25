using System.Collections.Generic;

namespace JogoDaVelha.Core.Logging;
    public class MoveData
    {
        public string[] State { get; set; }  // Estado do tabuleiro (posição 0 a 8)
        public int Action { get; set; }      // Ação realizada (posição jogada)
        public string Player { get; set; }   // "X" ou "O"
    }

    public class GameLog
    {
        public List<MoveData> History { get; set; } = new();
        public string Winner { get; set; }   // "X", "O" ou "None"
    }