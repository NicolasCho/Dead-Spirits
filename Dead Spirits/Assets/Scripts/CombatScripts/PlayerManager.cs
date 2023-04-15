using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    public int HP;
    public int Str;

    public void TakeDamage(){
        HP -= 1;
    }
}
