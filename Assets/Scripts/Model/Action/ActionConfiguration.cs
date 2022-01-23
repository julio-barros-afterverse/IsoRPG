using System.Collections.Generic;

namespace Model
{
    public abstract class ActionConfiguration
    {
        public readonly int MaxReach;
        public readonly int MinReach;

        protected ActionConfiguration(int maxReach, int minReach)
        {
            MaxReach = maxReach;
            MinReach = minReach;
        }

        public abstract AimStrategy AimStrategy();
        public abstract List<TargetPermissions> TargetPermissions();
        public abstract IAction GenerateAction(Hex target);
    }

    public enum AimStrategy
    {
        Linear
    }

    public enum TargetPermissions
    {
        Players
    }
}