using UnityEngine;
using UnityServiceLocator;

namespace Game.Batyr.ScoreManager
{
    public class ScoreManager : MonoBehaviour, IScoreManager
    {
        private int Score { get; set; }
        private int Penalty { get; set; }
        public int TotalScore { get; private set; }

        private void Awake()
        {
            ServiceLocator.ForSceneOf(this).Register<IScoreManager>(this);
        }

        public void AddScore(int score)
        {
            Score += score;
            Score = Mathf.Max(0, Score);
        }

        public void AddPenalty(int penalty)
        {
            Penalty += penalty;
            Penalty = Mathf.Max(0, Penalty);
        }

        public void CalculateTotalScore()
        {
            TotalScore = Score - Penalty;
        }
    }
}