using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace UnityReusables.Events
{
    public class BetterEventSOListener : BaseEventSOListener<BetterEvenSO>
    {
        [HideIf("ordered")] [SerializeField] private BetterEvent actions;

        [SerializeField] private bool ordered;

        [ShowIf("ordered")] [SerializeField] private BetterEvent orderedActions;

        private BetterEvent Actions
        {
            get
            {
                if (actions.Equals(null))
                    actions = new BetterEvent();
                return actions;
            }
        }

        private BetterEvent OrderedActions
        {
            get
            {
                if (orderedActions.Equals(null))
                    orderedActions = new BetterEvent();
                return orderedActions;
            }
        }

        public void Invoke()
        {
            BetterEvent betterEvent = ordered ? OrderedActions : Actions;

            foreach (var eventEntry in betterEvent.Events.Where(eventEntry => eventEntry != null))
            {
                eventEntry.Invoke();
            }
        }

        public void AddCallback(UnityAction callback)
        {
            if (ordered)
            {
                orderedActions.AddCallback(callback);
            }
            else
            {
                actions.AddCallback(callback);
            }
        }
    }
}