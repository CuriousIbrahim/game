using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInfoUpdater : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text fuelText;
    public Text altitudeText;
    public Text horizontalSpeedText;
    public Text verticalSpeedText;
    public Text messageText;
    private ImportantState stateTrack;

    public MainCharacter player;

    // Start is called before the first frame update
    void Start()
    {
        stateTrack = GameObject.Find("StateTrack").GetComponent<ImportantState>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeedText.text = "" + (int) player.getHorizontalSpeed();
        verticalSpeedText.text = "" + (int) player.getVerticalSpeed();
        altitudeText.text = "" + (int) player.getAltitude();
        scoreText.text = "" + stateTrack.getScore();
        fuelText.text= "" + stateTrack.getFuel();
        timeText.text = stateTrack.getTime();
    }

    public void SuccessLandingMessage(int points)
    {
        messageText.text = "You landed alive!\n" + points + " points!";
    }

    public void CrashingLandingMessage()
    {
        messageText.text = "You crashed!";
    }
}
