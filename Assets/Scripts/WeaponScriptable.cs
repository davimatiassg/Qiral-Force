using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Weapon")]
public class WeaponScriptable : ScriptableObject
{
    public string weaponName;
    public Sprite spr;
    public float Cd;
    public GameObject Shot;
}
