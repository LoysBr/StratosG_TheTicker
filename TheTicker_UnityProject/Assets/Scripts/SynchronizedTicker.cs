using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// We create only one Coroutine per tickDuration, in order to synchronize the ticks
/// for every Tickable having same tickDuration
/// </summary>
public class SynchronizedTicker : Ticker
{
    private HashSet<float>                      m_uniqueTickDurations; //used to check uniqueness
    private Dictionary<float, List<Tickable>>   m_registeredTickables;
    private Dictionary<float, Coroutine>        m_runningCoroutines;

    private void Awake()
    {
        m_uniqueTickDurations = new HashSet<float>();
        m_registeredTickables = new Dictionary<float, List<Tickable>>();
        m_runningCoroutines = new Dictionary<float, Coroutine>();
    }

    override public void RegisterTickable(Tickable _tickable, float _tickDuration)
    {
        //if it's a new tickDuration, create new Dictionary entry and start coroutine
        if (!m_uniqueTickDurations.Contains(_tickDuration))
        {
            m_uniqueTickDurations.Add(_tickDuration);

            List<Tickable> tickables = new List<Tickable>();
            tickables.Add(_tickable);
            m_registeredTickables.Add(_tickDuration, tickables);

            Coroutine routine = StartCoroutine(CoroutineTicker(_tickDuration));
            m_runningCoroutines.Add(_tickDuration, routine);
        }
        else //else simply register tickable
        {
            m_registeredTickables[_tickDuration].Add(_tickable);
        }       
    }

    override public void StopTickable(Tickable _tickable)
    {
        float duration = _tickable.GetTickDuration();

        if (m_uniqueTickDurations.Contains(duration)) 
        {
            m_registeredTickables[duration].Remove(_tickable);             

            if(m_registeredTickables[duration].Count == 0)
            {
                m_registeredTickables.Remove(duration);
                m_uniqueTickDurations.Remove(duration);

                StopCoroutine(m_runningCoroutines[duration]);
                m_runningCoroutines.Remove(duration);
                Debug.Log("Stopping Coroutine with duration = " + duration);
            }
        }
    }

    private IEnumerator CoroutineTicker(float _tickDuration)
    {
        WaitForSeconds wait = new WaitForSeconds(_tickDuration);
        while (true)
        {
            foreach(Tickable _tickable in m_registeredTickables[_tickDuration])
                _tickable.Tick();

            yield return wait;
        }
    }
}
