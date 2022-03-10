using System;
using System.Linq;
using System.Text;
using GuessingGame.interfaces;
using GuessingGame.models;

namespace GuessingGame.services
{
    public class ConsoleService : IInputOutputService
    {
        private readonly IHighScoreService _highScoreService;

        #region Contructor

        public ConsoleService(IHighScoreService highScoreService)
        {
            _highScoreService = highScoreService;
        }

        #endregion

        #region Public Methods

        public object PromptForInput(string prompt, Type type = null)
        {
            string input;

            while (true)
            {
                Console.WriteLine(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a value...");
                }
                else
                {
                    if (type != null && type == typeof(int))
                    {
                        if (int.TryParse(input, out var value))
                        {
                            return value;
                        }

                        Console.WriteLine("Please enter a number...");
                    }
                    else
                    {
                        break;
                    }
                }
            }
 
            return input;
        }

        public void ShowInstructions()
        {
            Console.WriteLine("\n--- Welcome to Guessing Game ---");
            Console.WriteLine("\nI am thinking of a number between 0 and 1000, your job is to guess the number!");
        }
        
        public void MessageText(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowHighScores()
        {
            Console.WriteLine($"\n{"--- Highscore ---",25}");
            Console.WriteLine($"Pos {"Player",15} {"Guesses",15}");
            var b = new StringBuilder();

            var orderedScores = _highScoreService.GetHighScores().OrderBy(s => s.NumGuesses);

            var i = 1;
            foreach (var highScore in orderedScores)
            {
                b.Append($"{i:D}, {highScore.PlayerName,15} {highScore.NumGuesses,15:D} \n");
                i++;
            }

            Console.WriteLine(b);
        }
        
        public void ShowGuesses(PlayerModel player)
        {
            Console.WriteLine($"{player.PlayerName}'s number of guesses: {player.NumGuesses}");
            Console.WriteLine($"{player.PlayerName}'s guesses: {string.Join(",", player.Guesses.Select(i => i.ToString()).ToArray())}");
        }

        public bool YesNoPrompt(string message)
        {
            Console.WriteLine($"{message}? (Y/N)");
            var yesOrNo = Console.ReadLine()?.ToLower();
            return yesOrNo is "y";
        }

        #endregion
    }
}