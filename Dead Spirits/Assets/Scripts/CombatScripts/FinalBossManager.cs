using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossManager : MonoBehaviour
{   
    public Transform target;
    public GameObject thunderMagic;
    public GameObject damagePoint;
    float magicCD = 3.0f;
    float randomThunder = 3.0f;
    public Animator animator;
    bool casting = false;
    bool isAngry = false;
    bool damaged=false;
    public int lifePhase = 10;
    public int HP = 2;

    private AudioSource attack_sound; 

    void Start(){
        attack_sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        magicCD -= Time.deltaTime;
        randomThunder -= Time.deltaTime;
        if ( magicCD < 0  && !casting){
            StartCoroutine(followPlayer());
        }
        if (randomThunder < 0){
            var position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            Instantiate(thunderMagic, new Vector2(transform.position.x, transform.position.y)+position, Quaternion.identity);   
            if (isAngry){
                var position_ = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
                Instantiate(thunderMagic, new Vector2(transform.position.x, transform.position.y)+position_, Quaternion.identity);
                randomThunder = 1f;
            }
            else
                randomThunder = 3f;
        }
    }

    IEnumerator followPlayer(){  
        casting = true;    
        animator.SetTrigger("casting");
        if (isAngry){
            for(int i=0; i<20; i++){
                var thunder = Instantiate(thunderMagic, target.position, Quaternion.identity);
                thunder.GetComponent<ThunderController>().ActivationCD = 0.5f;
                float time;
                float timer = 0f;
                time = 0.5f;
                while(timer < time){
                    timer += Time.deltaTime;
                    yield return null;
                }
            } 
        }else{
            for(int i=0; i<10; i++){
                if (damaged)
                    break;
                Instantiate(thunderMagic, target.position, Quaternion.identity);
                float time;
                float timer = 0f;
                time = 0.5f;
                while(timer < time){
                    timer += Time.deltaTime;
                    yield return null;
                }
            } 
        }
        if(!damaged)
            animator.SetTrigger("idle");
        if (isAngry)
            yield return new WaitForSeconds(2f);
        else
            yield return new WaitForSeconds(1f);
        StartCoroutine(CreateThunderAroundPoint(20, new Vector2(target.position.x, target.position.y), 5f));
    }

    // tirado de https://answers.unity.com/questions/1661755/how-to-instantiate-objects-in-a-circle-formation-a.html
    IEnumerator CreateThunderAroundPoint(int num, Vector2 point, float radius){ 
        animator.SetTrigger("casting");
        if(isAngry){
            for (int j = 0; j < 3; j++){
                for (int i = 0; i < num; i++){
                /* Distance around the circle */
                var radians = 2 * Mathf.PI / num * i;
        
                /* Get the vector direction */
                var vertical = Mathf.Sin(radians);
                var horizontal = Mathf.Cos(radians);
        
                var spawnDir = new Vector2(horizontal, vertical);
        
                /* Get the spawn position */
                var spawnPos = point + spawnDir * radius; 

                /* Now spawn */
                var thunder =Instantiate(thunderMagic, spawnPos, Quaternion.identity);
                thunder.GetComponent<ThunderController>().ActivationCD = 0.5f;
                }
                radius -= radius/2;
                num -= num/2;
                yield return new WaitForSeconds(1f);
            }
        }else{
            for (int i = 0; i < num; i++){
            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;
    
            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);
    
            var spawnDir = new Vector2(horizontal, vertical);
    
            /* Get the spawn position */
            var spawnPos = point + spawnDir * (radius+1); 

            /* Now spawn */
            Instantiate(thunderMagic, spawnPos, Quaternion.identity);
            Instantiate(thunderMagic, point, Quaternion.identity);            
            }
        }
        yield return new WaitForSeconds(2f);
        animator.SetTrigger("idle");
        casting = false;
        if (isAngry)
            magicCD = 2f;
        else
            magicCD = 5f;
    }  

    public void TakeDamage(){
        damaged = true;
        animator.SetTrigger("damaged");
        HP-=1;
        if (HP==1)
            isAngry = true;
        StartCoroutine(DamageTime());
    }

    IEnumerator DamageTime(){
        yield return new WaitForSeconds(1f);
        animator.SetTrigger("idle");
        damaged =false;
    }


    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "PlayerAttack"){
            attack_sound.Play();
            lifePhase -= 1;
        }
        else if (other.tag == "SummonedSpirit"){
            lifePhase -= 2;
        }
        if (lifePhase <= 0){
            var position = new Vector2(Random.Range(-5, 5), Random.Range(5, 5));
            var DP = Instantiate(damagePoint, new Vector2(transform.position.x, transform.position.y)+position, Quaternion.identity);
            DP.GetComponent<DamagePointManager>().boss = gameObject;
            lifePhase = 10;
        }
    } 
}
