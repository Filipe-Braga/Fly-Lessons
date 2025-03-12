using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatDefault : ItemBase
{
    
    public override void Wear (ItemManager itemOnPlayer)
    {
        Debug.Log("HatDefault");
        rb.gravityScale = 0.5f;
    }


}
