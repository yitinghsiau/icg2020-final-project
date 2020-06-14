using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fly : MonoBehaviour
{

    public int ID = 2;

    void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(this.gameObject);
    }


}
