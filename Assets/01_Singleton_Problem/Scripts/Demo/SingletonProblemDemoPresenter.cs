using UnityEngine;
using BeforeCombat = ToanLB.SingletonProblem.Before.CombatController;
using BeforeGM = ToanLB.SingletonProblem.Before.GameManager;
using AfterBootstrapper = ToanLB.SingletonProblem.After.CombatBootstrapper;

namespace ToanLB.SingletonProblem.Demo
{
    public class SingletonProblemDemoPresenter : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private AfterBootstrapper bootstrapper;
        [SerializeField] private BeforeCombat beforeCombatController;

        [Header("Universal Branding Thumbnail")]
        [Tooltip("Thumbnail minh hoạ Engineering Lab có lồng chân dung tác giả")]
        [SerializeField] private Texture2D thumbnailTexture;
        [SerializeField] private bool showThumbnailInGameView = true;

        private string _lastLog = "Sẵn sàng chạy thử nghiệm...";
        private int _attackCounter = 0;

        private void Awake()
        {
            LoadThumbnailFallback();
        }

#if UNITY_EDITOR
        private void Reset()
        {
            LoadThumbnailFallback();
        }
#endif

        private void LoadThumbnailFallback()
        {
            if (thumbnailTexture == null)
            {
#if UNITY_EDITOR
                thumbnailTexture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/Thumbnail_Engineering_Lab.png");
                if (thumbnailTexture == null)
                {
                    thumbnailTexture = UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/Thumbnail_Singleton_Problem.png");
                }
#endif
                if (thumbnailTexture == null)
                {
                    thumbnailTexture = Resources.Load<Texture2D>("Thumbnail_Engineering_Lab");
                }
                if (thumbnailTexture == null)
                {
                    thumbnailTexture = Resources.Load<Texture2D>("Thumbnail_Singleton_Problem");
                }
            }
        }

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

            LoadThumbnailFallback();
        }

        private void OnGUI()
        {
            // 1. Panel Điều Khiển Demo (Bên Trái)
            GUILayout.BeginArea(new Rect(20, 20, 480, 440), GUI.skin.box);
            
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
            if (GUILayout.Button("🔥 1. Bắn Đòn Đánh (Bad Singleton)", GUILayout.Height(38)))
            {
                _attackCounter++;
                beforeCombatController.DealDamage(100);
                _lastLog = $"[Cách Cũ] Đòn {_attackCounter}: Base 100 - Defense {BeforeGM.Instance.CurrentEnemyDefense} -> Gây 80 dmg. Điểm Singleton: {BeforeGM.Instance.PlayerScore}";
            }

            GUILayout.Space(12);

            // 2. Cách mới
            int cleanScore = (bootstrapper != null && bootstrapper.ScoreServiceInstance != null) 
                ? bootstrapper.ScoreServiceInstance.TotalScore 
                : 0;
            GUILayout.Label("<b>[Cách Mới - Pure C# DI (Sau khi Refactor)]</b>");
            GUILayout.Label($"• Điểm số ScoreService (Encapsulated): <color=green>{cleanScore}</color>");
            if (GUILayout.Button("✨ 2. Bắn Đòn Đánh (Clean Architecture DI)", GUILayout.Height(38)))
            {
                _attackCounter++;
                if (bootstrapper != null)
                {
                    bootstrapper.TestCleanAttack();
                    _lastLog = $"[Cách Mới] Đòn {_attackCounter}: Xử lý qua CombatProcessor (Pure C#). Điểm ScoreService: {cleanScore + 800}";
                }
            }

            GUILayout.Space(12);
            GUILayout.Label("<b>Trạng thái Log:</b>");
            var logStyle = new GUIStyle(GUI.skin.textArea)
            {
                fontSize = 12,
                wordWrap = true
            };
            GUILayout.TextArea(_lastLog, logStyle, GUILayout.Height(65));

            GUILayout.EndArea();

            // 2. Panel Hiển Thị Thumbnail (Bên Cạnh Trong GameView)
            if (showThumbnailInGameView && thumbnailTexture != null)
            {
                float panelX = 520;
                float panelY = 20;
                float availableWidth = Screen.width - panelX - 20;
                float availableHeight = Screen.height - panelY - 20;

                if (availableWidth > 200)
                {
                    float aspect = (float)thumbnailTexture.width / thumbnailTexture.height;
                    float drawHeight = Mathf.Min(440, availableHeight);
                    float drawWidth = drawHeight * aspect;

                    if (drawWidth > availableWidth)
                    {
                        drawWidth = availableWidth;
                        drawHeight = drawWidth / aspect;
                    }

                    Rect boxRect = new Rect(panelX, panelY, drawWidth + 20, drawHeight + 46);
                    GUI.Box(boxRect, GUIContent.none);

                    var headerStyle = new GUIStyle(GUI.skin.label)
                    {
                        fontSize = 13,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleLeft
                    };
                    GUI.Label(new Rect(panelX + 10, panelY + 4, drawWidth, 22), "🏛️ TOANLB ENGINEERING LAB (toanlb.dev):", headerStyle);

                    Rect thumbRect = new Rect(panelX + 10, panelY + 28, drawWidth, drawHeight);
                    GUI.DrawTexture(thumbRect, thumbnailTexture, ScaleMode.ScaleToFit);
                }
            }
        }
    }
}
