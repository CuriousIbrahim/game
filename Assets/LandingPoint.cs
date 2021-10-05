using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPoint : MonoBehaviour
{
    public float distanceFromPlayer;
    private MainCharacter playerS;
    private GameObject playerGO;
    public float maximumDistanceFromPlayerToBeConsideredForScore;
    private bool ranOnce = false;
    private ImportantState stateTrack;
    public int multiplier;
    private UIInfoUpdater uIInfoUpdater;
    private float waitTill;


    // Start is called before the first f+rame update
    void Start()
    {
        playerGO = GameObject.Find("AircraftWhole");
        playerS = playerGO.GetComponent<MainCharacter>();
        uIInfoUpdater = GameObject.Find("UI-Updater").GetComponent<UIInfoUpdater>();
        stateTrack = GameObject.Find("StateTrack").GetComponent<ImportantState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ranOnce && Time.time > waitTill)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        distanceFromPlayer = Vector3.Distance(playerGO.transform.position, transform.position);

        if (distanceFromPlayer <= maximumDistanceFromPlayerToBeConsideredForScore && playerS.LandedSafely() && !ranOnce)
        {

            float speed = playerS.getVerticalSpeedAtCollision();
            Debug.Log("Nice " + speed);

            int score = 0;

            if (speed <= 20f)
            {
                score = 10;
            }
            else if (speed <= 35f)
            {
                score = 5;
            }
            else
            {
                score = 2;
            }

            Debug.Log(score);

            score *= multiplier;

            Debug.Log(score);

            stateTrack.addToScore(score);

            ranOnce = true;

            uIInfoUpdater.SuccessLandingMessage(score);

            waitTill = Time.time + 3;

        }
    }
}
