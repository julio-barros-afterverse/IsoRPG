using Model;

namespace MainGameState
{
    public class AimingState: MainGameState
    {
        private readonly LocalPlayerSystem _localPlayerSystem;
        private readonly BoardSystem _boardSystem;
        private readonly MainGameStateMachine _stateMachine;

        public AimingState(
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
            _stateMachine.ChangeState(new MovingState(_localPlayerSystem, _boardSystem, _stateMachine, tile));
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
    }
}