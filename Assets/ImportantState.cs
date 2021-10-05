using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImportantState : MonoBehaviour
{
    private static bool _created = false;

    private int score;
    private double fuel;
    private int minutes;
    private int seconds;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        fuel = 1000f;
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
        float time = Time.time;
        minutes = (int) time / 60;
        seconds = (int) time % 60; 
    }

    public void addToScore (int scoreToAdd)
    {
        score += scoreToAdd;

        Debug.Log("score now is " + score);
    }

    public void decrementFuel ()
    {
        fuel -= 0.01;
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
}
