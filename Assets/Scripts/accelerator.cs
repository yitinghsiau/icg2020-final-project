using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class accelerator : MonoBehaviour
{

    public int ID = 1;

    void OnTriggerEnter(Collider other)
    {
        GameObject.Destroy(this.gameObject);
    }


}
