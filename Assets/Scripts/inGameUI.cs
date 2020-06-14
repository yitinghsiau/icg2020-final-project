using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inGameUI : MonoBehaviour
{
    [SerializeField] GameObject m_inGameUI_Object;
    [SerializeField] GameObject m_StartUI_Object;
    [SerializeField] GameObject m_EndGameUI_Object;

    [SerializeField] StartUI m_startUI;
    
    [SerializeField] Rigidbody m_car_Object;

    [SerializeField] carControl m_carControl;

    [SerializeField] Text m_message_Time;
    [SerializeField] Text m_message_Speed;
    [SerializeField] public Text m_message_Lap;
    [SerializeField] Text m_message_CountDown;

    float inGameTimer;
    public int minute = 0;
    public float second = 0;

    public float countDownTimer = 3.1f;
    public bool countDownCompleted = false;


    int lapNumber = 1;

    Vector3 position_car_initial = new Vector3(-114.6f, 2.007911f, 44.6f);
    Vector3 rotation_car_initial = new Vector3(0f, -138.239f, 0f);

    public bool resetCheck = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (m_startUI.gameStarted == true)
        {
            if (countDownCompleted == false)
            {
                m_message_CountDown.enabled = true;

                if (countDownTimer > 0.8f)
                {
                    
                    countDownTimer -= Time.deltaTime;
                    m_message_CountDown.text = countDownTimer.ToString("0");

                }
                else if (countDownTimer <= 0.8f)
                {
                    countDownCompleted = true;                    
                    countDownTimer -= Time.deltaTime;                    
                }
                
            }
            

            if (countDownCompleted == true)
            {
                m_message_CountDown.enabled = false;

                inGameTimer = inGameTimer + Time.deltaTime;
                minute = (int)(inGameTimer / 60f);
                second = inGameTimer % 60f;
                m_message_Time.text = minute.ToString("00") + ":" + second.ToString("00.00");
            }
            

            var speedDisplay = (int)(9 * m_car_Object.velocity.magnitude);
            m_message_Speed.text = speedDisplay.ToString("000") + " KM/H";

            if (m_carControl.startLine_passed == true)
            {
                lapNumber = lapNumber + 1;

                if (m_startUI.levelEasySelected == true)
                {
                    m_message_Lap.text = "Lap : " + lapNumber + "/1";
                }
                if (m_startUI.levelHardSelected == true)
                {
                    m_message_Lap.text = "Lap : " + lapNumber + "/3";
                }

                m_carControl.startLine_passed = false;
            }

        }

        if (m_startUI.levelEasySelected == true)
        {
            if (lapNumber == 2)
            {
                EndGame();

            }
        }
        if (m_startUI.levelHardSelected == true)
        {
            if (lapNumber == 4)
            {
                EndGame();

            }
        }
       
    }



    public void Reset()
    {
        resetCheck = true;
        m_startUI.ObjectReset();

        m_car_Object.transform.position = position_car_initial;
        m_car_Object.transform.rotation = Quaternion.Euler(rotation_car_initial);

        lapNumber = 1;
        m_message_Lap.text = "Lap : " + lapNumber + "/3";

        inGameTimer = 0;
        minute = (int)(inGameTimer / 60f);
        second = inGameTimer % 60f;
        m_message_Time.text = minute.ToString("00") + ":" + second.ToString("00.00");

        m_carControl.startLine_passed = false;
        m_carControl.checkPoint_2_passed = false;


        m_inGameUI_Object.SetActive(false);
        m_StartUI_Object.SetActive(true);
        m_EndGameUI_Object.SetActive(false);

        m_startUI.m_Camera[0].enabled = true;
        m_startUI.m_Camera[1].enabled = false;       
        m_startUI.m_Camera[2].enabled = false;

        m_startUI.levelEasySelected = false;
        m_startUI.levelHardSelected = false;

        m_startUI.m_StartUiText.text = "Please select game level";

        m_startUI.gameStarted = false;
        countDownCompleted = false;
        countDownTimer = 3.1f;

    }



    public void EndGame()
    {
        m_startUI.gameStarted = false;

        m_startUI.m_Camera[1].enabled = false;
        m_startUI.m_Camera[2].enabled = true;

        m_inGameUI_Object.SetActive(false);
        m_EndGameUI_Object.SetActive(true);
       
    }

    


}
