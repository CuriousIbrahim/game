using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingPoint : MonoBehaviour
{
    public float distanceFromPlayer;
    public MainCharacter playerS;
    public GameObject playerGO;
    public float maximumDistanceFromPlayerToBeConsideredForScore;
    private bool ranOnce = false;
    int multiplier;

    // Start is called before the first f+rame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(playerGO.transform.position, transform.position);

        if (distanceFromPlayer <= maximumDistanceFromPlayerToBeConsideredForScore && playerS.LandedSafely() && !ranOnce)
        {
            Debug.Log("hahah");
            ranOnce = true;
        }
    }
}
