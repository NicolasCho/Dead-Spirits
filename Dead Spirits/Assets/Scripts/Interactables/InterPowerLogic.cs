using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterPowerLogic : MonoBehaviour

{
    public GameObject explosionMagic;

    void OnTriggerStay2D(Collider2D other){
        if (Input.GetKey(KeyCode.E) && other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerManager>().gotMagic = true;
            Destroy(this.gameObject);
        }
    }
}