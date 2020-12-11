using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrillTower : Tower
{
    public override void Update()
    {
        base.Update();
    }

    public override void Create()
    {
        shootSpeed = 2;
        range = 4;
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

