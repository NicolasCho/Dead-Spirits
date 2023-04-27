using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public Camera cam;
    public float runSpeed = 5.0f;

    private Vector2 mousePos;

    float horizontal;
    float vertical;
    float mouseAngle;
    public Animator animator;
    public float magicRange;
    private bool invincibility =false;

    public bool canMove{get
    {
        return animator.GetBool("canMove");
    }
    }

    public bool canInput{get
    {
        return animator.GetBool("canInput");
    }
    }

    public bool canDodge{get
    {
        return animator.GetBool("canDodge");
    }
    }
    

    void Start(){
        rb = GetComponent<Rigidbody2D>(); 
        cam = Camera.main;
    }

    void Update(){
        if(canMove){
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical"); 
        }else{
            horizontal = 0;
            vertical = 0;
        }
        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);


        
        if(horizontal == 0 & vertical == 0){
            animator.SetBool("Walking", false);
        }else{
            animator.SetBool("Walking", true);
        }

        if(canInput){
            if (Input.GetMouseButtonDown(0)){
                    Vector2 lookDir = mousePos - rb.position;
                    mouseAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                    animator.SetFloat("Angle", mouseAngle);
                    animator.SetTrigger("Attack");
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)){
                GetComponent<PlayerManager>().ChangeMagic();
            }

            if(canDodge){
                if (Input.GetKeyDown("space"))
                {   
                    animator.SetTrigger("Dodge");
                }  
            }

            if (Input.GetMouseButtonDown(1)){
                if(GetComponent<PlayerManager>().comboCount == 3){
                    if(GetComponent<PlayerManager>().currMagic == 0)
                        SummonMagic();
                    else if(GetComponent<PlayerManager>().currMagic == 1)
                        ExplosionMagic();
                }   
            }
        }
    }

    public void SummonMagic(){
        animator.SetTrigger("Cast");
        GetComponent<PlayerManager>().ComboSystem(true);
        // Spawn spirits
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position,magicRange);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Spirit"){
                hitCollider.gameObject.GetComponent<SpiritManager>().ActivateMagic(0);
            }
        }
    }

    public void ExplosionMagic(){
        animator.SetTrigger("Cast");
        GetComponent<PlayerManager>().ComboSystem(true);
        // Spawn spirits
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position,magicRange);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Spirit"){
                hitCollider.gameObject.GetComponent<SpiritManager>().ActivateMagic(1);
            }
        }
    }

    IEnumerator InvincibilityFrames()
    {   
        invincibility = true;
        yield return new WaitForSeconds(3.0f);
        invincibility= false;
    }

    private void FixedUpdate(){  
        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (!invincibility){
            if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss" || (other.gameObject.tag == "Bomb" && other is BoxCollider2D)){
                StartCoroutine(InvincibilityFrames());
                animator.SetTrigger("Damaged");
                GetComponent<PlayerManager>().TakeDamage();
            }
        }
    }
}
