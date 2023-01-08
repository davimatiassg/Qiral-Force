using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : ShotBehaviour
{
    public override void Update()
    {
        base.Update();
        transform.position += dir*speed*Time.deltaTime;
    }
}
