using UnityEngine;
using UnityEngine.UI;

public class BatteryLevelUI : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    [SerializeField] private Color highColor = Color.white;
    [SerializeField] private Color mediumColor = Color.white;
    [SerializeField] private Color lowColor = Color.white;
    [SerializeField] private Image batteryFill;

    public void SetVisibility(bool value)
    {
        uiObject?.SetActive(value);
    }

    public void UpdateBatteryUI(float value, float maxValue)
    {
        float fillAmount = value / maxValue;
        batteryFill.fillAmount = fillAmount;
        Color color = highColor;

        if (fillAmount < 0.25f)
            color = lowColor;
        else if (fillAmount > 0.25f && fillAmount < 0.5f)
            color = mediumColor;

        batteryFill.color = color;
    }
}
