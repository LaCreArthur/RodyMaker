using UnityEngine;
using UnityReusables.Utils.LayerTagDropdown;

namespace UnityReusables.Utils.BetterColliderTrigger
{
    [RequireComponent(typeof(Collider))]
    public class BetterColliderOrTrigger : MonoBehaviour
    {
        [SerializeField, TagDropdown] protected string otherTag = "";

        [SerializeField] protected bool onlyOnce;

        [SerializeField] protected BetterEvent events;
    }
}