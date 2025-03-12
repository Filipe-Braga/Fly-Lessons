using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public GameObject Canvas;
    public GameObject interact;


    public void Update(){
        if(Canvas.activeSelf){//Aparentemente, o activeSelf é um bool que retorna se o objeto está ativo ou não
            if(Input.GetKeyDown(KeyCode.S)){
                Interact();
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            ActiveCanvas();
        }
    }

    public void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag == "Player"){
            DeactiveCanvas();
        }
    }

    public void ActiveCanvas(){
        Canvas.SetActive(true);
    }

    public void DeactiveCanvas(){
        Canvas.SetActive(false);
        interact.SetActive(false);
    }

    public void Interact(){
        Debug.Log("Interagindo com o objeto");
        interact.SetActive(true);
    }
}
