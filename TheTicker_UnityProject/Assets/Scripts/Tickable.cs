using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base Tickable Class. I assumed this is not a MonoBehavior as we
/// want to be independant of Unity Update loops.
/// </summary>
public class Tickable
{
    protected Ticker    m_ticker;
    protected float     m_tickDuration;
    protected int       m_ticksCount; 

    public Tickable(Ticker _ticker, float _tickDuration)
    {
        m_ticker = _ticker;
        m_tickDuration = _tickDuration;
    }    

    public void Start()
    {
        m_ticksCount = 0;
        if (m_ticker)
            m_ticker.RegisterTickable(this, m_tickDuration);
        else
            Debug.LogError(this.ToString() + " has a null Ticker reference.");
    }

    public void Stop()
    {
        if (m_ticker)
            m_ticker.StopTickable(this);
        else
            Debug.LogError(this.ToString() + " has a null Ticker reference.");       
    }

    virtual public void Tick()
    {        
        m_ticksCount++;
    }
}
