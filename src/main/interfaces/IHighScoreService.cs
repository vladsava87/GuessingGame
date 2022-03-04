using System.Collections.Generic;
using GuessingGame.models;

namespace GuessingGame.interfaces
{
    public interface IHighScoreService
    {
        List<HighScoreModel> GetHighScores();
        void AddScore(PlayerModel player);
        void ClearScores();
    }
}