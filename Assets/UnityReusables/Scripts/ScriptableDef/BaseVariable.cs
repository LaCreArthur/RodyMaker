using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    public class BaseVariable<T> : RegistrableScriptableObject
    {
        [SerializeField] protected T initialValue;

        [SerializeField] [ReadOnly] protected T previousValue;

        [SerializeField] protected T value;

        [SerializeField] protected bool debugChange;

        public T InitialValue
        {
            get => initialValue;
            set => initialValue = value;
        }

        public T v
        {
            get => value;
            set
            {
                previousValue = this.value;
                this.value = value;
//#if !UNITY_EDITOR
            if (debugChange) Debug.Log($"{this.name} is set to : {value}", this);
//#endif
                TriggerChange();
            }
        }

        public T PreviousValue => previousValue;

        protected override void OnInit()
        {
            v = initialValue;
        }

        public virtual void SetValue(T newVal)
        {
            v = newVal;
#if !UNITY_EDITOR
            if (debugChange) Debug.Log($"{this.name} is set to : {value}", this);
#endif
        }
    }
}