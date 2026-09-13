using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionInfoUI : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;
    [SerializeField] private TMP_Text nameText;

    public void SetVisible(bool value)
    {
        uiObject?.SetActive(value);
    }

    public void SetNameText(string text)
    {
        nameText.text = text;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(uiObject.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
    }
}
