using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDamage : Bullet
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = null;
    }
}
