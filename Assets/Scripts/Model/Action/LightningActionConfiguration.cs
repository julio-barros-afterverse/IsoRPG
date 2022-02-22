using System.Collections.Generic;

namespace Model.Action
{
    public class LightningActionConfiguration: ActionConfiguration
    {
        public LightningActionConfiguration(int maxReach, int minReach) : base(maxReach, minReach)
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

        public override string Name()
        {
            return "Lightning";
        }
    }
}