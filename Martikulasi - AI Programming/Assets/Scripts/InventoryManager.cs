using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    public List<ItemData> Items => items;

    public void AddItems(ItemData item)
    {
        Items.Add(item);
    }

    public bool CheckItem(string id)
    {
        bool isExists = Items.Exists(itemData => string.Equals(itemData.itemId, id));
        return isExists;
    }

    public void RemoveItem(ItemData item)
    {
        Items.Remove(item);
    }
}
