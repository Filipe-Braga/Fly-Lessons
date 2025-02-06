using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject asas;


    [Header("States")]
    public PlayerBase currentState;
    public WalkingScript walkingState = new WalkingScript();
    public FlyingScript flyingState = new FlyingScript();

    [Header("UGUI")]
    [SerializeField] TextMeshProUGUI HUD;



    void Start()
    {
        currentState = walkingState;
        
        currentState.InitializeDefaults(rb, boxCollider, groundLayer, asas);
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate(){
        currentState.FixedUpdate(this);
        HUDUpdate();
    }

    public void SwitchState( PlayerBase state )
    {
        currentState = state;
        currentState.InitializeDefaults(rb, boxCollider, groundLayer, asas);
        currentState.EnterState(this);
    }

    public void HUDUpdate(){
        HUD.text = "Velocidade: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h \n";
        HUD.text += "Altitude: " + transform.position.y.ToString("F0") + "m \n";

    }

}
