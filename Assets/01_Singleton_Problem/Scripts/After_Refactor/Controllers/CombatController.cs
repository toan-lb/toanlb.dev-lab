using System;
using UnityEngine;

namespace ToanLB.SingletonProblem.After
{
    public class CombatController : MonoBehaviour
    {
        private CombatProcessor _combatProcessor;

        public void Initialize(CombatProcessor combatProcessor)
        {
            _combatProcessor = combatProcessor ?? throw new ArgumentNullException(nameof(combatProcessor));
        }

        public void DealDamage(int baseDamage)
        {
            if (_combatProcessor == null)
            {
                Debug.LogError("[CombatController] Chưa được khởi tạo qua hàm Initialize!");
                return;
            }

            _combatProcessor.ExecuteAttack(baseDamage);
        }
    }
}
