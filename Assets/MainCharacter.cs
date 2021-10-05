using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    private Rigidbody rigidBody;
    public static float speed = 500f;
    public float thrust = 2f;
    private Transform moonTransform;
    public float altitude;
    public float horizontalSpeed = 0.1f;
    public float verticalSpeedNew;
    public bool isThrusting;
    public float verticalSpeed = -0.0000000000001f;
    private float nextSecond;
    public bool isTouchingMoon = false;
    public static float MAXIMUM_SPEED_FOR_BEST_LANDING = 20f;
    public static float MAXIMUM_SPEED_FOR_OK_LANDING = 35f;
    public static float MAXIMUM_SPEED_FOR_WORST_LANDING = 50f;
    public bool isDestroyed = false;
    public bool isUpright = false;
    private ImportantState stateTrack;
    public static float thresholdForBeingUpright = 0.6f;
    public float verticalSpeedAtCollision;
    public float nextTimeUpdateIfCrash;
    public UIInfoUpdater uIInfoUpdater;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        moonTransform = GameObject.Find("MOON").transform;
        nextSecond = Time.time + 1;
        verticalSpeedNew = 0;
        isThrusting = false;
        stateTrack = GameObject.Find("StateTrack").GetComponent<ImportantState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestroyed && Time.time > nextTimeUpdateIfCrash)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        horizontalSpeed = rigidBody.velocity.x * 100;
        verticalSpeed = rigidBody.velocity.y * 100;


        updateAltitude();
        updateIsUpright();

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.AddTorque(new Vector3(0, 0, -speed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.AddTorque(new Vector3(0, 0, speed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && stateTrack.getFuel() > 0)
        {
            stateTrack.decrementFuel();
            rigidBody.AddRelativeForce(new Vector3(0, 0, 1) * thrust);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isThrusting = true;
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            isThrusting = false;
        }
    }

    void updateIsUpright()
    {

        float track = Vector2.Dot(transform.forward, Vector3.up);
        isUpright = track > 0.98f;
    }

    void updateAltitude()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(downRay, out hit))
        {
            this.altitude = hit.distance;
        }

        //this.altitude = Vector3.Distance(transform.position, moonTransform.position);
    }

    public bool CloseToGround ()
    {
        return this.altitude <= 3f;
    }

    void setVelocity(float horizontalSpeed, float verticalSpeed)
    { 
        this.rigidBody.velocity = new Vector3(horizontalSpeed, verticalSpeed, 0);
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Moon")
        {
            verticalSpeedAtCollision = verticalSpeed;
            isTouchingMoon = true;

            Debug.Log("ha " + verticalSpeedAtCollision + " " + (verticalSpeedAtCollision > MAXIMUM_SPEED_FOR_WORST_LANDING) + " "  + !isUpright);

            if (Mathf.Abs(verticalSpeedAtCollision) > MAXIMUM_SPEED_FOR_WORST_LANDING && !isUpright)
            {
                Debug.Log("ouch");
                isDestroyed = true;
                uIInfoUpdater.CrashingLandingMessage();
                nextTimeUpdateIfCrash = Time.time + 3f;
            }
        }
    }

    public bool LandedSafely ()
    {
        return isTouchingMoon && !isDestroyed && isUpright;
    }
    
    public bool IsDestroyed ()
    {
        return isDestroyed;
    }

    public float getVerticalSpeed()
    {
        return this.verticalSpeed;
    }

    public float getHorizontalSpeed()
    {
        return this.horizontalSpeed;
    }

    public float getAltitude()
    {
        return this.altitude * 10;
    }

    public float getVerticalSpeedAtCollision()
    {
        return verticalSpeedAtCollision;
    }
}
