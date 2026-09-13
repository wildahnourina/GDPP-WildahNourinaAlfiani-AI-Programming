using UnityEngine;
using UnityEngine.Events;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] private InputManager input;

    private PlayerCharacterMovement movement;
    private PlayerCharacterStamina stamina;
    private InventoryManager inventory;
    private InteractDetector interactDetector;
    private CameraManager cameraManager;
    private Flashlight flashlight;

    public UnityEvent OnDeath;

    public InputManager  Input => input;
    public PlayerCharacterMovement Movement => movement;
    public PlayerCharacterStamina Stamina => stamina;
    public InventoryManager Inventory => inventory;
    public InteractDetector InteractDetector => interactDetector;
    public CameraManager Camera => cameraManager;
    public Flashlight Flashlight => flashlight;

    public bool IsHiding { get; private set; }

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        movement = GetComponent<PlayerCharacterMovement>();
        stamina = GetComponent<PlayerCharacterStamina>();
        inventory = GetComponent<InventoryManager>();
        interactDetector = GetComponent<InteractDetector>();
        cameraManager = GetComponent<CameraManager>();
        flashlight = GetComponent<Flashlight>();
    }

    public void SetIsHiding(bool isHiding) => IsHiding = isHiding;
}
