using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCutscene : MonoBehaviour
{
    public GameObject boss;
    public GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boss.GetComponent<FinalBossManager>().HP <= 0){
            director.SetActive(true);
        }
    }
}
