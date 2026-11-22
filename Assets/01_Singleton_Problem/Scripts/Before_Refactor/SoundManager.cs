using UnityEngine;

namespace ToanLB.SingletonProblem.Before
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void PlaySfx(string soundName)
        {
            Debug.Log($"<color=cyan>[SoundManager]</color> PlaySfx: '{soundName}' via Singleton.");
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStaticState()
        {
            Instance = null;
        }
    }
}
