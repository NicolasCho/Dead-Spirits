using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public Camera cam;
    public float runSpeed = 20.0f;

    private Vector2 mousePos;

    float horizontal;
    float vertical;
    float mouseAngle;
    public Animator animator;
    private float inhibitMovement; 
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
            if (Input.GetMouseButtonDown(0))
                {
                    Vector2 lookDir = mousePos - rb.position;
                    mouseAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                    animator.SetFloat("Angle", mouseAngle);
                    animator.SetTrigger("Attack");
                }

            if(canDodge){
                if (Input.GetKeyDown("space"))
                {
                    animator.SetTrigger("Dodge");
                }  
            }

            if (Input.GetMouseButtonDown(1)){
                animator.SetTrigger("Cast");
            }
                

        }
    }

    private void FixedUpdate(){  
        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Enemy"){
            animator.SetTrigger("Damaged");
            GetComponent<PlayerManager>().TakeDamage();
        }
    }

}
