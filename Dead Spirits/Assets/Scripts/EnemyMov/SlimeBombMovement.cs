using UnityEngine;
using System.Collections;
public class SlimeBombMovement : MonoBehaviour{
    public Transform target;
    public float speed = 2f;
    public float range = 8f;
    private float distance;
    public Animator animator;
    public bool canMove = true;
    public bool damagedTime = false;
    public GameObject spirit;


    void Update (){
        distance = Vector2.Distance(transform.position, target.position);

        if(canMove){
            if (distance <= range && !damagedTime)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                animator.SetFloat("deltax",transform.position.x - target.position.x);
            }
            else{
                animator.SetTrigger("Idle");
                animator.SetFloat("deltax",0f);
            } 
        }
    }

    public void Explode(){
        StartCoroutine(destroyObject());
    }

    IEnumerator destroyObject(){
        damagedTime = true;
        float time;
        float timer = 0f;
        animator.SetTrigger("Explode");

        time = 3f;
        while(timer < time){
            timer += Time.deltaTime;
            yield return null;
        }
        Instantiate(spirit, transform.position, Quaternion.identity);
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
