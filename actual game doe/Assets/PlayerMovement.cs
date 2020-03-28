using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    private float timeGuy = 0.3f;
    bool jump = false;
    bool crouch = false;
    bool jumpAttack = false;
    public bool swords = false;

    // Update is called once per frame
    void Update()
    { //test

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        }
        if (Input.GetButtonDown("Swords"))
        {
            swords = !swords;
            animator.SetBool("Swords", swords);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
           // animator.SetBool("IsCrouching", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
        if (swords && animator.GetBool("IsJumping") && Input.GetKey("right") && !jumpAttack) {
            // Initialize jump attack
            jumpAttack = true;
            
        }
}

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
        jumpAttack = false;
        jump = false;
        timeGuy = 0.3f;
    }

   // public void OnCrouching(bool isCrouching)
   // {
   //     animator.SetBool("IsCrouching", isCrouching);
   // }

    void FixedUpdate()
    {
        if (jumpAttack)
        {
            doJumpAttack();
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        //jump = false;
    }

    void doJumpAttack()
    {
        if (timeGuy < 0)
            OnLanding();

        timeGuy -= Time.deltaTime;
        Debug.Log(timeGuy);

        Vector3 position = this.transform.position;
        Vector3 scale = this.transform.localScale;
        double scaleX = scale.x;
        float velo = (float)0.125;
        if (scaleX < 0)
        {
            velo *= -1;
        }
        position.x += velo;
        this.transform.position = position;
        Debug.Log("Bruh");
    }
}
