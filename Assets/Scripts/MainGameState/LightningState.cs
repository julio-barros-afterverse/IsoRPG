using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;

namespace MainGameState
{
    public class LightningState: MainGameState
    {
        private readonly LocalPlayerSystem _localPlayerSystem;
        private readonly BoardSystem _boardSystem;
        private readonly MainGameStateMachine _stateMachine;
        private readonly TileSystem _tile;

        public LightningState(
            LocalPlayerSystem localPlayerSystem,
            BoardSystem boardSystem,
            MainGameStateMachine stateMachine,
            TileSystem tile
        )
        {
            _localPlayerSystem = localPlayerSystem;
            _boardSystem = boardSystem;
            _stateMachine = stateMachine;
            _tile = tile;
        }
        public override IEnumerator OnEnter()
        {
            var lightning = Object.Instantiate(_stateMachine.LightningPrefab);
            var line = lightning.GetComponent<LineRenderer>();
            line.widthCurve = AnimationCurve.Linear(0f, 3f, 1f, 3.5f);
            var mat = line.materials[0];
            line.materials = new[] { mat, mat, mat, mat, mat };

            var start = lightning.transform.Find("LightningStart");
            var end = lightning.transform.Find("LightningEnd");
            var startPos = _localPlayerSystem.CurrentTile.transform.position;
            var tgtPos = _tile.transform.position;
            var playerTransform = _localPlayerSystem.transform;
            var playerPos = playerTransform.position;
            
            playerTransform.LookAt(tgtPos);
            _localPlayerSystem.Lightning();
            start.transform.position = new Vector3(startPos.x, playerPos.y, startPos.z);
            end.transform.position =  new Vector3(tgtPos.x, playerPos.y, tgtPos.z);

            yield return new WaitForSeconds(1);
            
            Object.Destroy(lightning);

            _stateMachine.ChangeState(new SelectActionState(
                _stateMachine,
                _stateMachine.OptionButtonPrefab
            ));
            yield break;
        }
    }
}