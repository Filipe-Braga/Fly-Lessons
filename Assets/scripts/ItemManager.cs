using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [SerializeField] public PlayerManager playerManager;
    [SerializeField] private Rigidbody2D rb;

    [Header("Hat")]
    public ItemBase currentHat;
    public HatDefault hatDefault = new HatDefault(); 



    void Start()
    {
        currentHat = hatDefault;

        currentHat.InitializeDefaults(rb);
        currentHat.Wear(this);
    }


}
