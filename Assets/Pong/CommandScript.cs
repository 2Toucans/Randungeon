using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CommandScript : MonoBehaviour
{
    public InputField commandInput;
    public Button enterButton;
    public GameObject commandObject;

    private static CommandScript commandScript;

    public static CommandScript Instance()
    {
        if (!commandScript)
        {
            commandScript = FindObjectOfType(typeof(CommandScript)) as CommandScript;
        }
        return commandScript;
    }

    public void ButtonEvent(UnityAction enterEvent)
    {
        commandObject.SetActive(true);
        enterButton.onClick.RemoveAllListeners();
        enterButton.onClick.AddListener(enterEvent);
        enterButton.onClick.AddListener(ClosePanel);

        enterButton.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        commandObject.SetActive(false);
    }
}