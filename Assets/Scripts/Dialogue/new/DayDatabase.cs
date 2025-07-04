using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Day Database", fileName = "Day Database")]
public class DayDatabase : ScriptableObject
{
    public List<Day> days;

    public Day GetDay(int day)
    {
        if (day < 0 || day >= days.Count)
        {
            throw new Exception($"Day {day} is not supported!");
        }
        return days[day];
    }
}
