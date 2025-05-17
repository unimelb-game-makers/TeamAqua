using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 0)]
public class Item : ScriptableObject
{
    public string displayName;

    public Sprite sprite;
}
