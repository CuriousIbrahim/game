using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    private Rigidbody rigidBody;
    public float speed = 500f;
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
    private bool isStop = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        moonTransform = GameObject.Find("MOON").transform;
        nextSecond = Time.time + 1;
        verticalSpeedNew = 0;
        isThrusting = false;
        stateTrack = GameObject.Find("StateTrack").GetComponent<ImportantState>();

        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);

        if (Physics.Raycast(downRay, out hit))
        {
            this.altitude = hit.distance;
        }

        setVelocity(0.3f, 0f);

        stateTrack.setActive(true);
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isStop = isTouchingMoon | isStop;

        if (isDestroyed && Time.time > nextTimeUpdateIfCrash)
        {
            if (stateTrack.getFuel() <= 0)
            {
                Application.LoadLevel("MainMenu");
                //stateTrack.setActive(false);
                stateTrack.Reset();
            } else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
                
        }

        horizontalSpeed = rigidBody.velocity.x * 100;
        verticalSpeed = rigidBody.velocity.y * 100;


        updateAltitude();
        updateIsUpright();

        if (Input.GetKey(KeyCode.RightArrow) && !isStop)
        {
            rigidBody.AddTorque(new Vector3(speed, 0, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !isStop)
        {
            rigidBody.AddTorque(new Vector3(-speed, 0, 0) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow) && stateTrack.getFuel() > 0 && !isStop)
        {
            Debug.Log(rigidBody);
            stateTrack.decrementFuel();
            rigidBody.AddRelativeForce(new Vector3(0, 1, 0) * thrust);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && !isStop)
        {
            isThrusting = true;
        } else if (Input.GetKeyUp(KeyCode.UpArrow) && !isStop)
        {
            isThrusting = false;
        }
    }

    void updateIsUpright()
    {

        float track = Vector2.Dot(transform.up, Vector3.up);
        isUpright = track > 0.95f;
    }

    void updateAltitude()
    {
        //this.altitude = Vector3.Distance(transform.position, moonTransform.position);

        Transform sphereTransform = GameObject.Find("Sphere").transform;

       Debug.DrawRay(sphereTransform.position, -Vector3.up * 50);

       RaycastHit hit;
       Ray downRay = new Ray(sphereTransform.position, -Vector3.up);

        int layerMask = 1 << 6;

       if (Physics.Raycast(downRay, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.Log(hit.distance);
            Debug.Log(hit.transform.gameObject.name);
            this.altitude = Vector3.Distance(sphereTransform.position, hit.point);

        }

        //this.altitude = Vector3.Distance(transform.position, moonTransform.position);
    }

    public bool CloseToGround ()
    {
        return this.altitude <= 2f;
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

            if (Mathf.Abs(verticalSpeedAtCollision) > MAXIMUM_SPEED_FOR_WORST_LANDING || !isUpright)
            {
                GetComponents<AudioSource>()[0].Play();
                isDestroyed = true;
                uIInfoUpdater.CrashingLandingMessage();

                GameObject[] shipParts = GameObject.FindGameObjectsWithTag("RocketShipPart");

                foreach (GameObject shipPart in shipParts)
                {
                    Rigidbody gameObjectsRigidBody = shipPart.AddComponent<Rigidbody>();
                    gameObjectsRigidBody.mass = 10;
                    gameObjectsRigidBody.useGravity = true;

                }

                nextTimeUpdateIfCrash = Time.time + 3f;
            } else
            {
                GetComponents<AudioSource>()[1].Play();
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
        return this.altitude * 50;
    }

    public float getVerticalSpeedAtCollision()
    {
        return verticalSpeedAtCollision;
    }
}
