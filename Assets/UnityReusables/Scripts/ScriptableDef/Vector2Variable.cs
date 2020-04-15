using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    [CreateAssetMenu(menuName = "Variable/Vector2")]
    public class Vector2Variable : BaseVariable<Vector2>
    {
        public void SetValFromTransformPos(Transform t)
        {
            SetValue(t.position);
        }
    }
}