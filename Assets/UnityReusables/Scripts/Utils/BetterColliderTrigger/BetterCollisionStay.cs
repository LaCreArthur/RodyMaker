using UnityEngine;

namespace UnityReusables.Utils.BetterColliderTrigger
{
    public class BetterCollisionStay : BetterColliderOrTrigger
    {
        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag(otherTag)) events.Invoke();
        }
    }
}