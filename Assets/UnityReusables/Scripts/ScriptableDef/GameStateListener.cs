using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    [Serializable]
    class GameStateCallbacks
    {
        public BetterEvent onEnter, onLeave;
    }

    public class GameStateListener : SerializedMonoBehaviour
    {
        [SerializeField]
        private Dictionary<GameState, GameStateCallbacks> callbacks = new Dictionary<GameState, GameStateCallbacks>();

        [SerializeField] private GameStateVariable currentState;

        private void Start()
        {
            currentState.AddOnChangeCallback(OnChange);
        }

        void OnChange()
        {
            if (callbacks.ContainsKey(currentState.PreviousValue))
            {
                callbacks[currentState.PreviousValue].onLeave.Invoke();
            }

            if (callbacks.ContainsKey(currentState.v))
            {
                callbacks[currentState.v].onEnter.Invoke();
            }
        }

        private void OnDestroy()
        {
            currentState.RemoveOnChangeCallback(OnChange);
        }

        public GameStateVariable GetCurrentState()
        {
            return currentState;
        }

        /// <summary>Add onEnter and/or onLeave callbacks to a GameState through code</summary>
        /// <param name="gameState">The key of the dictionary to which the callbacks will be added</param>
        /// <param name="onEnter">the callbacks to add on the onEnter BetterEvent list</param>
        /// <param name="onLeave">the callbacks to add on the onLeave BetterEvent list</param>
        public void AddCallback(GameState gameState, BetterEvent onEnter, BetterEvent onLeave)
        {
            // create new game state callbacks if it is not present
            if (!callbacks.ContainsKey(gameState))
            {
                callbacks[gameState] = new GameStateCallbacks
                {
                    onEnter = new BetterEvent {Events = new List<BetterEventEntry>()},
                    onLeave = new BetterEvent {Events = new List<BetterEventEntry>()}
                };
            }

            // initialise the game state callbacks if it is present but null
            if (callbacks[gameState] == null)
                callbacks[gameState] = new GameStateCallbacks
                {
                    onEnter = new BetterEvent {Events = new List<BetterEventEntry>()},
                    onLeave = new BetterEvent {Events = new List<BetterEventEntry>()}
                };

            // add the enEnter and onLeave callbacks to the state
            var gameStateCallbacks = callbacks[gameState];
            gameStateCallbacks.onEnter.Events.AddRange(onEnter.Events);
            gameStateCallbacks.onLeave.Events.AddRange(onLeave.Events);
        }
    }
}