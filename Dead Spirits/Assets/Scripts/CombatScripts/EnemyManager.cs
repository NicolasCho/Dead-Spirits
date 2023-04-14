using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int HP;
    public int Str;

    public void TakeDamage(){
        HP -= 1;
    }

    public void onCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "PlayerAttack"){
            TakeDamage();
        }
    }
}
