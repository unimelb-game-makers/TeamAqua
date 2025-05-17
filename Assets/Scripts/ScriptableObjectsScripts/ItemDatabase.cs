using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Item Database", fileName = "Item Database")]
public class ItemDatabase : ScriptableObject
{
    [TableList]
    public Item[] items;

    public bool TryGetItem(string id, out Item item)
    {
        foreach (Item i in items)
        {
            if (i.name == id)
            {
                item = i;
                return true;
            }
        }

        item = null;
        return false;
    }
}