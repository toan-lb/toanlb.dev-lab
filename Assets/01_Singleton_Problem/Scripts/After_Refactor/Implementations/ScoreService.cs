using UnityEngine;

namespace ToanLB.SingletonProblem.After
{
    public class ScoreService : IScoreTracker
    {
        public int TotalScore { get; private set; }

        public void AddScore(int amount)
        {
            TotalScore += amount;
            Debug.Log($"<color=green>[ScoreService]</color> TotalScore: {TotalScore} (+{amount})");
        }
    }
}
