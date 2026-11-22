using NUnit.Framework;
using UnityEngine;
using ToanLB.SingletonProblem.Before;

namespace ToanLB.SingletonProblem.Tests
{
    [TestFixture]
    public class BadApproachTestLimitationDemo
    {
        private GameObject _holderGo;
        private CombatController _controller;

        [SetUp]
        public void Setup()
        {
            _holderGo = new GameObject("Test_BadCombatController");
            _controller = _holderGo.AddComponent<CombatController>();
        }

        [TearDown]
        public void Teardown()
        {
            if (_holderGo != null)
            {
                Object.DestroyImmediate(_holderGo);
            }
        }

        [Test]
        public void Demonstrate_WhyTestingSingletonIsPainful()
        {
            _controller.DealDamage(100);

            Assert.Greater(GameManager.Instance.PlayerScore, 0);
            Debug.LogWarning("[BadApproachTest] Singleton state đã bị làm bẩn trong RAM Editor!");
        }
    }
}
