using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleTickable : Tickable
{
    public string m_name;

    public ExampleTickable(Ticker _ticker, float _tickDuration) : base (_ticker, _tickDuration)
    {
        Debug.Log("New ExampleTickable created with tickDuration = " + _tickDuration);
    }

    //Example Tick implementation
    override public void Tick()
    {
        base.Tick();
        Debug.LogFormat("<b><color=#0000ffff>" + m_name + " tick " + m_ticksCount + "</color></b>");
    }
}
