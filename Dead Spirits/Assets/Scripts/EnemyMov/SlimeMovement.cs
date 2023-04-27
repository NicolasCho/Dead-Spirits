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
    public GameObject spirit;


    void Update (){
        distance = Vector2.Distance(transform.position, target.position);

        if(canMove){
           if (distance <= attackRange){
                StartCoroutine(AttackCoroutine());
            }
            else if (distance <= range && !damagedTime)
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

    IEnumerator stopMovement(bool kill){
        damagedTime = true;
        float time;
        float timer = 0f;
        
        time = 2f;
        while(timer < time){
            timer += Time.deltaTime;
            yield return null;
        }
        if (kill){
            Instantiate(spirit, transform.position, Quaternion.identity);
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            yield return new WaitForSeconds(2f);
            Destroy(this.gameObject);
        }    
        damagedTime = false;
    }

    void OnTriggerEnter2D(Collider2D other){
        GameObject attacker = other.gameObject;
        if (attacker.tag == "PlayerAttack" || attacker.tag == "SummonedSpirit"|| (other.gameObject.tag == "Spirit" && other is BoxCollider2D)){
            if (attacker.tag == "PlayerAttack")
                attacker.GetComponentInParent<PlayerManager>().ComboSystem(false);

            GetComponent<EnemyManager>().TakeDamage();
            if (GetComponent<EnemyManager>().HP == 0){
                canMove=false;
                animator.SetTrigger("Dead");
                StartCoroutine(stopMovement(true));
            }else{
                animator.SetTrigger("Damaged");
                StartCoroutine(stopMovement(false));
            } 
        }
    }
}
