using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string doorName;
    [SerializeField] protected Transform doorTransform;
    [SerializeField] protected float duration = 1f;
    [SerializeField] protected bool isLocked;
    [SerializeField] protected string keyId;

    protected Coroutine animatingDoorCo;
    protected bool isAnimating;
    protected bool isOpen;

    public bool IsAnimating => isAnimating;
    public string Name => doorName;

    public UnityEvent OnDoorOpen;
    public UnityEvent OnDoorClose;
    public UnityEvent OnOpenLockedDoor;

    public virtual void Open()
    {
        isOpen = true;
        OnDoorOpen?.Invoke();
    }

    public virtual void Close()
    {
        isOpen = false;
        OnDoorClose?.Invoke();
    }

    [ContextMenu("Interact Door")]
    public void Interact(PlayerCharacter player)
    {
        if (isLocked)
        {
            bool hasKey = player.Inventory.CheckItem(keyId);
            if (hasKey)
            {
                isLocked = false;
                Open();
            }
            else
            {
                OnOpenLockedDoor?.Invoke();
            }
        }
        else
        {
            if (isOpen) Close();
            else Open();
        }
    }
}
