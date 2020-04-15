using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.Events
{
    public class IntPEventListenerSO : BasePEventListenerSO<int>
    {
        [OnValueChanged("OnEventChange")]
        [SerializeField] protected new List<IntPEventSO> gameEvents;

        private void OnEventChange()
        {
            GameEvents = gameEvents;
        }
        private List<IntPEventSO> GameEvents
        {
            get => base.gameEvents.ConvertAll(x => (IntPEventSO) x);
            set => base.gameEvents = new List<BasePEventSO<int>>(value);
        }
    }
}