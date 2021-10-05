using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed;
    public float JumpForce;

    public float DashForce;
    public float StartDashTimer;

    float CurrentDashTimer;
    float DashDirection;

    bool isGrounded = false;
    bool isDashing;

    Rigidbody rb;
    float movX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movX = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(movX * Speed, rb.velocity.y,0f);
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isGrounded)
            {
                jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && !isGrounded && movX != 0)
        {
            isDashing = true;
            CurrentDashTimer = StartDashTimer;
            rb.velocity = Vector3.zero;
            DashDirection = (int)movX;
        }

        if (isDashing)
        {
            rb.velocity = transform.right * DashDirection * DashForce;
            CurrentDashTimer -= Time.deltaTime;

            if (CurrentDashTimer <= 0)
            {
                isDashing = false;
            }

        }
    }

    void jump()
    {
        rb.AddForce(transform.up * JumpForce);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isGrounded = true;
        }
    }
}
