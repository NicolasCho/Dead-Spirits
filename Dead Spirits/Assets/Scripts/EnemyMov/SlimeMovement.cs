using UnityEngine;
using System.Collections;
public class SlimeMovement : MonoBehaviour{
    public Transform target;
    public float speed = 2f;
    public float range = 8f;
    private float distance;

    public float attackRange;
    public Animator animator;
    public bool canMove = true;
    public bool damagedTime = false;


    void Update (){
        distance = Vector2.Distance(transform.position, target.position);

        if(canMove){
           if (distance <= attackRange){
                StartCoroutine(AttackCoroutine());
            }
            else if (distance <= range)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                animator.SetFloat("deltax",transform.position.x - target.position.x);
            }
            else{
                animator.SetTrigger("idle");
                animator.SetFloat("deltax",0f);
            } 
        }
        
    }

    IEnumerator AttackCoroutine()
    {   
        yield return new WaitForSeconds(1f);
        if (!damagedTime)
          transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime*1f);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1f);
        canMove = false;
        yield return new WaitForSeconds(3f);
        canMove = true;
    }

    IEnumerator stopMovement(){
        damagedTime = true;
        float time;
        float timer = 0f;
        
        time = 4f;
        while(timer < time){
            timer += Time.deltaTime;
            yield return null;
        }
        damagedTime = false;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PlayerAttack"){
            GetComponent<EnemyManager>().TakeDamage();
            animator.SetTrigger("Damaged");
            StartCoroutine(stopMovement());
        }
    }

}
