using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class convertedSteering : MonoBehaviour
{

    public int ID = 3;

    void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(this.gameObject);
    }


}
