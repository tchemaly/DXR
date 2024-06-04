using System;
using TMPro;
using UnityEngine;

public class TMPTextHandler : MonoBehaviour, OVRVirtualKeyboard.ITextHandler
{
    [SerializeField] private TMP_Text textDisplay;

    public Action<string> OnTextChanged { get; set; }
    public string Text => textDisplay.text;
    public bool SubmitOnEnter { get { return true; } }  // or false depending on your needs
    public bool IsFocused { get { return true; } }  // Assuming always focused when active

    public void AppendText(string text)
    {
        textDisplay.text += text;
        OnTextChanged?.Invoke(textDisplay.text);
    }

    public void ApplyBackspace()
    {
        if (!string.IsNullOrEmpty(textDisplay.text))
        {
            textDisplay.text = textDisplay.text.Substring(0, textDisplay.text.Length - 1);
            OnTextChanged?.Invoke(textDisplay.text);
        }
    }

    public void Submit()
    {
        // Handle the submit action, perhaps for executing a command or sending a message
        Debug.Log("Submit: " + textDisplay.text);
    }

    public void MoveTextEnd()
    {
        // Not needed for TMP_Text but required by interface
    }
}
