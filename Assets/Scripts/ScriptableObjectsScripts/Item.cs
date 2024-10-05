using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 0)]
public class Item : ScriptableObject 
{
    public int itemID;//Probably won't need this
    public string itemName;
    public Sprite sprite;
}

