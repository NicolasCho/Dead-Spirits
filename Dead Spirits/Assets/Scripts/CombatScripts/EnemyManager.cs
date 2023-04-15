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

}
