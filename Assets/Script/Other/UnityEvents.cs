using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEvents : MonoBehaviour
{
    public static UnityEvent<float> EngineModuleEventPlus = new();
    public static UnityEvent<float> EngineModuleEventMultiplie = new();

    public static UnityEvent<bool> ShieldUpdateEvent = new();
}
