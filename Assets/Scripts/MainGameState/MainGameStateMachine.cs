using System;
using System.Collections.Generic;
using Model;
using Model.Action;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameState
{
    public class MainGameStateMachine: MonoBehaviour
    {
        public LocalPlayerSystem LocalPlayerSystem;
        public BoardSystem BoardSystem;
        public Text SelectActionText;
        public Button OptionButtonPrefab;
        public Canvas Canvas;
        public GameObject LightningPrefab;
        public List<ActionConfiguration> ActionConfigurations = new List<ActionConfiguration>()
        {
            new MovementActionConfiguration(0, 3), new LightningActionConfiguration(0, 3)
        };
        private MainGameState _currentState;
        private MainGameState _nextState;
        public MainGameState CurrentState => _currentState;

        public void ChangeState(MainGameState nextState)
        {
            _nextState = nextState;
        }

        private void Awake()
        {
            _currentState = new SelectActionState(this, OptionButtonPrefab);
            _nextState = _currentState;
            BoardSystem.OnFinished(() =>
            {
                foreach (var coord in BoardSystem.Coordinates.Values)
                {
                    coord.RegisterMainGameStateMachine(this);
                }
    
                var zeroTile = BoardSystem.Coordinates[new Hex(0, 0)];
                LocalPlayerSystem.CurrentTile = zeroTile;
                LocalPlayerSystem.Move2D(zeroTile.transform.position);
            });
        }

        private void Start()
        {
            StartCoroutine(_currentState.OnEnter());
        }

        private void Update()
        {
            if (_nextState == _currentState) return;
            Debug.Log("changing");
            StartCoroutine(_currentState.OnExit());
            _currentState = _nextState;
            StartCoroutine(_currentState.OnEnter());
        }
    }
}