using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicBoxes : MonoBehaviour
{
    Vector3 position_straight_a = new Vector3(3.52f, 2.85f, 135.87f);
    Vector3 position_straight_b = new Vector3(5.5f, 2.85f, 132.8f);
    Vector3 position_bridge_a   = new Vector3(-72.9f, 2.85f, -157.1f);
    Vector3 position_bridge_b   = new Vector3(-74.18f, 2.85f, -160.56f);
    Vector3 position_mud_a      = new Vector3(99.07f, 2.85f, -134.69f);
    Vector3 position_mud_b      = new Vector3(128.4f, 2.85f, -98.9f);

    [SerializeField] carControl m_car;


    const float INTERVAL = 1f;
    float Timer = 0;

    
    void Start()
    {
        GameObject.Instantiate(Resources.Load<GameObject>("accelerator")).transform.position = position_straight_a;
        GameObject.Instantiate(Resources.Load<GameObject>("fly")).transform.position = position_straight_b;

        GameObject.Instantiate(Resources.Load<GameObject>("fly")).transform.position = position_bridge_a;
        GameObject.Instantiate(Resources.Load<GameObject>("convertedSteering")).transform.position = position_bridge_b;

        GameObject.Instantiate(Resources.Load<GameObject>("convertedSteering")).transform.position = position_mud_a;
        GameObject.Instantiate(Resources.Load<GameObject>("fly")).transform.position = position_mud_b;
    }

    
    // Update is called once per frame
    void Update()
    {
        
        if (m_car.acquiredBoxID != 0)
        {
            
            UpdateBox(m_car.acquiredBoxPos, m_car.acquiredBoxID);
            
        }
       
    }



    void UpdateBox(Vector3 boxPos, int ID)
    {
        if (Timer > INTERVAL)
        {
            Timer = 0;
            if (ID == 1)
            {
                GameObject.Instantiate(Resources.Load<GameObject>("accelerator")).transform.position = boxPos;
            }
            else if (ID == 2)
            {
                GameObject.Instantiate(Resources.Load<GameObject>("fly")).transform.position = boxPos;
            }
            else if (ID == 3)
            {
                GameObject.Instantiate(Resources.Load<GameObject>("convertedSteering")).transform.position = boxPos;
            }

            m_car.acquiredBoxID = 0;
        }
        else
        {
            Timer += Time.deltaTime;
        }
    }
}
