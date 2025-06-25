using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "ScriptableObjects/Day Database", fileName = "Day Database")]
public class DayDatabase : ScriptableObject
{
    public List<Day> days;
}
