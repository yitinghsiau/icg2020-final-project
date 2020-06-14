using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] inGameUI m_inGameUI;
    [SerializeField] Text m_message_Time;

    int m_minute = 0;
    float m_second = 0;

    public bool gameEnded;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_minute = m_inGameUI.minute;
        m_second = m_inGameUI.second;
        m_message_Time.text = m_minute.ToString("00") + ":" + m_second.ToString("00.00");
    }

    public void EndGameReset()
    {
        m_inGameUI.Reset();
    }
}
