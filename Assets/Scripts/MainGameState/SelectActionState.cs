using System;
using System.Collections;
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
            _actionConfigurations = _stateMachine.ActionConfigurations;
        }

        public override IEnumerator OnEnter()
        {
            _stateMachine.SelectActionText.enabled = true;
            var t = 1f;
            var maxT = _actionConfigurations.Count + 1;
            foreach (var actionConfiguration in _actionConfigurations)
            {
                var button = Object.Instantiate(_actionButtonPrefab);
                buttonByConfig[actionConfiguration.Name()] = button;
                button.GetComponentInChildren<Text>().text = actionConfiguration.Name();
                var position = (float) Util.IntLerp(minX, maxX, t / maxT);
                var transform = button.transform;
                transform.SetParent(_stateMachine.Canvas.transform);
                var buttonPos = transform.position;
                transform.localPosition = new Vector3(position, -171f, buttonPos.z);
                t += 1;
                button.onClick.AddListener(() =>
                {
                    _stateMachine.ChangeState(new AimingState(
                        _stateMachine.LocalPlayerSystem,
                        _stateMachine.BoardSystem,
                        _stateMachine,
                        actionConfiguration
                        ));
                });
            }

            yield break;
        }

        public override IEnumerator OnExit()
        {
            _stateMachine.SelectActionText.enabled = false;
            foreach (var configButton in buttonByConfig)
            {
                Object.Destroy(configButton.Value.gameObject);
            }
            buttonByConfig.Clear();

            yield break;
        }
    }
}