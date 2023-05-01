using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("o"))
        {
            Menu.SetActive(!Menu.activeSelf);
            player.GetComponent<PlayerMovement>().enabled = false;
            Time.timeScale = 0.0f;
        }
    }
}
