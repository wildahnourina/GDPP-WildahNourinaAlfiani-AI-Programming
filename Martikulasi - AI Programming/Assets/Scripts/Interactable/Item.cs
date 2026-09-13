using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour, IInteractable, IPickable
{
    [SerializeField] private ItemData itemData;

    public UnityEvent OnItemPicked;
    public string Name => itemData.itemName;

    [ContextMenu("Interact Item")]
    public void Interact(PlayerCharacter player)
    {
        Pickup(player);
    }

    public virtual void Pickup(PlayerCharacter player)
    {
        ItemData newItem = new ItemData(itemData.itemId, itemData.itemName);

        player.Inventory.AddItems(newItem);
        OnItemPicked?.Invoke();
        Destroy(gameObject);
    }
}
