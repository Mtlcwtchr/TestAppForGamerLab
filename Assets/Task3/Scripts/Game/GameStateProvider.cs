using System;
using System.Collections;
using System.Collections.Generic;
using Task3.Scripts.Game.UI;
using UnityEngine;

namespace Task3.Scripts.Game
{
    public class GameStateProvider : MonoBehaviour
    {
        private static GameStateProvider _instance;
        public static GameStateProvider Instance => _instance;

        public bool IsPaused => _currentState != GameState.Gameplay;

        private GameState _currentState = GameState.None;

        private List<GameState> _states = new()
        {
            GameState.None,
            GameState.Launch,
            GameState.Gameplay,
            GameState.Endgame
        };

        private void Awake()
        {
            _instance = this;
            StartCoroutine(ProceedStartup());
        }

        private IEnumerator ProceedStartup()
        {
            yield return null;
            
            GoNextState();
        }

        public void GoNextState()
        {
            var currentStateIndex = _states.IndexOf(_currentState);
            if (currentStateIndex == -1)
            {
                return;
            }

            ++currentStateIndex;
            currentStateIndex %= _states.Count;
            TrySetState(_states[currentStateIndex]);
        }

        public void TrySetState(GameState state)
        {
            if (CanTransitState(_currentState, state))
            {
                ProceedState(_currentState, _currentState = state);
            }
        }

        private void ProceedState(GameState from, GameState to)
        {
            switch (from)
            {
                case GameState.None:
                    break;
                case GameState.Launch:
                    UIManager.Instance.SetLaunchGUIVisible(false);
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.Endgame:
                    UIManager.Instance.SetEndgameGUIVisible(false);
                    break;
                default:
                    break;
            }
            
            switch (to)
            {
                case GameState.None:
                    break;
                case GameState.Launch:
                    UIManager.Instance.SetLaunchGUIVisible(true);
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.Endgame:
                    UIManager.Instance.SetEndgameGUIVisible(true);
                    break;
                default:
                    break;
            }
        }

        private bool CanTransitState(GameState from, GameState to) =>
            from switch
            {
                GameState.None => to == GameState.Launch,
                GameState.Launch => to == GameState.Gameplay,
                GameState.Gameplay => to == GameState.Endgame,
                GameState.Endgame => false,
                _ => false
            };
    }
}