using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class InteractionKey : MonoBehaviour
{

    public GameObject obj;

    public void NotifyPlayer()
    {
        obj.SetActive(true);
    }

    public void DeNotifyPlayer()
    {
        obj.SetActive(false);
    }
}
