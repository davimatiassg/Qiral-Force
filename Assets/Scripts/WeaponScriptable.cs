using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    public string weaponName;
    public float range;
    public float dmg;
    public float knb;
    public float cd = 0.1f;
    public float arc = 1f;
    public float acel = 1;
    public Sprite spr;
    public float angle = 0f;
    public WeaponBehaviour W_class;
}
