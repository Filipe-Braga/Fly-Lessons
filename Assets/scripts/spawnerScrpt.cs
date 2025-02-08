using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScrpt : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] BoxCollider2D spawnerArea;
    [SerializeField] float offset;
    [SerializeField] float spawnInterval = 5f; 
    private float spawnTimer = 0f; 

    

    void Update()
    { 
        spawnTimer += Time.deltaTime; // Incrementa o tempo

        if (spawnTimer >= spawnInterval) 
        {
            SpawnPlataform(); 
            spawnTimer = 0f; // Reseta o timer após o spawn
        }
    }

    Vector2 getPosition(){
        Vector2 spawnPositino = Vector2.zero;
        bool isValidSpace = false;

        int attempt = 0;
        int maxAttempt = 100;

        int InvalidLayer = LayerMask.NameToLayer("Plataform");

        while(!isValidSpace && attempt < maxAttempt){
            spawnPositino = getRandomPointInCollider();
            Collider2D[] colliders = Physics2D.OverlapCircleAll(spawnPositino,2f);

            bool isInvalid = false;
            foreach(Collider2D collider in colliders){
                if(collider.gameObject.layer == InvalidLayer){
                    isInvalid = true;
                    break;
                }
            }
            
            if(!isInvalid){
                isValidSpace = true;
            }

            attempt++;
        }

        if(!isValidSpace){
            Debug.LogWarning("Hoje o Céu está tão lindo");
        }

        return spawnPositino;
    }

    void SpawnPlataform(){
        Vector2 spawnPositino = getPosition();
        GameObject spawnCloud = Instantiate(prefab, spawnPositino, Quaternion.identity);
    }

    Vector2 getRandomPointInCollider(){
        Bounds colliderBounds = spawnerArea.bounds; //Pega as coordenadas do objeto spawn

        Vector2 minBounds = new Vector2(colliderBounds.min.x + offset, colliderBounds.min.y + offset);//Dá uma ajustada pra ele ficar um teco meno/maior f(offset)
        Vector2 maxBounds = new Vector2(colliderBounds.max.x - offset, colliderBounds.max.y - offset);
    
        float randomX = Random.Range(minBounds.x, maxBounds.x); //Acha um ponto random entre o maximo e o minimo
        float randomY = Random.Range(minBounds.y, maxBounds.y);
    
        return new Vector2(randomX, randomY); //retorna o ponto em x e y
    }
}
