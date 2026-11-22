using UnityEngine;

namespace ToanLB.SingletonProblem.After
{
    public class SoundService : MonoBehaviour, IAudioService
    {
        public void PlaySfx(string soundName)
        {
            Debug.Log($"<color=cyan>[SoundService]</color> PlaySfx cleanly: '{soundName}'");
        }
    }
}
