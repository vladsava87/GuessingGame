using System.Collections.Generic;

namespace GuessingGame.models
{
    public class PlayerModel
    {
        public string PlayerName { set; get; }
        public int NumGuesses => Guesses.Count;
        public List<int> Guesses { set; get; }
        public bool IsWinner { set; get; }

        public PlayerModel(string playerName)
        {
            PlayerName = playerName;
            Guesses = new List<int>();
        }
    }
}