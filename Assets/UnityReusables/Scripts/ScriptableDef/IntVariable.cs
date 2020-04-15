using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    [CreateAssetMenu(menuName = "Variable/Int")]
    public class IntVariable : BaseVariable<int>
    {
        public void Add(int x)
        {
            v += x;
        }

        public static IntVariable operator ++(IntVariable a)
        {
            a.v++;
            return a;
        }

        public static IntVariable operator --(IntVariable a)
        {
            a.v--;
            return a;
        }

        public static IntVariable operator +(IntVariable a)
        {
            return a;
        }

        public static IntVariable operator +(IntVariable a, IntVariable b)
        {
            var res = CreateInstance<IntVariable>();
            res.v = a.v + b.v;
            return res;
        }

        public static IntVariable operator -(IntVariable a)
        {
            a.v = -a.v;
            return a;
        }

        public static IntVariable operator -(IntVariable a, IntVariable b)
        {
            var res = CreateInstance<IntVariable>();
            res.v = a.v - b.v;
            return res;
        }
    }
}