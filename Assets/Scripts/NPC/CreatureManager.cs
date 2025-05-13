
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CreatureManager : MonoBehaviour
{
    public CreatureDatabase database;
    public List<Creature> activeCreatures = new();

    public void AddCreature(string name)
    {
        var data = database.GetCreatureDataByName(name);
        if (data != null)
        {
            Creature creature = data.Create();
            activeCreatures.Add(creature);
            Debug.Log($"Creature {name} added!");
        }
    }

    public void SendToCollect(string name)
    {
        var creature = activeCreatures.Find(c => c.Name == name);
        if (creature != null && creature.State == CreatureState.Ready)
        {
            creature.StartCollecting();
            Debug.Log($"{name} started collecting!");
        }
    }

    public void OnNextDay()
    {
        foreach (var creature in activeCreatures)
        {
            if (creature.State == CreatureState.Searching)
            {
                var data = database.GetCreatureDataByName(creature.Name);
                creature.FinishCollecting(data.collectionData);
                Debug.Log($"{creature.Name} finished collecting!");
            }
        }
    }

    public CollectionData[] CollectItems(string name)
    {
        var creature = activeCreatures.Find(c => c.Name == name);
        if (creature != null && creature.State == CreatureState.Done)
        {
            return creature.ReceiveResources();
        }
        return null;
    }
}
