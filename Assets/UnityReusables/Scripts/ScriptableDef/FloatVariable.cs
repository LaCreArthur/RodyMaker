using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    [CreateAssetMenu(menuName = "Variable/Float")]
    public class FloatVariable : BaseVariable<float>
    {
        public static FloatVariable operator ++(FloatVariable a)
        {
            a.v++;
            return a;
        }

        public static FloatVariable operator --(FloatVariable a)
        {
            a.v--;
            return a;
        }

        public static FloatVariable operator +(FloatVariable a)
        {
            return a;
        }

        public static FloatVariable operator +(FloatVariable a, FloatVariable b)
        {
            var res = CreateInstance<FloatVariable>();
            res.v = a.v + b.v;
            return res;
        }

        public static FloatVariable operator -(FloatVariable a)
        {
            a.v = -a.v;
            return a;
        }

        public static FloatVariable operator -(FloatVariable a, FloatVariable b)
        {
            var res = CreateInstance<FloatVariable>();
            res.v = a.v - b.v;
            return res;
        }
    }
}