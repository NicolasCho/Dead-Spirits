using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{   
    public int HP = 5;
    public int Str;
    public Transform respawnCoordinates;

    public int currHP = 5;

    public GameObject[] hearts;
    public GameObject[] gauges;
    public GameObject[] spells;

    public GameObject extraHeart;
    public GameObject extraHeartSpace;

    public bool gotMagic=false;
    public static bool gotExtraHealth = false;
    public int currMagic = 0;
    public int comboCount=0;

    void Start(){
        print(gotExtraHealth);
        if (gotExtraHealth){
            extraHeart.GetComponent<SpriteRenderer>().enabled = true;
            extraHeartSpace.GetComponent<SpriteRenderer>().enabled = true;
            HP = 6;
            currHP = 6;
        }
    }

    public void AddHealth(){
        gotExtraHealth = true;
        extraHeartSpace.GetComponent<SpriteRenderer>().enabled = true;
        extraHeart.GetComponent<SpriteRenderer>().enabled = true;
        foreach (GameObject heart in hearts){
            heart.GetComponent<SpriteRenderer>().enabled = true;
        }
        HP = 6;
        currHP = 6;
    }

    public void TakeDamage(int damage){
        if (currHP == 6){
            currHP -= damage;
            extraHeart.GetComponent<SpriteRenderer>().enabled = false;
        }
        else{
            currHP -= damage;
            hearts[currHP].GetComponent<SpriteRenderer>().enabled =false;
        }
        if (currHP == 0){
            PlayerDead();
        }
    } 

    public void PlayerDead(){
        foreach (GameObject heart in hearts){
            heart.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(gotExtraHealth){
            extraHeart.GetComponent<SpriteRenderer>().enabled = true;
            currHP = 6;
        }
        else
            currHP = 5;
        GetComponent<SpriteRenderer>().enabled = false;
        RespawnPlayer();
    }

    public void ComboSystem(bool resetCombo){
        gauges[comboCount].GetComponent<SpriteRenderer>().enabled = false;
        if(resetCombo)
            comboCount = 0;
        else if(comboCount < 3)
            comboCount += 1;
        gauges[comboCount].GetComponent<SpriteRenderer>().enabled = true;
    }



    public void ChangeMagic(){
        if (gotMagic){
            spells[currMagic].GetComponent<SpriteRenderer>().enabled = false;
            if(currMagic == 0){
                currMagic = 1;
            }
            else{
                currMagic = 0;
            }
            spells[currMagic].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void RespawnPlayer(){
        GetComponent<SpriteRenderer>().enabled = true;
        transform.position = new Vector2(respawnCoordinates.position.x, respawnCoordinates.position.y);
    }
}
