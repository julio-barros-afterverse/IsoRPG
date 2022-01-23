using Model;

public interface IActionPerformer
{
    void Perform(IAction action);
    bool Supports(IAction action);
}