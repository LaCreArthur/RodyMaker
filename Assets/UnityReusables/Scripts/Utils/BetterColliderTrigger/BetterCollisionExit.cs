using UnityEngine;

namespace UnityReusables.Utils.BetterColliderTrigger
{
    public class BetterCollisionExit : BetterColliderOrTrigger
    {
        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag(otherTag)) events.Invoke();
        }
    }
}