using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightColor = Color.white;
    [SerializeField] private Image crosshairImage;

    private void Awake()
    {
        SetHighlight(false);
    }

    public void SetHighlight(bool value)
    {
        if (value == true)
            crosshairImage.color = highlightColor;
        else
            crosshairImage.color = normalColor;
    }
}
