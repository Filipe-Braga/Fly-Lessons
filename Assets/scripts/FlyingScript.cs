using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingScript : PlayerBase
{
    [Header("Movement Settings")]
    private float horizontalInput, verticalInput;
    private float angleOfAttack, playerAngle, liftForce;

    //Thrust
    public float Throttle = 2f; // Força extra ao subir
    public float maxTrust = 2f; // Limite de velocidade ao subir

    //Criar o Lift
    public float liftPower = 8f; // Ajuste conforme necessário
    //public float maxLift = 1f; // Limite máximo da força Lift

    private Vector3 previousPosition, thrust;

    public override void EnterState(PlayerManager playerState){
        Debug.Log("Flying");
        rb.gravityScale = gravidadeBase;
    }

    public override void UpdateState(PlayerManager playerState){
        if (IsGrounded()) playerState.SwitchState(playerState.walkingState);

        // Captura os inputs apenas uma vez por frame
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        angleOfAttack = Vector2.Angle(rb.velocity, asas.transform.right) - 90f;
        Debug.Log(angleOfAttack);

        PlayerAngle();  
        Thrust();
        PointController();
        //LiftUpdate();


    }


//Usar como chamar as funções de Lift e Flap e então aplicar a forçe

    private void PlayerAngle()
    {
        if (Mathf.Abs(horizontalInput) > Mathf.Epsilon || Mathf.Abs(verticalInput) > Mathf.Epsilon)
        {
            playerAngle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg; 
        }

        asas.transform.rotation = Quaternion.Euler(0f, 0f, playerAngle - 90f);
    }


//Usar como base para o Flap  - Retornar a força que será somada ao objeto para ele se mover para onde o player quer
    
    private void Thrust (){
        if(Input.GetButton("Jump")){
            Vector3 direction = new Vector3(Mathf.Cos(playerAngle * Mathf.Deg2Rad), Mathf.Sin(playerAngle * Mathf.Deg2Rad), 0f);
        
            thrust = Throttle * maxTrust * direction;
            rb.AddForce(thrust, ForceMode2D.Force);
        }
    }
    
// DRAG

    private void Drag(){

    }


//Usar como base para o Lift - Retornar a força de sustentação do objeto

    private void LiftUpdate(){
        rb.AddForce(Lift());


    }
    private Vector3 Lift() //Lift = v^2 * coeficiente * liftPower
    {   //É UMA BOA OPÇÂO, MAS TALVEZ NÃO FUNCIONE POR CAUSA DOS ANGULOS LIMITADOS NO PC. VOU GRARDAR PARA MAIS TARDE
        // var liftVelocity2 = rb.velocity.sqrMagnitude; //Calculo do V2
        
        // var liftCoefficient = aoaCurve.Evaluate(angleOfAttack); //Calculo do Coeficiente f(aoa)

        // var liftForce = liftVelocity2 * liftCoefficient * liftPower;

        // var liftDirection = new Vector2(-rb.velocity.normalized.y, rb.velocity.normalized.x);
        // var lift = liftDirection * liftForce;

        // var dragForce = liftCoefficient * liftCoefficient * 0.5;
        // var dragDirection = -rb.velocity.normalized;
        // var innducedDrag = (float)(dragForce * liftVelocity2) * dragDirection;


        // return lift + innducedDrag;
    }
 
    private void PointController()
{   
    Vector3 moveDirection = rb.transform.position - previousPosition;

    if (moveDirection.sqrMagnitude > 0.0001f) // Evita erros quando parado
    {
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        rb.transform.rotation = Quaternion.Euler(0, 0, angle -90);

    }

    previousPosition = rb.transform.position; // Atualiza a posição anterior
}


//Terceiros 
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
