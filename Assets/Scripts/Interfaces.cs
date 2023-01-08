using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitableObj
{
    void TakeDmg(float dmg, Vector2 knb){}
}

public interface ISolidObj
{
    void touchLine(Construction vtx1, Construction vtx2);
}

public interface IWebEffect
{
    void causeEffect(IHitableObj target);
}
