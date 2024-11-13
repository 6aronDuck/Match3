using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectXformMover))]
public class MessageWindow : MonoBehaviour
{
    public Image messageIcon;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI buttonText;

    public void ShowMessage(Sprite icon = null, string message = "", string buttonMessage = "start")
    {
        if (messageIcon != null)
            messageIcon.sprite = icon;
        if (messageText != null)
            messageText.text = message;
        if (buttonText != null)
            buttonText.text = buttonMessage;
    }
}