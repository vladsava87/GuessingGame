using System;
using System.Collections.Generic;
using System.Linq;
using GuessingGame.interfaces;
using GuessingGame.models;

namespace GuessingGame
{
    public class Game
    {
        private readonly IConsoleService _consoleService;
        private readonly IHighScoreService _highScoreService;

        private int _number;
        private int _turns = 0;

        private readonly List<PlayerModel> _players = new List<PlayerModel>();

        public Game(IConsoleService consoleService, IHighScoreService highScoreService)
        {
            _consoleService = consoleService;
            _highScoreService = highScoreService;
        }

        public void Start()
        {
            _consoleService.ShowInstructions();

            var numberOfPLayers = (int) _consoleService.PromptForInput("Enter number of players? ", typeof(int));
            
            _turns = (int) _consoleService.PromptForInput("Enter number of turns?", typeof(int));

            for (var i = 1; i <= numberOfPLayers; i++)
            {
                var playerName = (string) _consoleService.PromptForInput($"Enter player #{i}'s name: ");
                _consoleService.MessageText($"Welcome {playerName}\n");
                _players.Add(new PlayerModel(playerName));
            }
            
            while (true)
            {

                StartRound();

                var winner = _players.SingleOrDefault(player => player.IsWinner);
                if (winner != null)
                {
                    _highScoreService.AddScore(winner);
                }
                else
                {
                    _consoleService.MessageText($"My number was: {_number}!");
                    // for no winners calculate the "winner"
                    // being the closest person to the result
                }
                DisplayScores();

                _consoleService.ShowHighScores();

                var playAgain = _consoleService.YesNoPrompt("Play again");

                if (playAgain)
                {
                    ClearAllResults();
                }
                else
                {
                    _consoleService.MessageText("Thanks for playing!");
                    break;
                }
            }
        }

        private void ClearAllResults()
        {
            foreach (var player in _players)
            {
                player.Guesses = new List<int>();
                player.IsWinner = false;
            }
        }

        private void StartRound()
        {
            ThinkOfNumber();

            for (var t = 1; t <= _turns; t++)
            {
                foreach (var player in _players)
                {
                    var isCorrectGuess = PlayerGuess(player);

                    if (isCorrectGuess)
                    {
                        return;
                    }
                } 

                _consoleService.MessageText($"End of turn #{t}");
            }
        }

        private void DisplayScores()
        {
            foreach (var player in _players.Where(player => player.IsWinner))
            {
                _consoleService.ShowGuesses(player);
            }

            foreach (var player in _players.Where(player => !player.IsWinner))
            {
                _consoleService.ShowGuesses(player);
            }
        }

        private bool PlayerGuess(PlayerModel player)
        {
            var guess = (int) _consoleService.PromptForInput($"What is your guess {player.PlayerName}? ", typeof(int));
            player.Guesses.Add(guess);

            if (guess != _number)
            {
                _consoleService.MessageText(guess < _number
                    ? "Incorrect, my number is higher."
                    : "Incorrect, my number is lower.");

                return false;
            }

            player.IsWinner = true;
            _consoleService.MessageText($"Congratulations {player.PlayerName}, you guessed it!");
            return true;
        }

        private void ThinkOfNumber()
        {
            var rand = new Random();
            _number = rand.Next(10);
        }
    }
}