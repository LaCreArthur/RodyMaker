using UnityEngine;

namespace UnityReusables.Utils.BetterColliderTrigger
{
    public class BetterTriggerEnter : BetterColliderOrTrigger
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(otherTag))
            {
                events.Invoke();
                if (onlyOnce) Destroy(gameObject);
            }
        }
    }
}