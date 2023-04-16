using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonedSpiritMovement : MonoBehaviour
{
    private GameObject target;
    public float speed = 2f;
    public float radius;
    public bool enemyFound;
    GameObject[] closeEnemies;
    public Animator animator;
    void Awake()
    {   
        SearchEnemy();
    }

    void Update (){
        if(enemyFound && target != null){
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            animator.SetFloat("deltax",transform.position.x - target.transform.position.x); 
        }else{
            SearchEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Enemy"){
            Destroy(this.gameObject);
        }
    }

    void SearchEnemy(){
        enemyFound = false;
        // Find the nearest enemy within radius and set as target
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);
        GameObject closest;
        float minDistance = 100000f;
        float distance;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Enemy"){
                enemyFound = true;
                distance = Vector2.Distance(transform.position, hitCollider.gameObject.transform.position);
                if (distance < minDistance){
                    closest = hitCollider.gameObject;
                }
            }
        }
        if(enemyFound)
            target = GameObject.FindWithTag("Enemy");
        else
            Destroy(this.gameObject);
    }
}
