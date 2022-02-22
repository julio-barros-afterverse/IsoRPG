using System.Collections;
using Model;
using Model.Action;

namespace MainGameState
{
    public class AimingState: MainGameState
    {
        private readonly LocalPlayerSystem _localPlayerSystem;
        private readonly BoardSystem _boardSystem;
        private readonly MainGameStateMachine _stateMachine;
        private readonly ActionConfiguration _actionConfiguration;

        public AimingState(LocalPlayerSystem localPlayerSystem,
            BoardSystem boardSystem,
            MainGameStateMachine stateMachine,
            ActionConfiguration actionConfiguration
        )
        {
            _localPlayerSystem = localPlayerSystem;
            _boardSystem = boardSystem;
            _stateMachine = stateMachine;
            _actionConfiguration = actionConfiguration;
        }

        public override void OnSelectTile(TileSystem tile)
        {
            switch (_actionConfiguration)
            {
                case MovementActionConfiguration _move:
                    _stateMachine.ChangeState(new MovingState(
                        _localPlayerSystem,
                        _boardSystem,
                        _stateMachine,
                        tile
                    ));
                    break;
                case LightningActionConfiguration _lightning:
                    _stateMachine.ChangeState(new LightningState(
                        _localPlayerSystem,
                        _boardSystem,
                        _stateMachine,
                        tile
                    ));
                    break;
            }
        }

        public override void OnHoverTile(TileSystem tile)
        {
            // draw line to tile
            var distance = Hex.Distance(_localPlayerSystem.CurrentTile.Position, tile.Position);
            if (distance <= 0)
            {
                return;
            }
            for (int i = 0; i <= distance; i++)
            {
                var coord = Hex.Lerp(_localPlayerSystem.CurrentTile.Position, tile.Position, (1.0f / distance) * i);
                var point = _boardSystem.Coordinates[coord];
                point.SetOnPath(true);
            }
        }
        
        public override void OnUnhoverTile(TileSystem tile)
        {
            foreach (var coord in _boardSystem.Coordinates.Values)
            {
                coord.SetOnPath(false);
            }
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