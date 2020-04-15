using System;
using UnityEngine;

namespace UnityReusables.ScriptableDef
{
    public enum GameState
    {
        Starting,
        Menu,
        InGame,
        Paused,
        StepTransition,
        LevelEnding,
        LevelCompleted,
        GameOver,
        Shop
    }

    [CreateAssetMenu(menuName = "Variable/GameState")]
    public class GameStateVariable : BaseVariable<GameState>
    {
        public bool isStepToMenu; // if false StepTransition goto level 
        public bool isGameOverToMenu; // if false game over goto level 
        public bool isLevelEnding; // if false InGame goto LevelCompleted

        public override void SetValue(GameState newVal)
        {
            if (v == newVal)
            {
                Debug.LogWarning($"switching from and to the same state {newVal} is not allowed");
                return;
            }

            string debug = "";
            switch (newVal)
            {
                case GameState.Starting:
                    debug = $"Game state try to switch to {newVal}, which is never possible (current state :{v})";
                    break;
                case GameState.Menu:
                    switch (v)
                    {
                        case GameState.Starting:
                        case GameState.InGame:
                        case GameState.LevelCompleted:
                        case GameState.Shop:
                            v = newVal;
                            break;

                        case GameState.StepTransition:
                            if (isStepToMenu)
                                v = newVal;
                            else debug = $"Game state try to switch from {v} to {newVal}, but isStepToMenu is false";
                            break;

                        case GameState.GameOver:
                            if (isGameOverToMenu)
                                v = newVal;
                            else
                                debug = $"Game state try to switch from {v} to {newVal}, but isGameOverToMenu is false";
                            break;
                        default:
                            debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                            break;
                    }

                    break;

                case GameState.InGame:
                    switch (v)
                    {
                        case GameState.Paused:
                            v = newVal;
                            break;
                        
                        case GameState.Menu:
                            v = newVal;
                            break;

                        case GameState.StepTransition:
                            if (!isStepToMenu)
                                v = newVal;
                            else debug = $"Game state try to switch from {v} to {newVal}, but isStepToMenu is true";
                            break;

                        case GameState.GameOver:
                            if (!isGameOverToMenu)
                                v = newVal;
                            else debug = $"Game state try to switch from {v} to {newVal}, but isGameOverToMenu is true";
                            break;
                        default:
                            debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                            break;
                    }
                    break;
                
                case GameState.Paused:
                    if (v == GameState.InGame)
                        v = newVal;
                    else debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                    break;

                case GameState.StepTransition:
                    if (v == GameState.InGame)
                        v = newVal;
                    else debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                    break;

                case GameState.LevelEnding:
                    if (v == GameState.InGame)
                        if (isLevelEnding)
                            v = newVal;
                        else debug = $"Game state try to switch from {v} to {newVal}, but isLevelEnding is false";
                    else debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                    break;

                case GameState.LevelCompleted:
                    switch (v)
                    {
                        case GameState.InGame  when isLevelEnding:
                            debug = $"Game state try to switch from {v} to {newVal}, but isLevelEnding is true";
                            break;
                        case GameState.InGame:
                            v = newVal;
                            break;
                        case GameState.LevelEnding when isLevelEnding:
                            v = newVal;
                            break;
                        case GameState.LevelEnding:
                            debug = $"Game state try to switch from {v} to {newVal}, but isLevelEnding is false";
                            break;
                        default:
                            debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                            break;
                    }
                    break;

                case GameState.GameOver:
                    if (v == GameState.InGame)
                        v = newVal;
                    else debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                    break;
                
                case GameState.Shop:
                    if (v == GameState.Menu)
                        v = newVal;
                    else debug = $"Game state try to switch from {v} to {newVal}, which is not possible";
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(newVal), newVal, null);
            }

            if (debug == "") Debug.Log($"Game state switch from {PreviousValue} to {newVal}");
            else Debug.LogWarning(debug);
        }
    }
}