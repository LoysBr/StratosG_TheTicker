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

    //let's instantiate some Tickables for testing purpose
    void Start()
    {
        ExampleTickable tickable1 = new ExampleTickable(m_ticker, 0.5f);    //ticks twice a second
        tickable1.m_name = "ExampleTickable 1";
        ExampleTickable tickable2 = new ExampleTickable(m_ticker, 1f);      //ticks once a second
        tickable2.m_name = "ExampleTickable 2";

        tickable1.Start();
        tickable2.Start();
    }
}
