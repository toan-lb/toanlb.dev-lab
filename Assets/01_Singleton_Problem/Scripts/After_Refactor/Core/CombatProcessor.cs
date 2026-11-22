using System;

namespace ToanLB.SingletonProblem.After
{
    public class CombatProcessor
    {
        private readonly IEnemyStatsProvider _enemyStats;
        private readonly IAudioService _audioService;
        private readonly IScoreTracker _scoreTracker;

        public CombatProcessor(
            IEnemyStatsProvider enemyStats,
            IAudioService audioService,
            IScoreTracker scoreTracker)
        {
            _enemyStats = enemyStats ?? throw new ArgumentNullException(nameof(enemyStats));
            _audioService = audioService;
            _scoreTracker = scoreTracker;
        }

        public int ExecuteAttack(int baseAttack)
        {
            int finalDamage = Math.Max(1, baseAttack - _enemyStats.Defense);

            _audioService?.PlaySfx("Hit_Sound");
            _scoreTracker?.AddScore(finalDamage * 10);

            return finalDamage;
        }
    }
}
