using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private PlayerCharacterMovement movement;
    private PlayerCharacterStamina stamina;
    private InventoryManager inventory;
    private InteractDetector interactDetector;
    private CameraManager camera;

    public PlayerCharacterMovement Movement => movement;
    public PlayerCharacterStamina Stamina => stamina;
    public InventoryManager Inventory => inventory;
    public InteractDetector InteractDetector => interactDetector;
    public CameraManager Camera => camera;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        movement = GetComponent<PlayerCharacterMovement>();
        stamina = GetComponent<PlayerCharacterStamina>();
        inventory = GetComponent<InventoryManager>();
        interactDetector = GetComponent<InteractDetector>();
        camera = GetComponent<CameraManager>();
    }
}
