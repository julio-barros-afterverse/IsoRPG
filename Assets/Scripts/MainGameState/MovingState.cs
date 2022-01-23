using System.Collections.Generic;
using Model;
using UnityEngine;

namespace MainGameState
{
    public class MovingState: MainGameState
    {
        private readonly LocalPlayerSystem _localPlayerSystem;
        private readonly BoardSystem _boardSystem;
        private readonly MainGameStateMachine _stateMachine;

        public MovingState(
            LocalPlayerSystem localPlayerSystem,
            BoardSystem boardSystem,
            MainGameStateMachine stateMachine
        )
        {
            _localPlayerSystem = localPlayerSystem;
            _boardSystem = boardSystem;
            _stateMachine = stateMachine;
        }

        public override void OnSelectTile(TileSystem tile)
        {
            var distance = Hex.Distance(_localPlayerSystem.CurrentTile.Position, tile.Position);
            if (distance <= 0)
            {
                return;
            }

            Queue<Vector3> path = new Queue<Vector3>();
            for (int i = 0; i <= distance; i++)
            {
                var coord = Hex.Lerp(_localPlayerSystem.CurrentTile.Position, tile.Position, (1.0f / distance) * i);
                var point = _boardSystem.Coordinates[coord];
                point.SetOnPath(true);
                path.Enqueue(point.transform.position);
            }
            _localPlayerSystem.Move2D(path);
            _localPlayerSystem.CurrentTile = tile;

            _stateMachine.CurrentState = new AimingState(
                _localPlayerSystem,
                _boardSystem,
                this,
                _stateMachine
            );
        }
    }
}