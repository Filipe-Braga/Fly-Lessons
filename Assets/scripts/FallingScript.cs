using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : PlayerBase
{
    private Vector3 previousPosition;
    private float rotationSpeed = 100f;


    // Start is called before the first frame update
    public override void EnterState(PlayerManager playerState){
        Debug.Log("Falling");
    }

    // Update is called once per frame
    public override void UpdateState(PlayerManager playerState){

    }

    public override void FixedUpdate(PlayerManager playerState){
        TwistPlayer();
    }

    public override void PlayerCollision (Collision2D collision){}

    private void TwistPlayer()
    {   
        float currentAngle = rb.transform.rotation.eulerAngles.z; // Captura o ângulo atual
        float newAngle = currentAngle + rotationSpeed * Time.deltaTime; // Soma um pouco ao ângulo atual
        
        rb.transform.rotation = Quaternion.Euler(0, 0, newAngle); // Aplica a nova rotação
    }



}
