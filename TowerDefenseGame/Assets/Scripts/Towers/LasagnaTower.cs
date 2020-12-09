using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasagnaTower : Tower
{
    //private float timer = 2.5f;

    public void Start()
    {
        Create();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Create()
    {
        shootSpeed = 5;
        range = 10;
        base.Create();
    }

    public override void Upgrade()
    {

    }

    public override void Shoot()
    {
        base.Shoot();
    }
}

