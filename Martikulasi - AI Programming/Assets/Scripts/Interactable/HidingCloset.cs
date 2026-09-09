using UnityEngine;

public class HidingCloset : MonoBehaviour, IInteractable
{
    [SerializeField] private string objectName;
    [SerializeField] private Transform hidePosition;
    [SerializeField] private Transform unhidePosition;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Door door;

    private PlayerCharacter player;

    public string Name => objectName;

    public void Interact(PlayerCharacter player)
    {
        throw new System.NotImplementedException();
    }
}
