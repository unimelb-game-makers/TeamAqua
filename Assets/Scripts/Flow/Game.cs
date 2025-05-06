using System.Collections.Generic;
using Kuroneko.UtilityDelivery;
using UnityEngine;

public class Game
{
    private const bool DEBUG = false;
    public static List<MonoBehaviour> managers = new();

    // This is pretty bad code, but oh well :D
    public static void AddManager(MonoBehaviour behaviour)
    {
        if (behaviour)
        {
            // Let's not add the same type
            for (int i = 0; i < managers.Count; ++i)
            {
                if (managers[i].GetType() == behaviour.GetType())
                {
                    return;
                }
            }

            managers.AddOnce(behaviour);
        }
    }
}