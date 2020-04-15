using UnityEngine;

namespace UnityReusables.Utils.BetterColliderTrigger
{
    public class BetterTriggerExit : BetterColliderOrTrigger
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(otherTag)) events.Invoke();
        }
    }
}