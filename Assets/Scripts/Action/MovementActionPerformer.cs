using Model;
using Model.Movement;
using UnityEngine;

namespace Action
{
    public class MovementActionPerformer: MonoBehaviour, IActionPerformer
    {
        // [SerializeField] private TileMovementSystem tileMovementSystem;
        public void Perform(IAction action)
        {
            if (!(action is MovementAction movement))
            {
                return;
            }
            
           //  tileMovementSystem.OnMoveToTile(movement.Target);
        }

        public bool Supports(IAction action)
        {
            return action is MovementAction;
        }
    }
}