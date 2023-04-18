using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    public int HP = 5;
    public int Str;

    public int currHP = 5;

    public GameObject[] hearts;
    public GameObject[] gauges;

    public int comboCount=0;

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

    public void ComboSystem(bool resetCombo){
        gauges[comboCount].GetComponent<SpriteRenderer>().enabled = false;
        if(resetCombo)
            comboCount = 0;
        else if(comboCount < 3)
            comboCount += 1;
        gauges[comboCount].GetComponent<SpriteRenderer>().enabled = true;
    }
}
