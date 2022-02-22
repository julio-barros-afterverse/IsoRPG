using System.Collections;
using System.Collections.Generic;
using Model;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameState
{
    public class MovingState: MainGameState
    {
        private readonly LocalPlayerSystem _localPlayerSystem;
        private readonly BoardSystem _boardSystem;
        private readonly MainGameStateMachine _stateMachine;
        private readonly TileSystem _tile;

        public MovingState(
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
            var distance = Hex.Distance(_localPlayerSystem.CurrentTile.Position, _tile.Position);
            if (distance <= 0)
            {
                yield break;
            }

            Queue<Vector3> path = new Queue<Vector3>();
            for (int i = 0; i <= distance; i++)
            {
                var coord = Hex.Lerp(_localPlayerSystem.CurrentTile.Position, _tile.Position, (1.0f / distance) * i);
                var point = _boardSystem.Coordinates[coord];
                point.SetOnPath(true);
                path.Enqueue(point.transform.position);
            }
            _localPlayerSystem.Move2D(path);
            _localPlayerSystem.CurrentTile = _tile;

            _stateMachine.ChangeState(new SelectActionState(
                _stateMachine,
                _stateMachine.OptionButtonPrefab
            ));
        }

        public override IEnumerator OnExit()
        {
            foreach (var coord in _boardSystem.Coordinates.Values)
            {
                coord.SetOnPath(false);
            }

            yield break;
        }
    }
}