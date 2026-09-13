using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private StaminaUI staminaUI;
    [SerializeField] private BatteryLevelUI batteryLevelUI;
    [SerializeField] private InteractionInfoUI interactionInfoUI;
    [SerializeField] private CrosshairUI crosshairUI;

    public static HUDManager Instance => instance;
    public BatteryLevelUI BatteryLevelUI => batteryLevelUI;
    public InteractionInfoUI InteractionInfoUI => interactionInfoUI;
    public CrosshairUI CrosshairUI => crosshairUI;

    private static HUDManager instance;

    public StaminaUI StaminaUI => staminaUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
