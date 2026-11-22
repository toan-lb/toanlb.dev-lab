using UnityEngine;

namespace ToanLB.SingletonProblem.Before
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var existing = FindFirstObjectByType<GameManager>();
                    if (existing != null)
                    {
                        _instance = existing;
                    }
                    else
                    {
                        var go = new GameObject("[ZOMBIE] GameManager_AutoCreated");
                        _instance = go.AddComponent<GameManager>();
                        DontDestroyOnLoad(go);
                        Debug.LogWarning("<color=yellow>[GameManager]</color> Lazy Instantiate GameObject mới khi truy cập Instance! Dễ sinh ra Zombie Object khi Unload Scene.");
                    }
                }
                return _instance;
            }
        }

        [Header("Global State - Ai cũng đọc và ghi đè được")]
        public int CurrentEnemyDefense = 20;
        public int PlayerScore = 0;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStaticState()
        {
            _instance = null;
        }
    }
}
