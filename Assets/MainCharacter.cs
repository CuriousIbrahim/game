using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    private Rigidbody rigidBody;
    public static float speed = 500f;
    private static float thrust = 1f;
    private Transform moonTransform;
    private float altitude;
    public float horizontalSpeed = 0.1f;
    public float verticalSpeedNew;
    public bool isThrusting;
    public float verticalSpeed = -0.0000000000001f;
    private float nextSecond;
    private bool isDestroyed = false;

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
        this.altitude = Vector3.Distance(transform.position, moonTransform.position);
    }

    void setVelocity (float horizontalSpeed, float verticalSpeed)
    {
        this.rigidBody.velocity = new Vector3(horizontalSpeed, verticalSpeed, 0);
    }

    void OnCollisionEnter (Collision collision)
    {

        Debug.Log("Something touched me");
        Debug.Log(collision.gameObject.tag);
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Moon")
        {
            isDestroyed = true;
        }
    }
}
