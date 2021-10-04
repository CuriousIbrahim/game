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

    public MainCharacter player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeedText.text = "" + player.getHorizontalSpeed();
        verticalSpeedText.text = "" + player.getVerticalSpeed();
        altitudeText.text = "" + player.getAltitude();
        scoreText.text = "" + player.getScore();
    }
}
