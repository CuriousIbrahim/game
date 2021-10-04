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


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        moonTransform = GameObject.Find("MOON").transform;
        setVelocity(horizontalSpeed, verticalSpeed);
        nextSecond = Time.time + 1;
        verticalSpeedNew = 0;
        isThrusting = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalSpeed = rigidBody.velocity.x * 100;
        verticalSpeed = rigidBody.velocity.y * 100;


        updateAltitude();

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidBody.AddTorque(new Vector3(0, 0, -speed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidBody.AddTorque(new Vector3(0, 0, speed) * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space))
        {

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
            isTouchingMoon = true;
        }
    }

    public bool LandedSafely ()
    {
        return isTouchingMoon;
    }
}
