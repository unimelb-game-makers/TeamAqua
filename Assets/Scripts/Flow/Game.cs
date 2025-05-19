using System.Collections.Generic;
using Kuroneko.UtilityDelivery;
using UnityEngine;

public class Game
{
    public static List<MonoBehaviour> managers = new();

    // This is pretty bad code, but oh well :D
    public static void AddManager(MonoBehaviour behaviour, bool force = false)
    {
        if (behaviour)
        {
            // Let's not add the same type
            for (int i = 0; i < managers.Count; ++i)
            {
                if (managers[i].GetType() == behaviour.GetType())
                {
                    if (force)
                    {
                        managers.RemoveAt(i);
                        managers.Add(behaviour);
                    }
                    return;
                }
            }

            managers.AddOnce(behaviour);
        }
    }

    public static void RemoveManager(MonoBehaviour behaviour)
    {
        managers.Remove(behaviour);
    }
}