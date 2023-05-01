using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject redSlime;
    public GameObject player;
    float spawnCD = 2.0f;
    public bool isInRange;
    public bool isBossAlive=true;
    bool aux = true;

    void Update()   
    {   
        if (isInRange && isBossAlive){
            spawnCD -= Time.deltaTime;
            if(spawnCD <= 0){
                int index = Random.Range (0, spawners.Length);
                var slime = Instantiate(redSlime, spawners[index].transform.position, Quaternion.identity);
                slime.GetComponent<SlimeBombMovement>().target = player.transform;
                spawnCD = 2f;
            }
        }
        if (!isBossAlive && aux){
            aux = false;
            RemoveSlimes();
        }

    }

    public void StopSpawning(){
        isBossAlive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }

    public void RemoveSlimes(){
        // Find all slimes and activate explosion in each one
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 20);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Bomb"){
                Destroy(hitCollider.gameObject);
            }
        }
    }
}
