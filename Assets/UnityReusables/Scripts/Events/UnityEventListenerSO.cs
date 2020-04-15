using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace UnityReusables.Events
{
    public class UnityEventListenerSO : BaseEventSOListener<UnityEventSO>
    {
        [HideIf("ordered")] [SerializeField] private List<UnityEvent> actions;

        [SerializeField] private bool ordered;

        [ShowIf("ordered")] [SerializeField] private List<UnityEvent> orderedActions;

        private List<UnityEvent> Actions
        {
            get
            {
                if (actions.Equals(null))
                    actions = new List<UnityEvent>();
                return actions;
            }
        }

        private List<UnityEvent> OrderedActions
        {
            get
            {
                if (orderedActions.Equals(null))
                    orderedActions = new List<UnityEvent>();
                return orderedActions;
            }
        }

        public void Invoke()
        {
            List<UnityEvent> actions = ordered ? OrderedActions : Actions;
            foreach (var eventEntry in actions)
                eventEntry.Invoke();
        }
    }
}