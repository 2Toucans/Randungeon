using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PanelScript : MonoBehaviour
{

    public Button yesButton;
    public Button noButton;
    public GameObject panelObject;

    private static PanelScript panelScript;

    public static PanelScript Instance()
    {
        if(!panelScript)
        {
            panelScript = FindObjectOfType(typeof(PanelScript)) as PanelScript;
        }
        return panelScript;
    }

    public void ButtonEvent(UnityAction yesEvent, UnityAction noEvent)
    {
        panelObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);
        yesButton.gameObject.SetActive(true);

        noButton.onClick.RemoveAllListeners();
        noButton.onClick.AddListener(noEvent);
        noButton.onClick.AddListener(ClosePanel);
        noButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        panelObject.SetActive(false);
    }
}
