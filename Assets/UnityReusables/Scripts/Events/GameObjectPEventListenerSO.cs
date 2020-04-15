using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityReusables.Events
{
    public class GameObjectPEventListenerSO : BasePEventListenerSO<GameObject>
    {
        [OnValueChanged("OnEventChange")]
        [SerializeField] protected new List<GameObjectPEventSO> gameEvents;

        private void OnEventChange()
        {
            GameEvents = gameEvents;
        }
        private List<GameObjectPEventSO> GameEvents
        {
            get => base.gameEvents.ConvertAll(x => (GameObjectPEventSO) x);
            set => base.gameEvents = new List<BasePEventSO<GameObject>>(value);
        }
    }
}