using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.Events
{
    [CreateAssetMenu(menuName = "Event/Better Event")]
    public class BetterEvenSO : BaseEventSO<BetterEventSOListener>
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
                    Debug.LogWarning($"Event {name} had a null listener that has been removed", this);
                    listeners.Remove(listener);
                }
            }
        }
    }
}