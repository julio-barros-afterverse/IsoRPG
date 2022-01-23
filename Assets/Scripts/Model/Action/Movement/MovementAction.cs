namespace Model.Movement
{
    public class MovementAction: IAction
    {
        public readonly Hex Target;

        public MovementAction(Hex target)
        {
            Target = target;
        }
    }
}