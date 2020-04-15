using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

/*
 * Created by CreArthur - 2019
 * Class for updating a TMP_Text based on a IntVariable
 * Support prefix, suffix and value offset
 */

namespace UnityReusables.ScriptableDef.TMPTextListeners
{
    [RequireComponent(typeof(TMP_Text))]
    public class IntTMPTextListener : MonoBehaviour
    {
        public IntVariable variable;

        public string prefix;
        public string suffix;

        public bool isValueOffset;
        [ShowIf("isValueOffset")] public int valueOffset;

        private TMP_Text m_text;

        private void Start()
        {
            m_text = GetComponent<TMP_Text>();
            variable.AddOnChangeCallback(SetText);
            SetText();
        }

        private void SetText()
        {
            m_text.text = $"{prefix}{(isValueOffset ? variable.v + valueOffset : variable.v)}{suffix}";
        }

        private void OnDestroy()
        {
            variable.RemoveOnChangeCallback(SetText);
        }
    }
}