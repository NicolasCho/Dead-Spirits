using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritManager : MonoBehaviour
{
    public GameObject summonedSpirit;

    public Animator animator;

    public void ActivateMagic(int magic){
        if (magic == 0)
            SummonMagic();
        else if (magic == 1)
            ExplosionMagic();
    }

    public void SummonMagic(){
        Instantiate(summonedSpirit, transform.position, Quaternion.identity);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        StartCoroutine(destroyObject());
    }

    public void ExplosionMagic(){
        animator.SetTrigger("magic");
        StartCoroutine(destroyObject());
    }

    IEnumerator destroyObject(){
        float time;
        float timer = 0f;
        
        time = 2f;
        while(timer < time){
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
