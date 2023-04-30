using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePointManager : MonoBehaviour
{
    public GameObject boss;
    public Animator animator;
    float DespawnTimer = 7.0f;

    void Start(){
        boss.GetComponent<BoxCollider2D>().enabled=false;
    }

    void Update(){
        DespawnTimer -= Time.deltaTime;
        if ( DespawnTimer < 0 ){
            animator.SetTrigger("damaged");
            StartCoroutine(Despawn());
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PlayerAttack"){
            animator.SetTrigger("damaged");
            boss.GetComponent<FinalBossManager>().TakeDamage();
            StartCoroutine(Despawn());
        }
    } 
    IEnumerator Despawn()
    {      
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
        boss.GetComponent<BoxCollider2D>().enabled=true;
    }
}
