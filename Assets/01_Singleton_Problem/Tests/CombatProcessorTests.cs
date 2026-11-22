using System;
using NUnit.Framework;
using ToanLB.SingletonProblem.After;

[TestFixture]
public class CombatProcessorTests
{
    private class FakeEnemyStats : IEnemyStatsProvider
    {
        public int Defense { get; set; }
    }

    private class SpyAudioService : IAudioService
    {
        public string LastPlayedSound { get; private set; }
        public void PlaySfx(string soundName) => LastPlayedSound = soundName;
    }

    private class SpyScoreTracker : IScoreTracker
    {
        public int TotalScore { get; private set; }
        public void AddScore(int amount) => TotalScore += amount;
    }

    [Test]
    public void ExecuteAttack_AttackHigherThanDefense_DealsDamageAndAddsScore()
    {
        var stats = new FakeEnemyStats { Defense = 30 };
        var audio = new SpyAudioService();
        var score = new SpyScoreTracker();
        var processor = new CombatProcessor(stats, audio, score);

        int damage = processor.ExecuteAttack(baseAttack: 100);

        Assert.AreEqual(70, damage);
        Assert.AreEqual(700, score.TotalScore);
        Assert.AreEqual("Hit_Sound", audio.LastPlayedSound);
    }

    [Test]
    public void ExecuteAttack_DefenseHigherThanAttack_ClampsMinimumOneDamage()
    {
        var stats = new FakeEnemyStats { Defense = 50 };
        var score = new SpyScoreTracker();
        var processor = new CombatProcessor(stats, audioService: null, scoreTracker: score);

        int damage = processor.ExecuteAttack(baseAttack: 10);

        Assert.AreEqual(1, damage);
        Assert.AreEqual(10, score.TotalScore);
    }

    #region Additional Production Edge Case Tests

    [Test]
    public void ExecuteAttack_WithNullAudioService_StillCalculatesDamageAndScore()
    {
        var stats = new FakeEnemyStats { Defense = 20 };
        var score = new SpyScoreTracker();
        var processor = new CombatProcessor(stats, audioService: null, scoreTracker: score);

        Assert.DoesNotThrow(() =>
        {
            int damage = processor.ExecuteAttack(baseAttack: 50);
            Assert.AreEqual(30, damage);
            Assert.AreEqual(300, score.TotalScore);
        });
    }

    [Test]
    public void Constructor_NullEnemyStats_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            var unused = new CombatProcessor(enemyStats: null, audioService: null, scoreTracker: null);
        });
    }

    #endregion
}
