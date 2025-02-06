using UnityEngine;

public abstract class PlayerBase
{
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    protected LayerMask groundLayer;

    protected GameObject asas;

    protected float gravidadeBase = 1;
    protected int verticalState; // -1: Falling, 0: Idle, 1: Rising

    protected bool isFlipped = true;

    // Método para configurar os valores padrão
    public void InitializeDefaults(Rigidbody2D rigidbody, BoxCollider2D collider, LayerMask layer, GameObject Asas)
    {
        rb = rigidbody;
        boxCollider = collider;
        groundLayer = layer;
        asas = Asas;


    }

    public abstract void EnterState(PlayerManager playerState);

    public abstract void UpdateState(PlayerManager playerState);

    public abstract void FixedUpdate(PlayerManager playerState);
}