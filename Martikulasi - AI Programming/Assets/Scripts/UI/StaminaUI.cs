using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    [SerializeField] private Image staminaFill;

    public void SetVisible(bool value)
    {
        uiObject?.SetActive(value);
    }

    public void SetStaminaFill(float value, float maxValue)
    {
        if (staminaFill != null)
            staminaFill.fillAmount = value / maxValue; 
    }
}
