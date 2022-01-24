using System;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MainGameState
{
    public class SelectActionState: MainGameState
    {
        private const int minX = -300;
        private const int maxX = 300;
        private readonly Button _actionButtonPrefab;
        private readonly MainGameStateMachine _stateMachine;
        private readonly List<ActionConfiguration> _actionConfigurations;
        private Dictionary<string, Button> buttonByConfig = new Dictionary<string, Button>();

        public SelectActionState(
            MainGameStateMachine stateMachine,
            Button actionButtonPrefab
        )
        {
            _stateMachine = stateMachine;
            _actionButtonPrefab = actionButtonPrefab;
        }

        public override void OnEnter()
        {
            var t = 1f;
            var maxT = _actionConfigurations.Count + 1;
            foreach (var actionConfiguration in _actionConfigurations)
            {
                var button = Object.Instantiate(_actionButtonPrefab);
                buttonByConfig[actionConfiguration.Name()] = button;
                button.GetComponentInChildren<Text>().text = actionConfiguration.Name();
                var position = (float) Util.IntLerp(minX, maxX, t / maxT);
                var transform = button.transform;
                var buttonPos = transform.position;
                transform.position = new Vector3(position, buttonPos.y, buttonPos.z);
                t += 1;
            }
        }
    }
}