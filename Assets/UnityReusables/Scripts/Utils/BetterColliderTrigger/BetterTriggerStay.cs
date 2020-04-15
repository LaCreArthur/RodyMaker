using UnityEngine;

namespace UnityReusables.Utils.BetterColliderTrigger
{
    public class BetterTriggerStay : BetterColliderOrTrigger
    {
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(otherTag)) events.Invoke();
        }
    }
}