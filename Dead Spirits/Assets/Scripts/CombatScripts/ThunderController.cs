using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderController : MonoBehaviour
{
    public float ActivationCD = 3.0f;
    public Animator animator;
    private AudioSource thunder;

    void Start()
    {   
        thunder = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ActivationCD -= Time.deltaTime;
        if ( ActivationCD < 0 ){
            StartCoroutine(ActivateMagic());
            ActivationCD = 200f;
        }
    }

    IEnumerator ActivateMagic()
    {      
        animator.SetTrigger("ActivateMagic");
        thunder.Play();
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
