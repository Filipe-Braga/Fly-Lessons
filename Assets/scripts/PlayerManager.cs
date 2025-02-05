using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject asas;

    [SerializeField] private AnimationCurve aoaCurve;

    [Header("States")]
    public PlayerBase currentState;
    public WalkingScript walkingState = new WalkingScript();
    public FlyingScript flyingState = new FlyingScript();


    void Start()
    {
        currentState = walkingState;
        
        currentState.InitializeDefaults(rb, boxCollider, groundLayer, asas, aoaCurve);
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState( PlayerBase state )
    {
        currentState = state;
        currentState.InitializeDefaults(rb, boxCollider, groundLayer, asas, aoaCurve);
        currentState.EnterState(this);
    }
}
