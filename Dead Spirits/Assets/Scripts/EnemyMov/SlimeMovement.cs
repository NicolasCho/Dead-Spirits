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
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime*1f);
        animator.SetTrigger("Attack");
        canMove = false;

        float timer = 0f;
        float time = 2f;
        while(timer < time){
            timer += Time.deltaTime;
            yield return null;
        }
        canMove = true;
    }

}
