using TMPro;
using UnityEngine;

namespace UnityReusables.ScriptableDef.TMPTextListeners
{
    public class GameStateTMPTextListener : MonoBehaviour
    {
        public GameStateVariable variable;

        public string prefix;
        public string suffix;

        private TMP_Text m_text;

        public bool isTimer;
        private float stateTime;
        private string currentState;

        private void Start()
        {
            m_text = GetComponent<TMP_Text>();
            variable.AddOnChangeCallback(SetText);
            SetText();
        }

        private void SetText()
        {
            currentState = variable.v.ToString();

            if (isTimer)
                stateTime = 0;
            else
                m_text.text = $"{prefix}{currentState}{suffix}";
        }

        private void Update()
        {
            if (!isTimer)
                return;
            stateTime += Time.deltaTime;
            m_text.text = $"{prefix}{currentState} ({stateTime:0.0}){suffix}";
        }

        private void OnDestroy()
        {
            variable.RemoveOnChangeCallback(SetText);
        }
    }
}