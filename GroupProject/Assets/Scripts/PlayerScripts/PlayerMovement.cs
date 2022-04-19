using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // References
    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    
    float horizontal = 0f;
    public bool jump = false;
    bool crouch = false;

    public bool movingUp;

    float currentPositionX;
    float previousPositionX; 

    // Update is called once per frame
    void Update()
    {
        // Input Collection
        // Left/Right Movement
        horizontal = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // jumping
        SetMovingUp();

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);

        }
    }

    private void FixedUpdate()
    {
        // Move Character
        controller.Move(horizontal * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void Onlanding()
    {
        animator.SetBool("IsJumping", false);
    }

    void SetMovingUp()
    {
        
        
    }
}
