namespace GuessingGame.models
{
    public class HighScoreModel
    {
        public string PlayerName { set; get; }
        public int NumGuesses { get; set; }

        public HighScoreModel(string playerName, int numGuesses)
        {
            PlayerName = playerName;
            NumGuesses = numGuesses;
        }
    }
}