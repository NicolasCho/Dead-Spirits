using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterPowerLogic : MonoBehaviour

{

    public bool isInRange;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKey(KeyCode.E)){
                player.GetComponent<PlayerManager>().gotMagic = true;
                Destroy(this.gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
            isInRange = true;
            collision.gameObject.GetComponent<InteractionKey>().NotifyPlayer();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            collision.gameObject.GetComponent<InteractionKey>().DeNotifyPlayer();
        }
    }
}