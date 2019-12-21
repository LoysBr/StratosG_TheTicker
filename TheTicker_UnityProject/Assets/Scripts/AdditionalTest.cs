using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTest : MonoBehaviour
{
    float m_timerUpdate;
    int m_updateCount;

    DateTime m_startDateTime;

    List<GameObject> m_instantiatedGOs;

    void Start()
    {
        m_timerUpdate = 0f;
        m_updateCount = 0;
        StartCoroutine("CoroutineTimer");
        //StartCoroutine("CoroutineTimerWithLongOperation");
        StartCoroutine("CoroutineTimer_Unscaled");     

        m_instantiatedGOs = new List<GameObject>();

        m_startDateTime = DateTime.Now;
    }

    
    void Update()
    {
        m_timerUpdate += Time.deltaTime;
        Debug.Log("Timer Update = " + m_timerUpdate);

        TimeSpan sysDelta = DateTime.Now - m_startDateTime;
        Debug.Log("System deltaTime = " + sysDelta);

        StartCoroutine("CoroutineWaitOneSec", m_updateCount.ToString());

        m_updateCount++;


        // big sync operation
        // I put it at the end of Update voluntarily 
        foreach (GameObject go in m_instantiatedGOs)
            DestroyImmediate(go);

        for (int i = 0; i < 100000; i++)
        {
            GameObject go = Instantiate(new GameObject());
            m_instantiatedGOs.Add(go);
        }
    }

    IEnumerator CoroutineTimer()
    {
        float timer = 0f;
        float tickDuration = 1f;
        while(true)
        {            
            yield return new WaitForSeconds(tickDuration);
            timer += tickDuration;
            Debug.Log("Timer Coroutine = " + timer);
        }
    }

    IEnumerator CoroutineTimerWithLongOperation()
    {
        float timer = 0f;
        float tickDuration = 1f;
        while (true)
        {
            yield return new WaitForSeconds(tickDuration);
            timer += tickDuration;
            Debug.Log("Timer CoroutineTimerWithLongOperation = " + timer);

            for (int i = 0; i < 10000; i++)
            {
                GameObject go = Instantiate(new GameObject());
            }
        }
    }

    IEnumerator CoroutineWaitOneSec(string n)
    {
        Debug.Log("CoroutineWaitOneSec number " + n + " Start");
        yield return new WaitForSeconds(1f);
        Debug.Log("CoroutineWaitOneSec number " + n + " End");
    }

    IEnumerator CoroutineTimer_Unscaled()
    {
        float timer = 0f;
        float tickDuration = 1f;
        while (true)
        {
            yield return new WaitForSecondsRealtime(tickDuration);
            timer += tickDuration;
            Debug.Log("Timer Coroutine_unscaled = " + timer);
        }
    }
}
