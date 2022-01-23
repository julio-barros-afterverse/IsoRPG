using System.Collections.Generic;

namespace Model.Movement
{
    public class MovementActionConfiguration: ActionConfiguration
    {
        public MovementActionConfiguration(int maxReach, int minReach) : base(maxReach, minReach)
        {
        }

        public override AimStrategy AimStrategy()
        {
            return Model.AimStrategy.Linear;
        }

        public override List<TargetPermissions> TargetPermissions()
        {
            return new List<TargetPermissions>();
        }

        public override IAction GenerateAction(Hex target)
        {
            return new MovementAction(target);
        }
    }
}