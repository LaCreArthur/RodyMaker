using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    public class BoolVariableListener : SerializedMonoBehaviour
    {
        public bool logOnChange;
        public BoolVariable variable;
        public BetterEvent onTrue;
        public BetterEvent onFalse;

        private void OnEnable()
        {
            variable.AddOnChangeCallback(OnChange);
        }

        private void OnDisable()
        {
            variable.RemoveOnChangeCallback(OnChange);
        }

        private void OnChange()
        {
            BetterEvent events = variable.v ? onTrue : onFalse;
            if (logOnChange) Debug.Log(variable.v ? "onTrue Called" : "onFalse called");
            events.Invoke();
        }
    }
}