using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private float initBatteryLevel = 100;
    [SerializeField] private float batteryDrainRate = 1;

    private PlayerCharacter player;
    private float batteryLevel;

    public bool HasFlashlight => player.Inventory.CheckItem("flashlight01");
    public bool HasBattery => batteryLevel > 0;

    private void Awake()
    {
        batteryLevel = initBatteryLevel;
        player = GetComponent<PlayerCharacter>();
    }

    private void Update()
    {
        UpdateFlashlightRotation();
        UpdateBatteryLevel();
    }

    public void UseFlashlight()
    {
        if (HasFlashlight && flashlight != null)
        {
            if (HasBattery) 
                flashlight.enabled = !flashlight.enabled;
            else 
                flashlight.enabled = false;
        }
    }

    public void UpdateBatteryLevel()
    {
        if (flashlight != null && flashlight.enabled)
        {
            if (HasBattery)
                batteryLevel -= batteryDrainRate * Time.deltaTime;
            else
            {
                batteryLevel = 0;
                flashlight.enabled = false;
            }
        }
    }

    public void RefillBatteryLevel() => batteryLevel = initBatteryLevel;

    private void UpdateFlashlightRotation()
    {
        flashlight.transform.rotation = Camera.main.transform.rotation;
    }
}
