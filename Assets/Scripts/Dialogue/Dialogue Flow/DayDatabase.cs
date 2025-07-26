using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using Sirenix.OdinInspector;
>>>>>>> main
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Day Database", fileName = "Day Database")]
public class DayDatabase : ScriptableObject
{
<<<<<<< HEAD
    public List<Day> days;

=======
    [InlineEditor]
    public List<Day> days;

    public void Init()
    {
        foreach (Day day in days)
        {
            day.Init();
        }
    }

>>>>>>> main
    public Day GetDay(int day)
    {
        if (day < 0 || day >= days.Count)
        {
            throw new Exception($"Day {day} is not supported!");
        }
        return days[day];
    }
}
