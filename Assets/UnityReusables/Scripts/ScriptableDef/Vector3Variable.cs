using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    [CreateAssetMenu(menuName = "Variable/Vector3")]
    public class Vector3Variable : BaseVariable<Vector3>
    {
        public void SetValFromTransformPos(Transform t)
        {
            SetValue(t.position);
        }
    }
}