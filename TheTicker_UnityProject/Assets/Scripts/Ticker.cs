using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ticker : MonoBehaviour
{
    private HashSet<Tickable>               m_uniqueTickables;        //used to check uniqueness
    private Dictionary<Tickable, Coroutine> m_registeredTickables;

    private void Awake()
    {
        m_uniqueTickables = new HashSet<Tickable>();
        m_registeredTickables = new Dictionary<Tickable, Coroutine>();
    }

    public void RegisterTickable(Tickable _tickable, float _tickDuration)
    {
        if(!m_uniqueTickables.Contains(_tickable))
        {
            m_uniqueTickables.Add(_tickable);

            Coroutine ticker = StartCoroutine(CoroutineTicker(_tickable, _tickDuration));
            m_registeredTickables.Add(_tickable, ticker);
        }
    }

    public void StopTickable(Tickable _tickable)
    {
        if (m_uniqueTickables.Contains(_tickable))
        {
            m_uniqueTickables.Remove(_tickable);

            StopCoroutine(m_registeredTickables[_tickable]);
            m_registeredTickables.Remove(_tickable);
        }
    }

    private IEnumerator CoroutineTicker(Tickable _tickable, float _tickDuration)
    {
        WaitForSeconds wait = new WaitForSeconds(_tickDuration);
        while (true)
        {
            _tickable.Tick();  
            yield return wait;           
        }
    }
}
