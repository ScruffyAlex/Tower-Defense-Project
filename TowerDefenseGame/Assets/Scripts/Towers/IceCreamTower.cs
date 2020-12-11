using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamTower : Tower
{
    public override void Update()
    {
        base.Update();
    }

    public override void Create()
    {
        shootSpeed = 1;
        range = 2;
        base.Create();
    }

    public override void Upgrade()
    {
        base.Upgrade();
    }

    public override void Shoot()
    {
        base.Shoot();
    }
}

