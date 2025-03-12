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
    [SerializeField] private ManaManager ManaManager;

    [SerializeField] public float globalAngle;
    [SerializeField] public GameObject collisionObject;
    [SerializeField] public Vector2 collisionNormal;



    [Header("States")]
    public PlayerBase currentState;
    public WalkingScript walkingState = new WalkingScript();
    public FlyingScript flyingState = new FlyingScript();
    public FallingScript fallingState = new FallingScript();
    public KnockBackScript knockBackState = new KnockBackScript();
    public FrozenScript frozenState = new FrozenScript();





    void Start()
    {
        currentState = walkingState;
        
        currentState.InitializeDefaults(rb, boxCollider, groundLayer, asas, ManaManager);
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void FixedUpdate(){
        currentState.FixedUpdate(this);
    }
    void OnCollisionEnter2D(Collision2D collision){
        currentState.PlayerCollision(collision);
        if (collision.contactCount == 0) return;

        collisionObject = collision.gameObject; //objeto que colidiu

        collisionNormal = collision.contacts[0].normal;
        globalAngle = Mathf.Atan2(collisionNormal.y, collisionNormal.x) * Mathf.Rad2Deg; //Angulo de colisão global (cima, baixo, esquerda, direita)

        if (Mathf.Abs(globalAngle) > 70f && Mathf.Abs(globalAngle) < 110f && globalAngle > 0)
        {
            SwitchState(walkingState);
        }
    }


    public void SwitchState( PlayerBase state )
    {
        currentState = state;
        currentState.InitializeDefaults(rb, boxCollider, groundLayer, asas, ManaManager);
        currentState.EnterState(this);
    }

    public void ManagerStartCoroutine(IEnumerator coroutine){
        StartCoroutine(coroutine);
    }



    

}
