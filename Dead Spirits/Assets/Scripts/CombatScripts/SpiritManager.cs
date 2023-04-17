using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
    public GameObject summonedSpirit;

    public void ActivateMagic(){
        Instantiate(summonedSpirit, transform.position, Quaternion.identity);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        StartCoroutine(destroyObject());
    }

    IEnumerator destroyObject(){
        float time;
        float timer = 0f;
        
        time = 1f;
        while(timer < time){
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
