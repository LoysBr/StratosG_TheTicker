using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ticker : MonoBehaviour
{
    public abstract void RegisterTickable(Tickable _tickable, float _tickDuration);
    public abstract void StopTickable(Tickable _tickable);
}
