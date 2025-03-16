namespace Game.Batyr.ScoreManager
{
    public interface IScoreManager
    {
        public int TotalScore { get; }
        public void AddScore(int score);
        public void AddPenalty(int penalty);
        public void CalculateTotalScore();
    }
}