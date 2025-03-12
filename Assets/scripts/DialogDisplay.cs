using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogDisplay : MonoBehaviour
{
    public TextMeshProUGUI TextComponent;
    public string[] lines;
    public float textSpeed;
    private int index;

    public PlayerManager playerManager;

    void Start(){
        TextComponent.text = string.Empty;
        StartDialog();
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.currentState = playerManager.frozenState;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.S)){
            if(TextComponent.text == lines[index]){
            NextLine();
            }
            else{
                StopAllCoroutines();
                TextComponent.text = lines[index];
            }
        }
    }

    void StartDialog(){
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine(){
        foreach(char c in lines[index].ToCharArray()){
            TextComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine(){
        if(index < lines.Length - 1){
            index++;
            TextComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else{
            index = 0;
            TextComponent.text = string.Empty;
            gameObject.SetActive(false); // Desativa o objeto para ocultar o diálogo
            playerManager.currentState = playerManager.walkingState;
        }
    }

}
