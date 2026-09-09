using UnityEngine;

[System.Serializable]
public class ItemData
{
    public string itemId;
    public string itemName;

    public ItemData (string itemId, string itemName)
    {
        this.itemId = itemId;
        this.itemName = itemName;
    }
}
