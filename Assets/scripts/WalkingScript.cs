using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingScript : PlayerBase
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Mana Settings")]
    public float manaRecovery = 20f;


    private float horizontalInput;

    private bool fastFallEnabled = true;



    public override void EnterState(PlayerManager playerState)
    {
        Debug.Log("Walking");
        rb.velocity = Vector2.zero;
        rb.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public override void UpdateState(PlayerManager playerState)
    {
        HandleInput();
        HandleJump(playerState);
    }

    public override void FixedUpdate(PlayerManager playerState){
        
        UpdateVerticalState();

        Move();
    }

    private float HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        return horizontalInput;
    }

    private void Move()
    {
        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;

        if (horizontalInput != 0)
        {
            //FlipCharacter();
        }
    }

    private void HandleJump(PlayerManager playerState)
    {
        if (Input.GetButtonDown("Jump") )//Acho que vc pode apagar tudo depois de &&
        {
            if (verticalState != 0)
            { 
                playerState.SwitchState(playerState.flyingState); 
            }
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (IsGrounded())
        {
            ManaManager.RecoverMana(manaRecovery);
        }
    }

    private void UpdateVerticalState()
    {
        if (rb.velocity.y == 0)
        {
            verticalState = 0;
            if (!fastFallEnabled)
            {
                rb.gravityScale = gravidadeBase;
                fastFallEnabled = true;
            }
        }
        else if (rb.velocity.y > 0)
        {
            verticalState = 1;
            if (!fastFallEnabled)
            {
                rb.gravityScale = gravidadeBase;
                fastFallEnabled = true;
            }
        }
        else if (rb.velocity.y < 0)
        {
            verticalState = -1;
            if (fastFallEnabled)
            {
                rb.gravityScale = gravidadeBase*2;
                fastFallEnabled = false;
            }
        }
    }

    private void FlipCharacter()
    {
        if ((horizontalInput > 0 && !isFlipped) || (horizontalInput < 0 && isFlipped))
        {
            isFlipped = !isFlipped;
            Vector3 scale = rb.transform.localScale;
            scale.y *= -1;
            rb.transform.localScale = scale;
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
        );
        return hit.collider != null;
    }

}
