public enum CreatureState
{
    Ready,
    Searching,
    Done
}

public class Creature
{
    public string Name;
    public CreatureState State = CreatureState.Ready;
    public CollectionData[] CollectedItems;

    public Creature(string name)
    {
        Name = name;
        State = CreatureState.Ready;
    }

    public void StartCollecting()
    {
        if (State == CreatureState.Ready)
        {
            State = CreatureState.Searching;
        }
    }

    public void FinishCollecting(CollectionData[] collected)
    {
        State = CreatureState.Done;
        CollectedItems = collected;
    }

    public CollectionData[] ReceiveResources()
    {
        if (State == CreatureState.Done)
        {
            State = CreatureState.Ready;
            var result = CollectedItems;
            CollectedItems = null;
            return result;
        }
        return null;
    }
}
