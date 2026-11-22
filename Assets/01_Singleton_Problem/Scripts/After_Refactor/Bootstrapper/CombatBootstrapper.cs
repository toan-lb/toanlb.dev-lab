using UnityEngine;

namespace ToanLB.SingletonProblem.After
{
    [DefaultExecutionOrder(-50)]
    public class CombatBootstrapper : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private EnemyStats enemyStats;
        [SerializeField] private SoundService soundService;
        [SerializeField] private CombatController combatController;

        public ScoreService ScoreServiceInstance { get; private set; }
        public CombatProcessor CombatProcessorInstance { get; private set; }

        private void Awake()
        {
            SetupDependencies();
        }

        public void SetupDependencies()
        {
            if (enemyStats == null) enemyStats = GetComponentInChildren<EnemyStats>();
            if (enemyStats == null) enemyStats = gameObject.AddComponent<EnemyStats>();

            if (soundService == null) soundService = GetComponentInChildren<SoundService>();
            if (soundService == null) soundService = gameObject.AddComponent<SoundService>();

            if (combatController == null) combatController = GetComponentInChildren<CombatController>();
            if (combatController == null) combatController = gameObject.AddComponent<CombatController>();

            ScoreServiceInstance = new ScoreService();
            CombatProcessorInstance = new CombatProcessor(enemyStats, soundService, ScoreServiceInstance);
            combatController.Initialize(CombatProcessorInstance);

            Debug.Log("<color=green>[CombatBootstrapper]</color> Dependencies successfully wired up via Pure C# DI!");
        }

        [ContextMenu("Test Clean Attack (Base: 100)")]
        public void TestCleanAttack()
        {
            if (combatController != null)
            {
                combatController.DealDamage(100);
            }
        }
    }
}
