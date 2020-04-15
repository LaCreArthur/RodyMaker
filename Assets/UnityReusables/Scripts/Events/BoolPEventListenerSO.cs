using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.Events
{
    public class BoolPEventListenerSO : BasePEventListenerSO<bool>
    {
        [OnValueChanged("OnEventChange")]
        [SerializeField] protected new List<BoolPEventSO> gameEvents;

        private void OnEventChange()
        {
            GameEvents = gameEvents;
        }
        private List<BoolPEventSO> GameEvents
        {
            get => base.gameEvents.ConvertAll(x => (BoolPEventSO) x);
            set => base.gameEvents = new List<BasePEventSO<bool>>(value);
        }
    }
}