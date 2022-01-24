using Model;
using UnityEngine;
using UnityEngine.UI;

namespace MainGameState
{
    public class MainGameStateMachine: MonoBehaviour
    {
        [SerializeField] private LocalPlayerSystem localPlayerSystem;
        [SerializeField] private BoardSystem boardSystem;
        public Text ActionText;
        public Button ActionButtonPrefab;
        private MainGameState _currentState;
        public MainGameState CurrentState => _currentState;

        public void ChangeState(MainGameState nextState)
        {
            _currentState.OnExit();
            _currentState = nextState;
            _currentState.OnEnter();
        }

        private void Awake()
        {
            _currentState = new AimingState(localPlayerSystem, boardSystem, this);
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