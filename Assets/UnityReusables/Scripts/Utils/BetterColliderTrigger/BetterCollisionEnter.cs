using UnityEngine;

namespace UnityReusables.Utils.BetterColliderTrigger
{
    public class BetterCollisionEnter : BetterColliderOrTrigger
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag(otherTag)) events.Invoke();
        }
    }
}