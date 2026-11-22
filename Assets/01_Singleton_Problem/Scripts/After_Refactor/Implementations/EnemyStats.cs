using UnityEngine;

namespace ToanLB.SingletonProblem.After
{
    public class EnemyStats : MonoBehaviour, IEnemyStatsProvider
    {
        [SerializeField] private int defense = 20;

        public int Defense => defense;

        public void SetDefense(int newDefense)
        {
            defense = Mathf.Max(0, newDefense);
        }
    }
}
