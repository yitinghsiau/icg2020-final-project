using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    [SerializeField] Light sun;
    [SerializeField] GameObject carLights;

    [SerializeField] public Camera[] m_Camera = new Camera[3];
    [SerializeField] GameObject m_StartUI_Object;
    [SerializeField] GameObject m_inGameUI_Object;
    [SerializeField] inGameUI m_inGameUI;

    [SerializeField] public Text m_StartUiText;


    Vector3 block_position_straight_a = new Vector3(-41.3f, 2.037f, 109.4f);
    Vector3 block_position_straight_b = new Vector3(-70.9f, 2.037f, 87.3f);
    Vector3 block_position_bridge_a   = new Vector3(-24.5f, 2.037f, -158.2f);
    Vector3 block_position_bridge_b   = new Vector3(27.5f, 2.037f, -147.2f);
    Vector3 block_position_turn       = new Vector3(115.9f, 2.037f, 38.5f);

    Vector3 hard_position_short_a = new Vector3(130.11f, 2.037f, -122.67f);
    Vector3 hard_position_short_b = new Vector3(133.26f, 2.037f, -117.19f);
    Vector3 hard_position_short_c = new Vector3(134.58f, 2.037f, -111.52f);
    Vector3 hard_position_long_a  = new Vector3(131.76f, 2.037f, 158.25f);
    Vector3 hard_position_long_b  = new Vector3(110.35f, 2.037f, 162.85f);

    Vector3 hard_rotation_short_a = new Vector3(0, -54.25f, 0);
    Vector3 hard_rotation_short_b = new Vector3(0, -68.23f, 0);
    Vector3 hard_rotation_short_c = new Vector3(0, -83.054f, 0);
    Vector3 hard_rotation_long_a  = new Vector3(0, 18.45f, 0);
    Vector3 hard_rotation_long_b  = new Vector3(0, 5.797f, 0);

    Vector3 easy_position_short_a = new Vector3(129.24f, 2.037f, -106.79f);
    Vector3 easy_position_short_b = new Vector3(137.48f, 2.037f, 148.23f);
    Vector3 easy_rotation_short_b = new Vector3(0, -69.53f, 0);


    public bool levelEasySelected = false;
    public bool levelHardSelected = false;

    public bool gameStarted = false;

    GameObject hard_short_a;
    GameObject hard_short_b;
    GameObject hard_short_c;
    GameObject hard_long_a;
    GameObject hard_long_b;

    GameObject block_straight_a;
    GameObject block_straight_b;
    GameObject block_bridge_a;
    GameObject block_bridge_b;
    GameObject block_turn;

    GameObject easy_short_a;
    GameObject easy_short_b;

    // Start is called before the first frame update
    void Start()
    {
        m_inGameUI_Object.SetActive(false);
    }

    
    void Update()
    {
        
    }


    public void GameStart()
    {
        if (levelEasySelected == true || levelHardSelected == true)
        {
            m_Camera[1].enabled = true;
            m_Camera[0].enabled = false;

            m_StartUI_Object.SetActive(false);

            gameStarted = true;
            m_inGameUI_Object.SetActive(true);
            m_inGameUI.resetCheck = false;            
        }
        else
        {
            m_StartUiText.text = "<color=yellow>PLEASE SELECT LEVEL!</color>";
        }
        
    }



    public void Day()
    {
        sun.enabled = true;
        carLights.SetActive(false);

    }

    public void Night()
    {
        sun.enabled = false;
        carLights.SetActive(true);
    }


    public void Easy()
    {
        m_inGameUI.m_message_Lap.text = "Lap : 1/1";

        if (levelEasySelected == false)
        {
            m_StartUiText.text = "Level selected : Easy";

            easy_short_a = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker_short"), easy_position_short_a, Quaternion.Euler(0f, 0f, 0f));
            easy_short_b = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker_short"), easy_position_short_b, Quaternion.Euler(easy_rotation_short_b));

            levelEasySelected = true;

            if (levelHardSelected == true)
            {
                Destroy(hard_short_a);
                Destroy(hard_short_b);
                Destroy(hard_short_c);

                Destroy(hard_long_a);
                Destroy(hard_long_b);

                Destroy(block_straight_a);
                Destroy(block_straight_b);
                Destroy(block_bridge_a);
                Destroy(block_bridge_b);
                Destroy(block_turn);

                levelHardSelected = false;
            }

        }
           
    }




    public void Hard()
    {

        m_inGameUI.m_message_Lap.text = "Lap : 1/3";

        if (levelHardSelected == false)
        {
            m_StartUiText.text = "Level selected : Hard";

            block_straight_a = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker"), block_position_straight_a, Quaternion.Euler(0f, 0f, 0f));
            block_straight_b = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker"), block_position_straight_b, Quaternion.Euler(0f, 0f, 0f));
            block_bridge_a = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker"), block_position_bridge_a, Quaternion.Euler(0f, 0f, 0f));
            block_bridge_b = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker"), block_position_bridge_b, Quaternion.Euler(0f, 0f, 0f));
            block_turn = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker"), block_position_turn, Quaternion.Euler(0f, 0f, 0f));

            hard_short_a = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker_short"), hard_position_short_a, Quaternion.Euler(hard_rotation_short_a));
            hard_short_b = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker_short"), hard_position_short_b, Quaternion.Euler(hard_rotation_short_b));
            hard_short_c = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker_short"), hard_position_short_c, Quaternion.Euler(hard_rotation_short_c));

            hard_long_a = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker_long"), hard_position_long_a, Quaternion.Euler(hard_rotation_long_a));
            hard_long_b = (GameObject)Instantiate(Resources.Load<GameObject>("RoadBlocker_long"), hard_position_long_b, Quaternion.Euler(hard_rotation_long_b));

            levelHardSelected = true;


            if (levelEasySelected == true)
            {
                Destroy(easy_short_a);
                Destroy(easy_short_b);

                levelEasySelected = false;
            }
        }
        
    }


    public void ObjectReset()
    {
        Destroy(hard_short_a);
        Destroy(hard_short_b);
        Destroy(hard_short_c);

        Destroy(hard_long_a);
        Destroy(hard_long_b);

        Destroy(block_straight_a);
        Destroy(block_straight_b);
        Destroy(block_bridge_a);
        Destroy(block_bridge_b);
        Destroy(block_turn);

        Destroy(easy_short_a);
        Destroy(easy_short_b);
    }

}
