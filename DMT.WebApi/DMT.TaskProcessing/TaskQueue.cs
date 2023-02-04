namespace DMT.TaskProcessing;

public class TaskQueue
{
    private static readonly TaskQueue Instance = new TaskQueue();

    public string Date { get; private set; }
    
    private TaskQueue()
    {
        Date = "";
    }
    
    public static TaskQueue GetInstance()
    {
        return Instance;
    }
}