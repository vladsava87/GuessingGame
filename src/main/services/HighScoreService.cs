using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GuessingGame.interfaces;
using GuessingGame.models;

namespace GuessingGame.services
{
    public class HighScoreService : IHighScoreService
    {
        private readonly string _highScoreFilePath;
        
        private List<HighScoreModel> _highScores = new List<HighScoreModel>();

        #region Contructors
        public HighScoreService() : this("highscore.txt")
        {
        }

        public HighScoreService(string highScoreFile)
        {
            _highScoreFilePath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName + Path.DirectorySeparatorChar + highScoreFile;
            LoadHighScores();
        }
        #endregion

        #region Public Properties

        public void ClearScores()
        {
            File.WriteAllText(_highScoreFilePath, string.Empty);
        }

        public List<HighScoreModel> GetHighScores()
        {
            return _highScores;
        }

        public void AddScore(PlayerModel player)
        {
            var existingScore = _highScores.FirstOrDefault(highScore => highScore.PlayerName.Equals(player.PlayerName));
            if (existingScore == null)
            {
                InsertScore(player);
            }
            else if (player.NumGuesses < existingScore.NumGuesses)
            {
                _highScores.Remove(existingScore);
                InsertScore(player);
            }
        }

        #endregion

        #region Private Methods

        private void InsertScore(PlayerModel player)
        {
            _highScores.Add(new HighScoreModel(player.PlayerName, player.NumGuesses));
            StoreHighScores();
        }
        
        private void LoadHighScores()
        {
            try
            {
                if (File.Exists(_highScoreFilePath))
                {
                    using var sr = File.OpenText(_highScoreFilePath);
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        _highScores.Add(ParseLine(s));
                    }
                    return;
                }
                var file = new FileInfo(_highScoreFilePath);
                file.Directory?.Create();
                File.WriteAllText(file.FullName, null);
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
                throw new Exception("No highscore file");
            }
        }
        
        private HighScoreModel ParseLine(string line)
        {
            var nameAndGuesses = line.Split(":");
            return new HighScoreModel(nameAndGuesses[0], int.Parse(nameAndGuesses[1]));
        }
        
        private void StoreHighScores()
        {
            var fi = new FileInfo(_highScoreFilePath);

            // save scores to file     
            var sw = fi.CreateText();
            var output = string.Join(Environment.NewLine,
                _highScores.Select(score => $"{score.PlayerName}:{score.NumGuesses:D}"));
            sw.Write(output);
            sw.Flush();
            sw.Close();
        }
        #endregion
    }
}