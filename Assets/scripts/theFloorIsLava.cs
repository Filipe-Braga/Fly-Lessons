using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theFloorIsLava : MonoBehaviour
{
    [SerializeField] private float towerX, towerY;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Rigidbody2D rb;
    private BoxCollider2D boxcol;

    [SerializeField] public Equilibrio balanceManager;

    [SerializeField] public ManaManager manaManager;

    void Start()
    {
        boxcol = GetComponent<BoxCollider2D>();
    }


    void FixedUpdate()
    {
        if(onLava()){
            rb.velocity = Vector2.zero;
            transform.position = new Vector3(towerX,towerY,0);

            //Resetar Equilibrio 
            balanceManager.ResetBalance();


            //Resetar Mana
            manaManager.ResetMana();
        }
    }

    private bool onLava() 
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcol.bounds.center, boxcol.bounds.size, 0, Vector2.down, 0.1f, Ground );
        return raycastHit.collider != null;
    }
}
