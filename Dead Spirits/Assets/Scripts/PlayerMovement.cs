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
    public Animator animator;
    private float inhibitMovement; 
    public bool canMove{get
    {
        return animator.GetBool("canMove");
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

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

    }

    private void FixedUpdate(){  
        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        // int animationIdx = ChangeSprite(angle);
    }

}
