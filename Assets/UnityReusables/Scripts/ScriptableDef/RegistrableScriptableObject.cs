using System;
using Sirenix.OdinInspector;

namespace UnityReusables.ScriptableDef
{
    public class RegistrableScriptableObject : SerializedScriptableObject
    {
        private Action m_onChange;

        private void OnEnable()
        {
            OnInit();
        }

        protected virtual void OnInit()
        {
        }

        protected void TriggerChange()
        {
            m_onChange?.Invoke();
        }

        public void AddOnChangeCallback(Action callback)
        {
            m_onChange += callback;
        }

        public void RemoveOnChangeCallback(Action callback)
        {
            m_onChange -= callback;
        }
    }
}