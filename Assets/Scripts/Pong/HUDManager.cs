using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    Text HUDText;
    public int p1Score = 0;
    public int p2Score = 0;
    public GameObject myBall;
    public GameObject panelObject;
    private PanelScript panelScript;
    public GameObject commandObject;
    private CommandScript commandScript;
    private bool cmdOpen = false;
    public InputField commandInput;
    public GameObject skyObject;

    // Use this for initialization
    void Start()
    {
        HUDText = GetComponent<Text>();
        panelScript = PanelScript.Instance();
        panelObject.SetActive(false);
        commandScript = CommandScript.Instance();
        commandObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HUDText.text = p1Score.ToString("G") + " - " + p2Score.ToString("G");

        if (p1Score == 10)
        {
            HUDText.text = p1Score.ToString("G") + " - " + p2Score.ToString("G")
                + "\nPLAYER ONE WINS";
            panelScript.ButtonEvent(ResetGame, ExitGame);
        }
        else if (p2Score == 10)
        {
            HUDText.text = p1Score.ToString("G") + " - " + p2Score.ToString("G")
                + "\nPLAYER TWO WINS";
            panelScript.ButtonEvent(ResetGame, ExitGame);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if(cmdOpen)
                commandScript.ClosePanel();
            else
                commandScript.ButtonEvent(EnterCommand);
        }
    }

    public void Score(string wallID)
    {
        if (wallID == "RightWall")
        {
            p1Score++;
            myBall.SendMessage("ResetBall", 0.5f, SendMessageOptions.RequireReceiver);
        }
        else
        {
            p2Score++;
            myBall.SendMessage("ResetBall", 0.5f, SendMessageOptions.RequireReceiver);
        }
    }

    public void ResetGame()
    {
        p1Score = 0;
        p2Score = 0;
        myBall.SendMessage("ResetBall", 0.5f, SendMessageOptions.RequireReceiver);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void EnterCommand()
    {
        if (commandInput.text == "Nighttime")
            DarkenSky();
        else if (commandInput.text == "Gottagofast")
            SpeedUpBall();
        //else tell user they input an incorrect command
    }

    public void DarkenSky()
    {
        skyObject.GetComponent<Renderer>().material.color = new Color(0, 0, 1);
    }

    public void SpeedUpBall()
    {
        myBall.SendMessage("SpeedUp", 0.5f, SendMessageOptions.RequireReceiver);
    }
}
