using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantState : MonoBehaviour
{
    private static bool _created = false;

    private static double STARTING_FUEL = 500;

    private int score;
    private double fuel;
    private int minutes;
    private int seconds;
    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        fuel = STARTING_FUEL;
        minutes = 0;
        seconds = 0;
    }

    void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
            //Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            float time = Time.time;
            minutes = (int)time / 60;
            seconds = (int)time % 60;
        }
    }

    public void Reset()
    {
        score = 0;
        fuel = STARTING_FUEL;
        minutes = 0;
        seconds = 0;
    }

    public void addToScore (int scoreToAdd)
    {
        score += scoreToAdd;

        Debug.Log("score now is " + score);
    }

    public void decrementFuel ()
    {
        fuel -= 0.1;
    }

    public string getTime()
    {
        return minutes + ":" + seconds;
    }

    public int getFuel()
    {
        return (int) fuel;
    }

    public int getScore()
    {
        return score;
    }

    public void setActive(bool fuck)
    {
        isActive = fuck;
    }
}
