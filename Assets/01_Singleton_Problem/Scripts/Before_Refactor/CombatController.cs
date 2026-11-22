using UnityEngine;

namespace ToanLB.SingletonProblem.Before
{
    public class CombatController : MonoBehaviour
    {
        public void DealDamage(int baseDamage)
        {
            // 1. Hidden dependency vào GameManager để lấy chỉ số phòng thủ
            int defense = GameManager.Instance.CurrentEnemyDefense;
            int finalDamage = Mathf.Max(1, baseDamage - defense);

            // 2. Gọi trực tiếp SoundManager
            SoundManager.Instance.PlaySfx("Hit_Sound");

            // 3. Tự ý thay đổi global state
            GameManager.Instance.PlayerScore += finalDamage * 10;
            
            Debug.Log($"Gây {finalDamage} sát thương.");
        }
    }
}
