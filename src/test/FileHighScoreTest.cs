using System.IO;
using GuessingGame.models;
using GuessingGame.services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class FileHighScoreTest
    {
        private HighScoreService _highScore;
        private string _highScoreFilePath;

        [TestInitialize]
        public void Startup()
        {
            const string highScoreFile = "\\test\\tempScores.txt";

            _highScore = new HighScoreService(highScoreFile);

            _highScoreFilePath = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent?.FullName + highScoreFile;
            var file = new FileInfo(_highScoreFilePath);

            file.Directory?.Create();
            File.WriteAllText(file.FullName, null);

            Assert.IsTrue(file.Exists);
        }

        [TestMethod]
        public void AddScore()
        {
            Assert.IsTrue(_highScore.GetHighScores().Count == 0);
            _highScore.AddScore(new PlayerModel("Rocky", 12));
            Assert.IsTrue(1 == _highScore.Scores.Count);
            _highScore.AddScore("Rubble", 10);
            Assert.IsTrue(2 == _highScore.Scores.Count);
            _highScore.AddScore("Rubble", 12);
            Assert.IsTrue(2 == _highScore.Scores.Count);
            _highScore.AddScore("Rubble", 8);
            Assert.IsTrue(2 == _highScore.Scores.Count);
        }


        [TestMethod]
        public void GetScores()
        {
            _highScore.AddScore("Rocky", 12);
            _highScore.AddScore("Rubble", 8);
            _highScore.AddScore("Chase", 10);

            Assert.IsTrue(3 == _highScore.Scores.Count);
        }

        [TestMethod]
        public void ClearScores()
        {
            _highScore.AddScore("Rocky", 14);
            _highScore.ClearScores();

            Assert.IsTrue(0 == _highScore.Scores.Count);
        }
    }
}
