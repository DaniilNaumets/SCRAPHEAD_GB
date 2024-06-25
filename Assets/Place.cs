using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    [SerializeField] private typePlace type;
    public enum typePlace
    {
        Gun, Engine
    }

    public typePlace GetTypePlace() => type;
}
