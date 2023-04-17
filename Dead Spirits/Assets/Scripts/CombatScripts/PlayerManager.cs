using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    public int HP = 5;
    public int Str;

    public int currHP = 5;

    public GameObject[] hearts;

    public void TakeDamage(){
        currHP -= 1;
        hearts[currHP].GetComponent<SpriteRenderer>().enabled =false;
        if (currHP == 0){
            PlayerDead();
        }
    } 

    public void PlayerDead(){
        //Melhorar isso aqui
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
