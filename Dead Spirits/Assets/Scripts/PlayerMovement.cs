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

    

    void Start(){
        rb = GetComponent<Rigidbody2D>(); 
        cam = Camera.main;
    }

    void Update(){
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
    }

    private void FixedUpdate(){  
        rb.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        // int animationIdx = ChangeSprite(angle);
    }
}
