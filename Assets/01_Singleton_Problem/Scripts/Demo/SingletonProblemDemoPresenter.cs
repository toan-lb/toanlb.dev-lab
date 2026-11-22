using UnityEngine;
using BeforeCombat = ToanLB.SingletonProblem.Before.CombatController;
using BeforeGM = ToanLB.SingletonProblem.Before.GameManager;
using AfterBootstrapper = ToanLB.SingletonProblem.After.CombatBootstrapper;

namespace ToanLB.SingletonProblem.Demo
{
    public class SingletonProblemDemoPresenter : MonoBehaviour
    {
        [SerializeField] private AfterBootstrapper bootstrapper;
        [SerializeField] private BeforeCombat beforeCombatController;

        private string _lastLog = "Sẵn sàng chạy thử nghiệm...";
        private int _attackCounter = 0;

        private void Start()
        {
            if (bootstrapper == null)
            {
                bootstrapper = FindFirstObjectByType<AfterBootstrapper>() ?? gameObject.AddComponent<AfterBootstrapper>();
            }

            if (beforeCombatController == null)
            {
                beforeCombatController = FindFirstObjectByType<BeforeCombat>() ?? gameObject.AddComponent<BeforeCombat>();
            }
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(20, 20, 480, 420), GUI.skin.box);
            
            var titleStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter
            };
            GUILayout.Label("⚔️ DEMO: BÀI 01 - SINGLETON PROBLEM", titleStyle);
            GUILayout.Space(10);

            // 1. Cách cũ
            int badScore = BeforeGM.Instance != null ? BeforeGM.Instance.PlayerScore : 0;
            GUILayout.Label("<b>[Cách Cũ - Singleton (Trước khi Refactor)]</b>");
            GUILayout.Label($"• Điểm số GameManager.Instance: <color=yellow>{badScore}</color>");
            if (GUILayout.Button("🔥 1. Bắn Đòn Đánh (Bad Singleton)", GUILayout.Height(40)))
            {
                _attackCounter++;
                beforeCombatController.DealDamage(100);
                _lastLog = $"[Cách Cũ] Đòn {_attackCounter}: Base 100 - Defense {BeforeGM.Instance.CurrentEnemyDefense} -> Gây 80 dmg. Điểm Singleton: {BeforeGM.Instance.PlayerScore}";
            }

            GUILayout.Space(15);

            // 2. Cách mới
            int cleanScore = (bootstrapper != null && bootstrapper.ScoreServiceInstance != null) 
                ? bootstrapper.ScoreServiceInstance.TotalScore 
                : 0;
            GUILayout.Label("<b>[Cách Mới - Pure C# DI (Sau khi Refactor)]</b>");
            GUILayout.Label($"• Điểm số ScoreService (Encapsulated): <color=green>{cleanScore}</color>");
            if (GUILayout.Button("✨ 2. Bắn Đòn Đánh (Clean Architecture DI)", GUILayout.Height(40)))
            {
                _attackCounter++;
                if (bootstrapper != null)
                {
                    bootstrapper.TestCleanAttack();
                    _lastLog = $"[Cách Mới] Đòn {_attackCounter}: Xử lý qua CombatProcessor (Pure C#). Điểm ScoreService: {cleanScore + 800}";
                }
            }

            GUILayout.Space(15);
            GUILayout.Label("<b>Trạng thái Log:</b>");
            var logStyle = new GUIStyle(GUI.skin.textArea)
            {
                fontSize = 12,
                wordWrap = true
            };
            GUILayout.TextArea(_lastLog, logStyle, GUILayout.Height(70));

            GUILayout.EndArea();
        }
    }
}
