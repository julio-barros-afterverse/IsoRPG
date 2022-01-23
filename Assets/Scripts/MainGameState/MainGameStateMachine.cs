using Model;
using UnityEngine;

namespace MainGameState
{
    public class MainGameStateMachine: MonoBehaviour
    {
        [SerializeField] private LocalPlayerSystem localPlayerSystem;
        [SerializeField] private BoardSystem boardSystem;
        public MainGameState CurrentState;

        private void Awake()
        {
            CurrentState = new MovingState(localPlayerSystem, boardSystem, this);
            boardSystem.OnFinished(() =>
            {
                foreach (var coord in boardSystem.Coordinates.Values)
                {
                    coord.RegisterMainGameStateMachine(this);
                }
    
                var zeroTile = boardSystem.Coordinates[new Hex(0, 0)];
                localPlayerSystem.CurrentTile = zeroTile;
                localPlayerSystem.Move2D(zeroTile.transform.position);
            });
        }
    }
}