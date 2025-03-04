using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{

    [Header("UGUI")]
    [SerializeField] TextMeshProUGUI HUD;
    [SerializeField] public PlayerManager playerManager;
    [SerializeField] private Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HUDUpdate();
    }

    public void HUDUpdate(){
        HUD.text = "Velocidade: " + (rb.velocity.magnitude * 3.6f).ToString("F0") + "km/h \n";
        HUD.text += "Altitude: " + transform.position.y.ToString("F0") + "m \n";
        HUD.text += "Estado: " + playerManager.currentState + "\n";
        HUD.text += "UltimaColisão: " + playerManager.globalAngle + "\n";
        
    }
}
