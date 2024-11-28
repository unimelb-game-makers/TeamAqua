using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 0)]
public class Item : ScriptableObject 
{
    public int itemID;
    public string itemName;
    public Sprite sprite;
}

