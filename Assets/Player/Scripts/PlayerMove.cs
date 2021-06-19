using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMove : MonoBehaviour
{
   [SerializeField] LayerChecker rightFootChecker;
   [SerializeField] LayerChecker leftFootChecker;
   public float runSpeed = 2;
   public bool isLookingRight;
   public float runSpeedOnAir = 1;
   public float jumpSpeed = 3;
   public bool isGrounded;
   public float dashForce = 3;
   public Animator animator;
   public SpriteRenderer SpriteRenderer;
   public float startDashTimer;
   float currentDashTimer;
   bool isDashing = false;
   bool isAttacking = false;
   Rigidbody2D rb2D;
   public bool betterJump = false;
   public float fallMultiplier = 0.5f;
   public float lowJumpMultiplier = 1f;

   private void CheckGround() {
        isGrounded = rightFootChecker.isTouching || leftFootChecker.isTouching;
    }

   private IEnumerator RecoverFromAttack() {
       yield return new WaitForSeconds(0.05f);
   }

    void Start() {
        rb2D = GetComponent<Rigidbody2D>();
        isLookingRight = true;
    }

    void FixedUpdate() {
        CheckGround();
        
        if (Input.GetKey("d") && isGrounded)
        {
            SpriteRenderer.flipX = false;
            animator.SetBool("isWalking",true);
            rb2D.velocity = new Vector2(runSpeed,rb2D.velocity.y);
            isLookingRight = true;

        }

        else if (Input.GetKey("d") && !isGrounded)
        {
            SpriteRenderer.flipX = false;
            rb2D.velocity = new Vector2(runSpeedOnAir,rb2D.velocity.y);
            isLookingRight = true;
        }

        else if(Input.GetKey("a")&& isGrounded)
        {
          SpriteRenderer.flipX = true;
          animator.SetBool("isWalking",true);
          rb2D.velocity = new Vector2(-runSpeed,rb2D.velocity.y);
          isLookingRight = false;

        }

        else if (Input.GetKey("a")&& !isGrounded)
        {
            SpriteRenderer.flipX = true;
            rb2D.velocity = new Vector2(-runSpeedOnAir,rb2D.velocity.y);
            isLookingRight = false;

        }

        else
        {
            animator.SetBool("isWalking",false);
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }




        if (Input.GetKey("space") && isGrounded)
          {
              rb2D.velocity = new Vector2(rb2D.velocity.x,jumpSpeed);

          }

          if(isGrounded){
              animator.SetBool("isJumping",false);
          }if(!isGrounded){
              animator.SetBool("isJumping",true);
          }


        if(betterJump)
        {
            if (rb2D.velocity.y < 0)
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }

            if  (rb2D.velocity.y > 0 && !Input.GetKey("space"))
            {
                rb2D.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }






        if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey("d") || Input.GetKey("a"))
        {
            animator.SetBool("isDashing",true);
            isDashing = true;
            currentDashTimer = startDashTimer;
            rb2D.velocity = Vector2.zero;

        }

       if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isDashing",false);
            isDashing = false;
        }

        if(isDashing)
        {
            currentDashTimer -= Time.deltaTime;
            animator.SetBool("isWalking",false);

            if (isLookingRight)
            {
                rb2D.velocity = new Vector2(dashForce,rb2D.velocity.y);
            }

            else if (!isLookingRight)
            {
               rb2D.velocity = new Vector2(-dashForce,rb2D.velocity.y);
            }

         }

        if (currentDashTimer <= 0)
        {
            isDashing = false;
        }


        if (Input.GetKeyDown("k"))
        {
            animator.SetTrigger("isAttacking");
            isAttacking = true;
        }
        else if (Input.GetKeyUp("k"))
        {
            animator.SetBool("isAttacking",false);
            isAttacking = false;
        }



    }
}
