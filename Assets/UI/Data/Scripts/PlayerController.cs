using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10;

    public float jumpForce = 25;

    public float dashForce = 50;
    public float dashTimer = 0.25f;
    float currentDashTimer;
    float dashDirection;
    bool isDashing;
    bool dashCounter;

    public float knockBackForce = 20;
    public float knockBackTime = 0.25f;
    private float knockBackCounter;

    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale = 4;
    //private int jumpCount;
   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            moveDirection = new Vector3(moveDirection.x, moveDirection.y, 0f);
            moveDirection.x = Input.GetAxis("Horizontal") * moveSpeed;
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            if (controller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        if (controller.isGrounded)
        {
            dashCounter = true;
            if (Input.GetKeyDown(KeyCode.Z))
            {
                isDashing = false;
                dashCounter = false;
                currentDashTimer = dashTimer;
            }
        }
        else
        { 
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isDashing = true;
            currentDashTimer = dashTimer;
        }
        }
        if (isDashing && dashCounter)
        {
            currentDashTimer -= Time.deltaTime;
            if (Input.GetAxis("Horizontal") > 0)
            {
                moveDirection.x = dashForce;
            }
            if (Input.GetAxis("Horizontal") < 0)
            {
                moveDirection.x -= dashForce;
            }
            if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") == 0)
            {
                moveDirection.y = dashForce / 2f;
            }
            if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") > 0)
            {
                moveDirection.y = dashForce * 0.5f;
                moveDirection.x = dashForce * 0.8f;
            }
            if (Input.GetAxis("Vertical") > 0 && Input.GetAxis("Horizontal") < 0)
            {
                moveDirection.y = dashForce * 0.5f;
                moveDirection.x -= dashForce * 0.2f;
            }
            if (currentDashTimer <= 0)
            {
                isDashing = false;
                dashCounter = false;
            }
            
        }

        controller.Move(moveDirection * Time.deltaTime);


    }
    public void knockBack(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        //moveDirection.x = knockBackForce;
        moveDirection.y = knockBackForce * 1.15f;
    }
}
