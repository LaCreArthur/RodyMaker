using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.Events
{
    public class BasePEventSO<T> : BaseEventSO<BasePEventListenerSO<T>>
    {
        [Button]
        public void Raise(T t)
        {
            Log(t);
            foreach (var listener in listeners)
            {
                if (listener != null)
                    listener.Invoke(t);
                else
                {
                    Debug.LogWarning($"Event {name} had a null listener that has been removed");
                    listeners.Remove(listener);
                }
            }
        }
    }
}