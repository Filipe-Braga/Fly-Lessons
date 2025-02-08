using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingScript : PlayerBase
{
    [Header("Movement Settings")]
    private float horizontalInput, verticalInput;
    private float angleOfAttack, playerAngle;


    //Thrust
    public float incrementThrottle = 0.1f; // Aceleração por uso
    public float Throttle; // Porcentagem Atual de velocidade
    public float maxTrust = 30f; // Limite de velocidade ao subir

    //Criar o Lift
    public float liftPower = 0.0001f; // Ajuste conforme necessário
    //public float maxLift = 1f; // Limite máximo da força Lift

    public float dragForce = 0.001f;

    private Vector3 previousPosition;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f; // Tempo máximo entre os toques

    public override void EnterState(PlayerManager playerState){
        Debug.Log("Flying");
        rb.gravityScale = gravidadeBase;
        Throttle = 0;
    }

    public override void UpdateState(PlayerManager playerState){
        if (IsGrounded() || isDobleClick(KeyCode.Space)) playerState.SwitchState(playerState.walkingState);

        // Captura os inputs apenas uma vez por frame
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    
    public override void FixedUpdate(PlayerManager playerState){
        PlayerAngle();  
        PointController();
        Move();

                // Aplica o limite de velocidade máxima
        if (rb.velocity.magnitude > maxTrust)
        {
            rb.velocity = rb.velocity.normalized * maxTrust;
        }


    }

    private void Move(){
        Thrust();
        Lift();
        Drag();
    }


//Usar como chamar as funções de Lift e Flap e então aplicar a forçe

    private void PlayerAngle()
    {
        if (Mathf.Abs(horizontalInput) > Mathf.Epsilon || Mathf.Abs(verticalInput) > Mathf.Epsilon)
        {
            playerAngle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg; 
        }

        asas.transform.rotation = Quaternion.Euler(0f, 0f, playerAngle - 90f);
        angleOfAttack = Vector2.Angle(rb.velocity, asas.transform.right) - 90f;
    }


//Usar como base para o Flap  - Retornar a força que será somada ao objeto para ele se mover para onde o player quer
    
    private void Thrust (){
        Vector3 direction = new Vector3(Mathf.Cos(playerAngle * Mathf.Deg2Rad), Mathf.Sin(playerAngle * Mathf.Deg2Rad), 0f);
        if(Input.GetButton("Jump")){Throttle += incrementThrottle;}
        else if(Input.GetKey(KeyCode.LeftControl)){Throttle -= incrementThrottle;}

        Throttle = Mathf.Clamp(Throttle, 0f, 100f);

        rb.AddForce(direction * maxTrust * Throttle,  ForceMode2D.Force);
    }
    
// DRAG

    private void Drag(){
        Throttle -= Throttle * (dragForce * rb.velocity.magnitude);
        Throttle = Mathf.Clamp(Throttle, 0f, 100f); // Garante que não fique negativo
    }


//Usar como base para o Lift - Retornar a força de sustentação do objeto
    private void Lift() 
    {
        float horizontalSpeed = Mathf.Abs(rb.velocity.x); // Considera apenas a velocidade horizontal
        float liftForce = horizontalSpeed * horizontalSpeed * liftPower; // Proporcional ao quadrado da velocidade

        rb.AddForce(Vector3.up * liftForce);
    }
 

//Terceiros 

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

    private bool isDobleClick(KeyCode key){
        if (Input.GetKeyDown(key))
        {
            if (Time.time - lastTapTime <= doubleTapThreshold)
            {
                lastTapTime = 0f; // Reseta para evitar múltiplas ativações
                return true;
            }

            lastTapTime = Time.time; // Atualiza o tempo do último toque
        }

        return false;
    }


}
