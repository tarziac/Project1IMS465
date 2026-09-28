using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 0.0f;



    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private bool canJump = true;
    private Rigidbody2D rb;

    public PhysicsMaterial2D bounceMat, normalMat;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");



        if (jumpForce == 0.0f && isGrounded)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(0.0f, rb.linearVelocityY);
        }


        if (Input.GetKey(KeyCode.Space) && isGrounded && canJump)
        {
            jumpForce += 0.1f * Time.deltaTime * 120;
            
        }
        if (jumpForce >= 12f || Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            float tempx = moveInput * moveSpeed;
            float tempy = jumpForce;
            rb.linearVelocity = new Vector2(tempx, tempy);
            canJump = false;
            Invoke("ResetJump", .6f);
        }
        if (isGrounded)
        {
            rb.sharedMaterial = normalMat;
        }
        else
        {
            rb.sharedMaterial = bounceMat;
        }
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
    }
    private void ResetJump()
    {
        canJump = true;
        jumpForce = 0.0f;
    }


       
    }

