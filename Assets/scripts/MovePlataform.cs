using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlataform : MonoBehaviour
{
    [SerializeField] public float velocity = -1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x + velocity * Time.deltaTime, transform.position.y, 0);
    }

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.transform.parent = transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            collision.gameObject.transform.parent = null;
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
         Debug.Log($"Colidiu com: {collision.gameObject.name} (Tag: {collision.gameObject.tag})");

        if(collision.gameObject.CompareTag("Killer")){
            Destroy(gameObject);
            Debug.Log("Destroi vai, nunca te pedi nada");
        }
    }
}
