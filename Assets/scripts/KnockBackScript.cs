using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackScript : PlayerBase
{

    [Header("KnockBack")]
        private bool isKnockingBack = false;
        private PlayerManager playerState;
        private float duration = 1f;
        private float knockBackXForce = -25f; 
        private float knockBackYForce = 15f; 

    public override void EnterState(PlayerManager playerState){
        Debug.Log("KnockBack");
        this.playerState = playerState;
    }

    // Update is called once per frame
    public override void UpdateState(PlayerManager playerState){
    }

    public override void FixedUpdate(PlayerManager playerState){
        knockBack();
    }

    public Vector2 KnockBackDirection(){
    //Declarações
        float knockBackY = knockBackYForce;
        float knockBackX = knockBackXForce;

    //Eixo y
        if(rb.velocity.y < 0){ knockBackY = -knockBackYForce;}
    
    //Eixo x
        if(rb.velocity.x < 0){ knockBackX = -knockBackXForce;}

    //Junção dos vetores
        Vector2 knockBackDirection = new Vector2(knockBackX, knockBackY);
        return knockBackDirection;
    }

    public void knockBack(){
        if (isKnockingBack) return; // Evita chamadas repetidas

        Debug.Log("knockingBack");

        isKnockingBack = true;

        rb.AddForce(KnockBackDirection(), ForceMode2D.Impulse);
        
        playerState.ManagerStartCoroutine(KnockBackCoroutine());
    }

    private IEnumerator KnockBackCoroutine()
    {
        yield return new WaitForSeconds(duration);
        isKnockingBack = false;
        playerState.SwitchState(playerState.flyingState);
    }

    public override void PlayerCollision (Collision2D collision){
    
        if (collision.contactCount == 0) return;

        Vector2 collisionNormal = collision.contacts[0].normal;
        float globalAngle = Mathf.Atan2(collisionNormal.y, collisionNormal.x) * Mathf.Rad2Deg;
    
    }
}
