using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.Events
{
    [CreateAssetMenu(menuName = "Event/Unity Event")]
    public class UnityEventSO : BaseEventSO<UnityEventListenerSO>
    {
        [Button]
        public void Raise()
        {
            Log();
            foreach (var listener in listeners)
            {
                if (listener != null)
                    listener.Invoke();
                else
                {
                    Debug.LogWarning($"Event {name} had a null listener that has been removed");
                    listeners.Remove(listener);
                }
            }
        }
    }
}