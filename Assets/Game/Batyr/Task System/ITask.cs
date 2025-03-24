namespace Game.Batyr.Task_System
{
    public interface ITask
    {
        bool IsCompleted();
        string GetDescription();
    }
}