using System.Collections.Generic;

namespace UnityReusables.ScriptableDef
{
    public class BaseListVariable<T> : BaseVariable<List<T>>
    {
        public T this[int i]
        {
            get => v[i];
            set => v[i] = value;
        }


        public void Add(T x)
        {
            v.Add(x);
        }

        public int Count => v.Count;
    }
}