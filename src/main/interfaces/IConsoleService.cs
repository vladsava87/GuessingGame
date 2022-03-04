using System;
using GuessingGame.models;

namespace GuessingGame.interfaces
{
    public interface IConsoleService
    { 
        object PromptForInput(string prompt, Type type = null);
        public void ShowInstructions();
        public void MessageText(string message);
        public void ShowHighScores();
        public void ShowGuesses(PlayerModel player);
        public bool YesNoPrompt(string message);
    }
}