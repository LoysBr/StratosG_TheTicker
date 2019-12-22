using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleGameManager : MonoBehaviour
{
    /// <summary>
    /// I chose to make it only accessible and not alterable via code
    /// So we can only set it via Unity Inspector 
    /// We could also made the Ticker a Singleton, depending on our needs
    /// </summary>
    [SerializeField]
    private Ticker m_ticker; 
    public Ticker Ticker { get { return m_ticker; } }

    private ExampleTickable m_tickable1;
    private ExampleTickable m_tickable2;

    //let's instantiate some Tickables for testing purpose
    void Start()
    {
        m_tickable1 = new ExampleTickable(m_ticker, 0.5f);    //ticks twice a second
        m_tickable1.m_name = "ExampleTickable 1";
        m_tickable2 = new ExampleTickable(m_ticker, 1f);      //ticks once a second
        m_tickable2.m_name = "ExampleTickable 2";

        m_tickable1.Start();
        m_tickable2.Start();

        StartCoroutine(StopTickableAfterSomeTimeTest(m_tickable1, 3f));
    }

    private IEnumerator StopTickableAfterSomeTimeTest(Tickable _tickable, float _time)
    {
        yield return new WaitForSeconds(_time);
        _tickable.Stop();
    }
}
