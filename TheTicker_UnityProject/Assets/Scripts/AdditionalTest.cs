using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalTest : MonoBehaviour
{
    public bool m_checkUnityTime;
    public bool m_checkUnityUnscaledTime;
    public bool m_checkSystemTime;
    public bool m_checkBlockingOperation;
    public bool m_checkDelta;

    private int m_updateCount;

    private float m_unityElapsedTime;
    private float m_unityUnscaledElapsedTime;

    private DateTime m_startDateTime;
    private TimeSpan m_systemElapsedTime;
    private TimeSpan m_systemDeltaTime;

    private List<GameObject> m_instantiatedGOs;

    private void Start()
    {
        m_updateCount = 0;
    }

    private void Init()
    {
        m_unityElapsedTime = m_unityUnscaledElapsedTime = 0f;

        m_instantiatedGOs = new List<GameObject>();

        m_startDateTime = DateTime.Now;
        m_systemDeltaTime = new TimeSpan();
        m_systemElapsedTime = new TimeSpan();

        if (m_checkUnityTime)
            StartCoroutine("CoroutineTimer", 1f);

        if (m_checkUnityUnscaledTime)
            StartCoroutine("CoroutineTimer_Unscaled", 1f);

        //StartCoroutine("CoroutineTimerWithLongOperation");     
    }
        
    void Update()
    {
        //there is some time between Start and first Update so I init timers here
        if (m_updateCount == 0)
        {
            Init();
        }
        else //I add delta here to have a proper 0 elapsed time
        {
            m_unityElapsedTime += Time.deltaTime;
            m_unityUnscaledElapsedTime += Time.unscaledDeltaTime;

            TimeSpan newElapsedTime = DateTime.Now - m_startDateTime;
            m_systemDeltaTime = newElapsedTime - m_systemElapsedTime;
            m_systemElapsedTime = newElapsedTime;
        }
                

        if (m_checkSystemTime)
        {    
            if(m_checkDelta)
                Debug.Log(m_updateCount + " deltaTime System  = " + m_systemDeltaTime);

            Debug.Log(m_updateCount + " ElapsedTime System = " + m_systemElapsedTime);
        }

        if (m_checkUnityTime)
        {
            if(m_checkDelta)
                Debug.Log(m_updateCount + " deltaTime Unity   = " + Time.deltaTime);
            
            //Debug.Log(m_updateCount + "My ElapsedTime Unity = " + m_unityElapsedTime);
            Debug.Log(m_updateCount + "   ElapsedTime Unity = " + Time.time);
        }
        if (m_checkUnityUnscaledTime)
        {
            if(m_checkDelta)
                Debug.Log(m_updateCount + " deltaTime unscaled Unity = " + Time.unscaledDeltaTime);

            //Debug.Log(m_updateCount + "My ElapsedTime unscaled Unity = " + m_unityUnscaledElapsedTime);
            Debug.Log(m_updateCount + "ElapsedTime unscaled Unity = " + Time.unscaledTime);
        }


        //StartCoroutine("CoroutineWaitOneSec", m_updateCount.ToString());
        m_updateCount++;


        // big sync operation
        // I put it at the end of Update voluntarily 
        if (m_checkBlockingOperation)
        {
            foreach (GameObject go in m_instantiatedGOs)
                DestroyImmediate(go);

            for (int i = 0; i < 40000; i++)
            {
                GameObject go = Instantiate(new GameObject());
                m_instantiatedGOs.Add(go);
            }
        }        
    }

    IEnumerator CoroutineTimer(float _tickDuration)
    {
        float timer = 0f;
        WaitForSeconds wait = new WaitForSeconds(_tickDuration);
        while (true)
        {            
            yield return wait;
            timer += _tickDuration;
            Debug.LogFormat("<b><color=#0000ffff>Timer Coroutine = " + timer + "</color></b>");
        }
    }

    IEnumerator CoroutineTimerWithLongOperation(float _tickDuration)
    {
        float timer = 0f;
        while (true)
        {
            yield return new WaitForSeconds(_tickDuration);
            timer += _tickDuration;
            Debug.LogFormat("<b><color=#0000ffff>Timer CoroutineTimerWithLongOperation = " + timer + "</color></b>");

            for (int i = 0; i < 100000; i++)
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

    IEnumerator CoroutineTimer_Unscaled(float _tickDuration)
    {
        float timer = 0f;
        while (true)
        {
            yield return new WaitForSecondsRealtime(_tickDuration);
            timer += _tickDuration;

            Debug.LogFormat("<b><color=#ffff00ff>Timer Coroutine_unscaled = " + timer + "</color></b>");
        }
    }
}
