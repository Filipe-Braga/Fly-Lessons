using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenScript : PlayerBase
{

    public override void EnterState(PlayerManager playerState){
        Debug.Log("Fozen");
    }

    public override void UpdateState(PlayerManager playerState){
    }

    public override void FixedUpdate(PlayerManager playerState){
    }

    public override void PlayerCollision (Collision2D collision){}
}
